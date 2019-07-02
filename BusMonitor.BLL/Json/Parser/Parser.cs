using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMonitor.BLL.Extensions;

namespace BusMonitor.BLL.Json
{
    public class Parser {

        #region INIT

        Token[]             _tokens;
        int                 _head = 0;
        
        public Parser( Token[] tokens ) { 

            _tokens = tokens;
            _head = 0;

        }

        #endregion

        #region TOKENS

        Token Read() { 

            return ReadOffset ( 0 );

        }

        Token ReadNext() { 

            return ReadOffset ( 1 );

        }

        Token ReadOffset( int offset ) { 

            Token ret = null;

            if ( _tokens != null ) { 

                int read_head = _head + offset ;
                bool overflow = ( read_head >= _tokens.Length ) || ( read_head < 0 );

                if ( !overflow ) { 

                    ret = _tokens[read_head];
                }
            }

            return ret;
        }

        void MoveNext() { 

            _head++;

        }

        void MoveOffset ( int offset ) { 

            _head = _head + offset ;

        }

        Token Next() { 

            Token ret = Read();
            MoveNext();
            return ret;
        }

        bool ExpectedToken( Token token , TokenType type , string lexema="" ) { 

            bool ret= false;

            if ( token != null ) { 

                ret = type == token.type;
                
                if ( lexema.IsNotEmpty() ) { 

                    ret = ret && token.ToString() == lexema;

                }

            }

            return ret;
        }

        #endregion

        #region CONVERTS

        float ToFloat( string data ) { 

            float ret = default(float);

            if ( data.IsNotEmpty() ) { 

                if ( float.TryParse( data , NumberStyles.Any, CultureInfo.InvariantCulture,  out float aux ) ) { 

                    ret = aux;

                }
            }

            return ret;

        }

        #endregion

        #region DYNAMIC

        public static void AddProperty ( 

            System.Dynamic.ExpandoObject    expando , 
            string                          name, 
            object                          val

        ) {
                IDictionary<string, object> dic = expando as IDictionary<string, object>;

                if ( dic.ContainsKey ( name ) ) {

                    dic [ name ] = val;

                } else { 

                    dic.Add ( name , val );

                }
                    
        }


        #endregion

        #region REGLAS DE PRODUCCIÓN

        public dynamic Parse ( ) { 

            return ParseObject();

        }


        public dynamic ParseObject() { 

            dynamic ret = new System.Dynamic.ExpandoObject();

            Token start = Read();
            
            bool so_far = ExpectedToken( start , TokenType.Object , "{" );
            if ( so_far ) { 

                MoveNext();
                
                bool exit = false;
                while ( !exit ) { 

                    exit = true;

                    JsonKeyValue pair = ParseKeyValue();

                    if ( pair != null ) {

                        AddProperty ( ret , pair.Name , pair.Value );
                    }

                    Token next = Next();
                    bool end = ExpectedToken( next , TokenType.Object , "}" );

                    if ( !end ) { 

                        exit = !ExpectedToken( next , TokenType.Comma );
                    }
                }

            }

            return ret;
        }

        public object[] ParseArray() { 

            List<object> ret = new List<object>();

            Token start = Read();
            
            bool so_far = ExpectedToken( start , TokenType.Array , "[" );
            if ( so_far ) { 

                MoveNext();
                
                bool exit = false;
                while ( !exit ) { 

                    object new_item = ParseValue();
                    ret.Add ( new_item ); 

                    Token next = Next();
                    bool end = ExpectedToken( next , TokenType.Object , "]" );

                    if ( !end ) { 

                        exit = !ExpectedToken( next , TokenType.Comma );
                    }
                    
                }

            }

            return ret.ToArray();
        }

        public JsonKeyValue ParseKeyValue() { 

            JsonKeyValue ret = null;

            Token key = Read();
            
            bool so_far = ExpectedToken( key , TokenType.String );
            
            if ( so_far ) { 

                MoveNext();

                Token colon = Read();
                so_far = so_far && ExpectedToken( colon , TokenType.Colon );

                ret = new JsonKeyValue( key );

                if ( so_far ) { 

                    MoveNext();
                    ret.Value = ParseValue();
                }

            }

            return ret;
        }

        public object ParseValue() { 

            object ret = null;
            
            Token value = Read(); 
            string lexema = value.ToString();

            switch ( value.type ) {

                case TokenType.Object:
                    ret = ParseObject();
                    break;

                case TokenType.Array:
                    ret = ParseArray();
                    break;

                case TokenType.String:
                    ret = lexema.RemoveFirst().RemoveLast(); 
                    MoveNext();
                    break;

                case TokenType.Number:
                    ret = ToFloat(lexema); 
                    MoveNext();
                    break;

                case TokenType.Keyword:

                    switch ( lexema ) {

                        case "true":
                            ret = true;
                            break;

                        case "false":
                            ret = false;
                            break;

                        case "null":
                            ret = null;
                            break;

                        default:
                            break;
                    }
                    MoveNext();
                    break;

                default:
                    MoveNext(); 
                    break;
            }

                

            return ret;
        }

        #endregion

    }
}
