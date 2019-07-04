using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor.Display
{
    public class Terminal : BusDisplay
    {
        ConsoleColor prev_back;
        ConsoleColor prev_fore;


        public Terminal() {

            prev_back = Console.BackgroundColor;
            prev_fore = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Black;
        }

        ~Terminal() {

            Console.BackgroundColor = prev_back;
            Console.ForegroundColor = prev_fore;

        }

        ConsoleColor ResolveColor(int col) {

            ConsoleColor ret = ConsoleColor.DarkGray;

            switch (col)
            {
                case 1:
                    ret = ConsoleColor.Red;
                    break;
                case 2:
                    ret = ConsoleColor.Yellow;
                    break;
                default:
                    ret = ConsoleColor.DarkGray;
                    break;
            }

            return ret;

        }



        public void Clear() {

            Console.Clear();

        }

        //void SetPixel(int x, int y, int col) {
        //    //int offset = ( (y-1) * Console.WindowWidth) + x;
        //    //buffer[offset] = col;
        //}

        public void DrawPixel(int x, int y, int col) {
            //SetPixel(x, y, col);
            //RefreshBuffer();
        }

        public void DrawText(int x, int y, int col,string txt)
        {
            PaintBig ( txt , col );
        }

        #region POWERSHELL

        bool InScreen( int x , int y ) {

            bool ret = true;
	
	        if ( x < 0 || x > Console.WindowWidth || y < 0 || y > Console.WindowHeight ) {

		        ret = false;

	        }

	        return ret;
        }

        void SetPixel ( int x , int y , int color ) { 

	        bool paint = InScreen(x,y);
	        if ( paint ) {

                (int, int, ConsoleColor) before = ( Console.CursorLeft , Console.CursorTop , Console.BackgroundColor );
		        
		        Console.SetCursorPosition(x,y);
                Console.BackgroundColor = ResolveColor( color );

                Console.Write(" ");

                Console.SetCursorPosition(before.Item1 , before.Item2);
                Console.BackgroundColor = before.Item3;
	        }
        }

        void WriteBig ( int length, byte data, int color ) { 
		

            //$bg = (get-host).ui.rawui.BackgroundColor

            string binary_string = Convert.ToString( data , 2 ).PadLeft(length,'0');
            char[] arr = binary_string.ToCharArray();

            //ConsoleColor before = Console.ForegroundColor
            Console.ForegroundColor = ResolveColor( color );

		    foreach ( char c in arr ) {

                char pix = c == '1' ? '█' : ' ';
                Console.Write( pix );
            }

            
        }

        void PaintBig ( string txt , int color ) { 

	        //$OutputEncoding =  [Console]::OutputEncoding

	        Dictionary<string,byte[]> alphabet = new Dictionary<string, byte[]>();

	        alphabet["A"] = new byte[] { 0x03,0x07,0x05,0x07,0x05,0x05 };
	        alphabet["B"] = new byte[] { 0x03,0x06,0x05,0x06,0x05,0x06 };
	        alphabet["C"] = new byte[] { 0x03,0x07,0x04,0x04,0x04,0x07 };
	        alphabet["D"] = new byte[] { 0x03,0x06,0x05,0x05,0x05,0x06 };
	        alphabet["E"] = new byte[] { 0x03,0x07,0x04,0x07,0x04,0x07 };
	        alphabet["F"] = new byte[] { 0x03,0x07,0x04,0x07,0x04,0x04 };
	        alphabet["G"] = new byte[] { 0x03,0x07,0x04,0x04,0x05,0x07 };
	        alphabet["H"] = new byte[] { 0x03,0x05,0x05,0x07,0x05,0x05 };
	        alphabet["I"] = new byte[] { 0x01,0x01,0x01,0x01,0x01,0x01 };
	        alphabet["J"] = new byte[] { 0x03,0x01,0x01,0x01,0x05,0x07 };
	        alphabet["K"] = new byte[] { 0x04,0x09,0x0A,0x0C,0x0A,0x09 };
	        alphabet["L"] = new byte[] { 0x03,0x04,0x04,0x04,0x04,0x07 };
	        alphabet["M"] = new byte[] { 0x05,0x11,0x1B,0x15,0x11,0x11 };
	        alphabet["N"] = new byte[] { 0x04,0x09,0x0D,0x0B,0x09,0x09 };
	        alphabet["O"] = new byte[] { 0x03,0x07,0x05,0x05,0x05,0x07 };
	        alphabet["P"] = new byte[] { 0x03,0x07,0x05,0x07,0x04,0x04 };
	        alphabet["Q"] = new byte[] { 0x03,0x07,0x05,0x05,0x07,0x02 };
	        alphabet["R"] = new byte[] { 0x04,0x0F,0x09,0x0F,0x0A,0x09 };
	        alphabet["S"] = new byte[] { 0x03,0x07,0x04,0x07,0x01,0x07 };
	        alphabet["T"] = new byte[] { 0x03,0x07,0x02,0x02,0x02,0x02 };
	        alphabet["U"] = new byte[] { 0x03,0x05,0x05,0x05,0x05,0x07 };
	        alphabet["V"] = new byte[] { 0x05,0x11,0x11,0x11,0x0A,0x04 };
	        alphabet["W"] = new byte[] { 0x05,0x11,0x11,0x11,0x15,0x0A };
	        alphabet["X"] = new byte[] { 0x03,0x05,0x05,0x02,0x05,0x05 };
	        alphabet["Y"] = new byte[] { 0x03,0x05,0x05,0x05,0x02,0x02 };
	        alphabet["Z"] = new byte[] { 0x03,0x07,0x01,0x02,0x04, 0x07 };

            alphabet["0"] = new byte[] { 0x03,0x07,0x05,0x05,0x05,0x07 };
	        alphabet["1"] = new byte[] { 0x02,0x01,0x03,0x01,0x01,0x01 };
	        alphabet["2"] = new byte[] { 0x03,0x07,0x01,0x07,0x04,0x07 };
	        alphabet["3"] = new byte[] { 0x03,0x07,0x01,0x07,0x01,0x07 };
	        alphabet["4"] = new byte[] { 0x03,0x05,0x05,0x07,0x01,0x01 };
	        alphabet["5"] = new byte[] { 0x03,0x07,0x04,0x07,0x01,0x07 };
	        alphabet["6"] = new byte[] { 0x03,0x07,0x04,0x07,0x05,0x07 };
	        alphabet["7"] = new byte[] { 0x03,0x07,0x01,0x02,0x02,0x02 };
	        alphabet["8"] = new byte[] { 0x03,0x07,0x05,0x07,0x05,0x07 };
	        alphabet["9"] = new byte[] { 0x03,0x07,0x05,0x07,0x01,0x01 };
                                                                       
	        alphabet[" "] = new byte[] { 0x01,0x00,0x00,0x00,0x00,0x00 };
	        alphabet["."] = new byte[] { 0x01,0x00,0x00,0x00,0x00,0x01 };
	        alphabet[":"] = new byte[] { 0x01,0x00,0x00,0x01,0x00,0x01 };
	        alphabet["!"] = new byte[] { 0x01,0x01,0x01,0x01,0x00,0x01 };
	        alphabet["-"] = new byte[] { 0x04,0x00,0x00,0x0F,0x00,0x00 };
	        alphabet["/"] = new byte[] { 0x03,0x01,0x01,0x02,0x04,0x04 };
	        alphabet["\\"] = new byte[] { 0x03,0x04,0x04,0x02,0x01,0x01 };
	        alphabet["("] = new byte[] { 0x02,0x01,0x02,0x02,0x02,0x01 };
	        alphabet[")"] = new byte[] { 0x02,0x02,0x01,0x01,0x01,0x02 };
	        alphabet["["] = new byte[] { 0x02,0x03,0x02,0x02,0x02,0x03 };
	        alphabet["]"] = new byte[] { 0x02,0x03,0x01,0x01,0x01,0x03 };
	        alphabet["<"] = new byte[] { 0x03,0x01,0x02,0x04,0x02,0x01 };
	        alphabet[">"] = new byte[] { 0x03,0x04,0x02,0x01,0x02,0x04 };
	        alphabet["{"] = new byte[] { 0x03,0x03,0x02,0x06,0x02,0x03 };
	        alphabet["}"] = new byte[] { 0x03,0x06,0x02,0x03,0x02,0x06 };
	        alphabet["-"] = new byte[] { 0x02,0x00,0x00,0x03,0x00,0x00 };
	        alphabet[","] = new byte[] { 0x02,0x00,0x00,0x00,0x01,0x02 };
	        alphabet[";"] = new byte[] { 0x02,0x00,0x01,0x00,0x01,0x02 };

	//$len = $txt.Length 
	//$window_width = $Host.UI.RawUI.WindowSize.Width
            
            List<string> lines = new List<string>();
            string line = "";
	        int offset_line = 0;
	
	        for ( int i=0; i < txt.Length; i++ ) {

		        string letter = txt[i].ToString().ToUpper();
		        offset_line = offset_line + alphabet[letter][0] + 1;

		        if ( offset_line >= Console.WindowWidth ) {

			        lines.Add ( line );
			        offset_line = 0;
			        line = letter;

		        } else {

			        line = $"{line}{letter}";

		        }
	        }

	        if ( line.Length > 0 ) { lines.Add( line ); }

            lines.SafeForEach ( l => { 

                int computed_len = l.Length;


                for ( int subline = 1;subline < 6;subline++ ) {

                    Console.WriteLine("");
                    Console.Write( " " );
   
                    for ( int c = 0; c <  computed_len; c++ ) {

				        string letter = l[c].ToString().ToUpper();
   				        int letter_len = alphabet[letter][0];
   
                        WriteBig( letter_len , alphabet[letter][subline] , color );
                        Console.Write( " ");
                
   

                    }
                }

                Console.WriteLine("");

            });

	        Console.WriteLine();
            Console.WriteLine();
	
        }

        void PaintInvader ( int color )
        {
            Console.WriteLine("   ▀▄   ▄▀   ");
            Console.WriteLine("  ▄█▀███▀█▄  ");
            Console.WriteLine(" █▀███████▀█ ");
            Console.WriteLine(" ▀ ▀▄▄ ▄▄▀ ▀ ");

            // ▀▄█
        }


//function Paint-Ghost {  
	
//	  param([string] $color)
  
//	  if ( [string]::IsNullOrWhiteSpace($color ) ) {
//		  $color = "Green";
//	  }
	
//	  Write-Host  "   ▄██████▄  " -ForegroundColor $color
	
//	  Write-Host -NoNewLine " ▄"   -ForegroundColor $color
//	  Write-Host -NoNewLine "█▀█" -ForegroundColor White
//	  Write-Host -NoNewLine "██"  -ForegroundColor $color
//	  Write-Host -NoNewLine "█▀█" -ForegroundColor White
//	  Write-Host  "██▄ " -ForegroundColor $color

//	  Write-Host -NoNewLine " █"   -ForegroundColor $color
//	  Write-Host -NoNewLine "▄▄█" -ForegroundColor White
//	  Write-Host -NoNewLine "██"  -ForegroundColor $color
//	  Write-Host -NoNewLine "▄▄█" -ForegroundColor White
//	  Write-Host  "███ " -ForegroundColor $color

//	  Write-Host  " ████████████ " -ForegroundColor $color
//	  Write-Host  " ██▀██▀▀██▀██ " -ForegroundColor $color
//	  Write-Host  " ▀   ▀  ▀   ▀ " -ForegroundColor $color
//	  # ▀▄█
   
//  }


//function Internal-Save-Buffer {
	
//	$rect =  New-Object System.Management.Automation.Host.Rectangle 0,0,($Host.UI.RawUI.BufferSize.Width-1),($Host.UI.RawUI.BufferSize.Height-1)
//	$script:buffer = $Host.UI.RawUI.GetBufferContents($rect)
//}

//function Internal-Restore-Buffer {

//	if ( $script:buffer -ne $null ) {

//		$coord = new-object System.Management.Automation.Host.Coordinates 
//		$Host.UI.RawUI.SetBufferContents($coord,$script:buffer)

//	}
//}

//function Internal-Wait-Keypressed {
//	For(;;) {

//			if ($host.ui.RawUi.KeyAvailable) { 

//					$key = $host.ui.RawUI.ReadKey("NoEcho,IncludeKeyDown") 

//					if ( $key.VirtualKeyCode ) {
//							break
//					}

					
//			}

//			Start-Sleep -MilliSeconds 500
//	}
//}


//function Pause {

//	param([string]$alt_msj) 

//	$display = " -- PAUSA -- Pulsa una tecla para continuar...";
//	if (-not([string]::IsNullOrWhiteSpace($alt_msj))) {  

//		$display = $alt_msj;
//	} 

//	Write-Host $display
//	$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');

//}

#endregion	

   

    }
}
