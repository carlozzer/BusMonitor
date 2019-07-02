using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BusMonitor.BLL.Validation
{
    public class Validator
    {
        #region GENERAL

        public static bool IsNotEmpty(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static bool IsX<T>(string data)
        {
            bool ret = false;
            T converted = default(T);

            if (!string.IsNullOrWhiteSpace(data))
            {
                string trimmed = data.Trim();

                try
                {
                    Type U = Nullable.GetUnderlyingType(typeof(T));
                    if (U != null)
                    {
                        converted = (T)Convert.ChangeType(data, U);
                    }
                    else
                    {
                        converted = (T)Convert.ChangeType(data, typeof(T));
                    }
                    ret = converted != null;
                }
                catch (Exception ex)
                {
                    //ExceptionHelper.Manage(ex);
                    ret = false;
                }
            }

            return ret;
        }

        #endregion

        #region NUMBERS

        public static bool IsYYYY(string year)
        {
            bool ret = false;

            if (!string.IsNullOrWhiteSpace(year))
            {
                string trimmed = year.Trim();
                if (trimmed.Length == 4)
                {
                    int result;
                    if (Int32.TryParse(trimmed, out result))
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public static bool IsInteger(string integer)
        {
            bool ret = false;

            if (!string.IsNullOrWhiteSpace(integer))
            {
                string trimmed = integer.Trim();
                Int64 result;
                if (Int64.TryParse(trimmed, out result))
                {
                    ret = true;
                }
            }

            return ret;
        }

        public static bool IsNumber(string number)
        {
            bool ret = false;

            if (!string.IsNullOrWhiteSpace(number))
            {
                string trimmed = number.Trim();
                Double result;
                if (Double.TryParse(trimmed, out result))
                {
                    ret = true;
                }
            }

            return ret;
        }

        public static bool IsDateTime(string datetime)
        {
            bool ret = false;

            if (!string.IsNullOrWhiteSpace(datetime))
            {
                string trimmed = datetime.Trim();
                DateTime result;
                if (DateTime.TryParse(trimmed, out result))
                {
                    ret = true;
                }
            }


            return ret;
        }

        #endregion

        #region BOOLEAN

        private static bool IsText( string text , string compare ) {

            bool ret = text.Safe().Trim().ToLower() == compare.Safe().Trim().ToLower();
            return ret;

        }

        public static bool IsTrue ( string boolean )
        {
            bool ret = IsText( boolean, "true");
            return ret;
        }

        public static bool IsTrueYesSiVerdadero1OK(string boolean)
        {
            List<bool> cases = new List<bool>() {
                IsText(boolean, "true"),
                IsText(boolean, "yes"),
                IsText(boolean, "1"),
                IsText(boolean, "si"),
                IsText(boolean, "sí"),
                IsText(boolean, "verdadero"),
                IsText(boolean, "ok")
            };

            bool ret = cases.Where(b => b).Count() > 0;

            return ret;
        }

        #endregion

        #region EMAIL ADDRESS

        public static bool 
        IsEmailAddress ( string email ) {

            bool ret = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return ret;
        }

        #endregion

        #region DNI

        public static bool 
        IsDNI ( string dni ) {

            bool ret = false;

            Regex regex = new Regex( "^[0-9]{8}[a-zA-Z]$" );
            if ( regex.IsMatch( dni.Safe() ) ) {

                int    numero = Convert.ToInt32( dni.Substring( 0, dni.Length - 1 ) );
                string letra  = dni.Substring( dni.Length - 1, 1 );
                
                numero = numero % 23;
                string LETRAS ="TRWAGMYFPDXBNJZSQVHLCKET";
                char letra_valida = LETRAS[numero];

                if (letra_valida.ToString() == letra.ToUpper()) {
                    ret = true; // formato y letra correcta
                }
            }

            return ret;
        }


        //var ValidarNIE = function (nie) {

        //   var ret = false;

        //   if (nie) {

        //      var start_ok = false;
        //      var first_number = '';

        //      var start_x = nie.substring(0,1).toLowerCase() === 'x';
        //      var start_y = nie.substring(0,1).toLowerCase() === 'y';
        //      var start_z = nie.substring(0,1).toLowerCase() === 'z';

        //      if (start_x ) { start_ok = true; first_number = '0'; }
        //      if (start_y ) { start_ok = true; first_number = '1'; }
        //      if (start_z ) { start_ok = true; first_number = '2'; }

        //      if (start_ok) {
        //         var dni = '' + first_number + nie.substring(1);
        //         ret = ValidarDNI(dni);
        //      }

        //   }

        //   return ret;
        //};

        //var ValidarPasaporte = function( pasaporte ) {
        //   var ret = false;

        //   if (pasaporte){
        //      ret = (pasaporte.length > 0);
        //   }

        //   return ret;
        //};

        //var ValidarID = function (codigo, tipo)  {

        //   var ret = false;

        //   if (tipo.toLowerCase() === 'dni') {
        //      ret = ValidarDNI(codigo);
        //   }

        //   if (tipo.toLowerCase() === 'nie') {
        //      ret = ValidarNIE(codigo);
        //   }

        //   if (tipo.toLowerCase() === 'pasaporte') {
        //      ret = ValidarPasaporte(codigo);
        //   }

        //   return ret;
        //};


        #endregion

        #region IBAN

        static int TranslateChar( char c ) { 

            int ret = 0;

            ret = ( (int) c - (int)'A' ) + 10;

            return  ret;

        }

        static string TranslateAccount( string account ) { 

            string ret = string.Empty;

            account.Safe().SafeForEach( c => { 

                if ( Validator.IsInteger( c.ToString() ) ) { 

                    ret = string.Concat( ret , c );

                } else { 

                    ret = string.Concat( ret , TranslateChar( c ).ToString() );
                }

            });

            return ret;

        }

        static Nullable<int> CountryCode ( string pais ) { 

            Nullable<int> ret = default( Nullable<int> );

            if ( pais.Safe().IsMatch(@"^[A-Z]{2}$") ) { 

                // A = 10, B = 11 ... Z = 35
                ret = TranslateChar( pais[0] ) * 100;
                ret = ret + TranslateChar( pais[1] );
            }

            return ret;
        }

        public static bool IsIBAN ( string iban ) { 

            string clean = Regex.Replace( iban.Safe(), @"\s+", "").Safe();

            string country = clean.SafeSubstring ( 0,2 );
            string cc      = clean.SafeSubstring ( 2,2 );
            string account = clean.SafeSubstring ( 4 );

            string working = $"{TranslateAccount(account)}{CountryCode(country)}{cc}";

            int head = 0;
            int step = 9;

            string segment = working.SafeSubstring( head , step ); 
            int    mod     = segment.ToIntOrZero() % 97;
            head += step;

            while ( head <= working.Length ) { 

                segment = mod.ToString().PadLeft(2,'0') + working.SafeSubstring( head , step - 2 ); 
                mod = segment.ToIntOrZero() % 97;
                head += step - 2;
                
            }

            return mod == 1;
        }

        #endregion
    }
}
