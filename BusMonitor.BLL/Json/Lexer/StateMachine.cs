using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Json
{
    public class StateMachine 
    {
        bool accepting_state = false;
        public bool IsAcceptingState => accepting_state;

        TokenType type_detected = TokenType.Unknown;
        public TokenType TypeDetected => type_detected;

        static int ERR = State.ERR;
        static int UNK = 14; // unknown

        static int[,] transitions = new int[8,15] { 

//           BLANK  CR    {     :     }     "     '     [     ]     ,     .    +-    NUM  ALPHA  UNK
/* BLANK 0*/{ ERR , ERR,  0  ,  0  ,  0  , 102 , 103 ,  0  ,  0  ,  0  , 105 , 104 , 104 , 107 , ERR },
/*  CR   1*/{ ERR , ERR, ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR },
/*   "   2*/{ 102 , ERR, 102 , 102 , 102 ,  0  , 102 , 102 , 102 , 102 , 102 , 102 , 102 , 102 , 102 },
/*   '   3*/{ 103 , ERR, 103 , 103 , 103 , 103 ,  0  , 103 , 103 , 103 , 103 , 103 , 103 , 103 , 103 },
/*  NUM  4*/{ -1  , -1 , ERR , ERR , -1  , ERR , ERR , ERR , -1  , -1  , 104 , ERR , 104 , ERR , ERR },
/*  NUM  5*/{ ERR , ERR, ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , ERR , 106 , ERR , ERR },
/*  NUM  6*/{ -1  , -1 , ERR , ERR , -1  , ERR , ERR , ERR , ERR , -1  , ERR , ERR , 106 , ERR , ERR },
/* ALPHA  */{ -1  , -1 , ERR , ERR , -1  , ERR , ERR , ERR , -1  , -1  , ERR , ERR , ERR , 107 , ERR },
        };
        
        static int ByteToInputSymbol( byte b ) { 

            int ret = UNK;

            switch (b) {
                case 0x00: // NULL
                case 0x09: // HORIZONTAL TAB
                case 0x20: // SPACE
                    ret = 0;
                    break;

                case 0x0A: // LINE FEED
                case 0x0C: // FORM FEED
                case 0x0D: // CARRIAGE RETURN
                    ret = 1;
                    break;

                case 0x7B: // {
                    ret = 2;
                    break;
                case (int)':': 
                    ret = 3;
                    break;
                case 0x7D: // }
                    ret = 4;
                    break; 

                case (int)'"': 
                    ret = 5;
                    break;
                case (int)'\'': 
                    ret = 6;
                    break; 

                case 0x5B: // [ 
                    ret = 7;
                    break;
                case 0x5D: // ]
                    ret = 8;
                    break;

                case (int)',': 
                    ret = 9;
                    break;
                case (int)'.': 
                    ret = 10;
                    break;

                case (int)'+': 
                case (int)'-':
                    ret = 11;
                    break;

                case (int)'0':
                case (int)'1':
                case (int)'2':
                case (int)'3':
                case (int)'4':
                case (int)'5':
                case (int)'6':
                case (int)'7':
                case (int)'8':
                case (int)'9':
                    ret = 12;
                    break;

                default:
                                        
                    if ( 
                        ( ( b >= 'a' ) && ( b <= 'z' ) ) || 
                        ( ( b >= 'A' ) && ( b <= 'Z' ) ) 
                    ){ 
                        ret = 13;
                    } 
                    
                    break;
            }

            return ret;
        }
        
        static TokenType GetTokenType( int state , byte b ) { 

            TokenType ret = TokenType.Unknown;

            if ( state == 100 ) {

                switch ( b ) {
            

                    case 0x7B: // {
                    case 0x7D: // }
                        ret = TokenType.Object;
                        break;
                    case (int)':': 
                        ret = TokenType.Colon;
                        break;
                    case 0x5B: // [ 
                    case 0x5D: // ]
                        ret = TokenType.Array;
                        break;
                    case (int)',': 
                        ret = TokenType.Comma;
                        break;
                
                }

            } else { 

                switch (state) {

                    case 102:
                    case 103:
                        ret = TokenType.String;
                        break;

                    case 104:
                    case 105:
                    case 106:
                        ret = TokenType.Number;
                        break;

                    case 107:
                        ret = TokenType.Keyword;
                        break;
                }
            }

            return ret;
        }

        public int Transition ( int current_state , byte b ) {

            int symbol = ByteToInputSymbol ( b );
            int state = transitions[ current_state - State.q0 , symbol ];

            type_detected = GetTokenType( current_state , b );
            accepting_state = state <= 0;

            return state;
        }
    }
}
