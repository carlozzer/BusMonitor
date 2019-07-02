using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Json
{
    public class Lexer {

        Token           token;
        int             q;
        int             head;

        StateMachine   machine;

        void Reset ( bool full=false ) { 

            token   = new Token();
            q       = State.q0;

            if ( full ) { 

                head = 0;
                machine = new StateMachine(); 
            }
        }

        public Token[] ParseBytes ( byte[] buffer ) { 
            
            int cont = 0;
            Token[] tokens = null;

            if ( buffer != null ) {
            
                tokens = new Token[buffer.Length];

                Reset ( full:true );

                while ( head < buffer.Length ) { 

                    byte b = buffer[head];

                    token.AddByte ( b );

               
                    q = machine.Transition ( q , b );
                        
                    if ( machine.IsAcceptingState ) { 

                        if ( q != State.ERR ) {

                            token.RemoveRecoil ( q );
                            token.type = machine.TypeDetected;

                            tokens[cont++] = token;
                        
                            head += q;

                        } // else IGNORE ERROR
                            
                        Reset ();
                            
                    } 
                
                    head++;
                }

            }

            Token[] ret = null;

            if ( tokens != null && cont > 0 ) { 

                ret = new Token[ cont ];
                Array.Copy ( tokens , 0 , ret , 0 , cont );

            }

            return ret;

        }

        


    }
}
