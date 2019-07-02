using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Json
{
    public class Token {

        int ChunkSize  = 512; // bytes
        int head;
        int BufferSize;

        byte[] lexema { get; set; }

        public TokenType type { get; set; }

        #region CONSTRUCTOR

        public Token() { 

            BufferSize = ChunkSize;
            lexema = new byte[BufferSize];
            head = 0;
        }

        #endregion


        #region ADD BYTES

        byte[] AddChunk ( ) { 

            BufferSize = BufferSize + ChunkSize;
            byte[] new_buffer = new byte[ BufferSize ];
            Array.Copy ( lexema , 0 , new_buffer , 0 , lexema.Length );
            
            return new_buffer;

        }

        public void AddByte ( byte b ) {

            if ( head >= BufferSize ) { 

                lexema = AddChunk( );
                lexema[head] = b;

            } else { 

                lexema[ head ] = b;

            }

            head++;
        }

        #endregion

        #region REMOVE RECOIL

        public void RemoveRecoil ( int recoil ) { 

            if ( lexema != null ) { 

                int new_length = head + recoil; // recoil es <= 0
                byte[] buffer = new byte [ new_length ];

                Array.Copy ( lexema , buffer , new_length );

                lexema = new byte [ new_length ];

                Array.Copy ( buffer , lexema , new_length );

            }

        }

        #endregion

        #region TOSTRING

        public override string ToString()
        {
            // TODO implementación para salir del paso y poder depurar
            StringBuilder display = new StringBuilder();

            if ( lexema != null ) { 

                foreach ( byte b in lexema ) { 

                    char c = (char)b;
                    if ( !char.IsControl(c) ) { 

                        display.Append( c );
                    }
                }
            }
            return display.ToString();
        }

        #endregion

        public byte[] ReadBytes() { 

            return lexema;
        }
    }
}
