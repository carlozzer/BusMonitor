using BusMonitor.BLL.Validation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class StringExtensions
    {
        #region SAFE
        
        public static string Safe(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty : str;
        }

        public static string SafeSubstring ( this string str , int StartIndex , int length=0 ) {

            string ret = string.Empty;

            string work_string = str.Safe();

            if ( work_string.Length > StartIndex ) { 

                if ( length == 0 || work_string.Length <= StartIndex + length ) { 

                    ret = work_string.Substring( StartIndex );

                } else { 

                    ret = work_string.Substring( StartIndex , length );
                }

            }

            return ret;
        }

        public static string ValueOrDefault(this string str, string defaultVal)
        {
            string ret = str.IsEmpty() ? defaultVal : str;
            return ret;
        }

        #endregion

        #region CHECKS

        public static bool IsNotEmpty(this string str)
        {
            string work_string = str.Safe();
            return work_string.Length > 0;
        }

        public static bool IsEmpty(this string str)
        {
            bool ret = true;

            if (str != null) {
                ret = string.IsNullOrWhiteSpace(str);
            }

            return ret;
        }

        [Obsolete("Nombre incompleto")]
        public static bool NotEmpty(this string str)
        {
            string work_string = str.Safe();
            return work_string.Length > 0;
        }

        [Obsolete("Nombre incompleto")]
        public static bool Empty(this string str)
        {
            bool ret = true;

            if (str != null) {
                ret = string.IsNullOrWhiteSpace(str);
            }

            return ret;
        }

        public static bool IsInteger (this string str )
        {
            bool ret = Validator.IsInteger( str.Safe() );
            return ret;
        }

        public static bool IsMatch( this string str , string pattern ) {

            bool ret = false;

            if ( str.IsNotEmpty() && pattern.IsNotEmpty() ) {

                Regex reg = new Regex(pattern.Safe());
                ret = reg.IsMatch( str.Safe() );

            }

            return ret;
        }


        #endregion

        #region TRANSFORM

        public static string Preview(this string obj, int max)
        {
            string ret = obj;

            if (obj != null && obj.Length > max)
            {
                ret = string.Concat(obj.Substring(0, max), "...");
            }

            return ret;
        }

        public static string FirstUpper(this string text)
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(text))
            {
                string work_string = text.ToLower();
                string before = work_string.Substring(0, 1);
                string after = work_string.Substring(1);
                ret = string.Concat(before.ToUpper(), after);
            }

            return ret;
        }

        public static string Singular(this string text)
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(text))
            {
                if (text.ToLower().EndsWith("s"))
                {
                    ret = text.Substring(0, text.Length - 1);
                }
            }

            return ret;
        }

        /// <summary>
        /// Extension method to string class for replacing substrings ingoring case
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="textToFind">Text to find</param>
        /// <param name="textToReplace">Text to replace</param>
        /// <param name="ignoreCase">true if the matching ignores case; false otherwise</param>
        /// <returns>Replaced string</returns>
        public static string Replace(this string text, string textToFind, string textToReplace, bool ignoreCase)
        {
            if (!ignoreCase)
                return text.Replace(textToFind, textToReplace);

            var regex = new Regex(textToFind, RegexOptions.IgnoreCase);
            string aux = regex.Replace(text, textToReplace);
            return aux;
        }

        /// <summary>
        /// Appends the string list to one single string
        /// </summary>
        /// <param name="list">Input string list</param>
        /// <returns>Resulting string</returns>
        public static string Join(this List<string> list)
        {
            var sb = new StringBuilder();
            foreach (string str in list)
                sb.Append(str);
            return sb.ToString();
        }

        /// <summary>
        /// Extract the metadata search filters from a keyword search
        /// </summary>
        /// <param name="keyWordQuery">The search expression</param>
        /// <param name="nvc">The metadata filters contained within the expression</param>
        /// <returns>The pure keywords from the expression</returns>
        public static string ExtractMetadataFilters(string keyWordQuery, out NameValueCollection nvc)
        {
            nvc = new NameValueCollection();
            if (string.IsNullOrEmpty(keyWordQuery))
                return "";

            string keyWord = keyWordQuery;

            var regex = new Regex(@"((""[^""]+"")|([^"" ]+)):((""[^""]+"")|([^"" ]+))");
            MatchCollection mc = regex.Matches(keyWordQuery);
            foreach (Match m in mc)
            {
                string matchVal = m.Value.Replace("\"", string.Empty);
                string name = matchVal.Split(':')[0];
                string val = matchVal.Split(':')[1];
                nvc.Add(name, val);
            }

            return regex.Replace(keyWordQuery, string.Empty);
        }

        public static string[] ExtractSeparatedValues(this string str, string separator)
        {
            string[] ret = null;

            if (!string.IsNullOrWhiteSpace(str))
            {
                ret = str.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }

            return ret;
        }

        public static string RemoveLineEndings(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
        }

        public static string CurrencyEuro(this string amount)
        {
            string ret = string.Empty;

            if (!string.IsNullOrEmpty(amount))
            {
                if (NumberHelper.IsNumeric(amount))
                {
                    ret = string.Format("{0:#.00} €", Convert.ToDecimal(amount));
                    if (ret.StartsWith(",")) ret = string.Concat(0, ret);
                }
            }

            return ret;
        }

        public static string RemoveHtml(this string str) {

            string ret = string.Empty;

            string pattern = @"<(.|\n)*?>";
            ret = Regex.Replace(str.Safe(), pattern, string.Empty);

            return ret;
        }

        public static string 
        AddQuotes ( this string str, string quote="'" ) {
            return "{0}{1}{0}".ApplyFormat(quote, str.Safe());
        }

        public static string
        AddDoubleQuotes(this string str) {
            return "{0}{1}{0}".ApplyFormat("\"", str.Safe());
        }

        public static string
        RemoveFirst(this string str) {

            string ret = string.Copy(str.Safe());

            if (str.Safe().Length > 0) {
                ret = str.Safe().Substring(1);
            }

            return ret;
        }

        public static string
        RemoveFirstIfStartsWith(this string str, char ch) {

            string ret = string.Copy(str.Safe());

            if (str.Safe().StartsWith(ch.ToString())) {
                ret = RemoveFirst(str);
            }

            return ret;
        }

        public static string
        RemoveLast(this string str) {

            string ret = string.Copy(str.Safe());

            if (str.Safe().Length > 0) {
                ret = str.Safe().Substring(0, str.Safe().Length -1);
            }

            return ret;
        }

        public static string
        RemoveLastIfEndsWith(this string str, char ch) {

            string ret = string.Copy(str.Safe());

            if (str.Safe().EndsWith(ch.ToString())) {
                ret = RemoveLast(str);
            }

            return ret;
        }

        public static string
        RemoveEmptyLines ( this string str ) {

            StringBuilder ret = new StringBuilder();

            if ( str.IsNotEmpty() ) {

                using ( StringReader reader = new StringReader( str )) {

                    string line = string.Empty;
                    while ((line = reader.ReadLine()) != null) {

                        if ( line.IsNotEmpty() ) {

                            ret.AppendLine(line);

                        }
                    }
                }

            }

            return ret.ToString();
        }

        public static string 
        EnsureOneDot ( this string cad ) {

            string ret = "";
            bool dot_hit = false;

            cad.Safe().SafeForEach( ( c , index ) => {

                bool first = index == 0;
                bool last = index == cad.Safe().Length - 1;
                bool match = false;

                if (c == '.') {

                    if ( !dot_hit ) {

                        if ( !first && !last ) {

                            match = true;

                        }

                        dot_hit = true;

                    }

                } else {

                    dot_hit = false;
                    match = true;

                }

                if (match) {

                    ret = string.Concat ( ret , c.ToString() );

                }

            });

            ret = ret.Safe().RemoveLastIfEndsWith('.');

            return ret;
        }

        public static string
        RemoveDiacritics ( this string str ) {

            string ret = string.Empty;

            if ( str.IsNotEmpty() ) {
            
                ret = new string(
                
                    str.Normalize(System.Text.NormalizationForm.FormD)
                         .ToCharArray()
                         .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                         .ToArray());
                    // the normalization to FormD splits accented letters in accents+letters
                    // the rest removes those accents (and other non-spacing characters)
            }

            return ret;
        }


        #endregion

        #region DATES

        static string ExtractSafeDateTimeIndex(string dt, int index)
        {
            string ret = string.Empty;

            string[] fechahora = dt.Safe().Split(" ".ToCharArray());
            if (fechahora.SafeLength() > index) {
                ret = fechahora[index];
            }

            return ret;
        }

        static string ExtractSafeDate(string dt) {
            return ExtractSafeDateTimeIndex(dt,0);
        }

        static string ExtractSafeTime(string dt)
        {
            return ExtractSafeDateTimeIndex(dt, 1);
        }


        static Nullable<int> ParseDateSegment(string date, int index, string separator) {
            
            Nullable<int> ret = null;

            // bounds
            if (index >= 0 && index <= 2) {

                string[] segments = date.Safe().Split(separator.ToCharArray());
                if (segments.SafeLength() == 3) {

                    string int_string = segments[index];
                    int int_aux;
                    if (Int32.TryParse(int_string, out int_aux)) {
                        ret = int_aux;
                    }
                }
            }

            return ret;
        }

        static Nullable<int> ParseTimeSegment(string time, int index)
        {

            Nullable<int> ret = 0;

            // bounds
            if (index >= 0 && index <= 4) // solo haremos caso a los 3 primeros, mínimo 2 (hora y minutos)
            {
                string[] segments = time.Safe().Split(":".ToCharArray());
                if (segments.SafeLength() >= 2 && segments.SafeLength() > index)
                {
                    string int_string = segments[index];
                    int int_aux;
                    if (Int32.TryParse(int_string, out int_aux))
                    {
                        ret = int_aux;
                    }
                }
            }

            return ret;
        }



        static Nullable<DateTime> BuildNullableDateTime(params Nullable<int>[] parts) {

            Nullable<DateTime> ret = null;

            if (parts.SafeLength() == 6 && parts.NotASingleNull())
            {
                int dia     = parts[0].Value;       // +semántico
                int mes     = parts[1].Value;
                int ano     = parts[2].Value;
                int hora    = parts[3].Value;
                int minuto  = parts[4].Value;
                int segundo = parts[5].Value;

                if (DateHelper.CheckInBounds(dia, mes, ano, hora, minuto, segundo)) {
                    ret = new DateTime(ano, mes, dia, hora, minuto, segundo);
                }
            }

            return ret;
        }

        static Nullable<DateTime> 
        ToNullableDateTime ( 
                string str_date, 
                string separator, 
                int DayIndex, 
                int MonthIndex, 
                int YearIndex ) {

            Nullable<DateTime> ret = null;

            string fecha = ExtractSafeDate(str_date);
            string hora = ExtractSafeTime(str_date);
            
            ret = BuildNullableDateTime (
                        ParseDateSegment(fecha, DayIndex, separator),
                        ParseDateSegment(fecha, MonthIndex, separator),
                        ParseDateSegment(fecha, YearIndex, separator),
                        ParseTimeSegment(hora,0),
                        ParseTimeSegment(hora,1),
                        ParseTimeSegment(hora,2)
            );

            return ret;
        }

        public static Nullable<DateTime> ToNullableDateTime(this string str, string format = "dd/MM/yyyy")
        {
            Nullable<DateTime> ret = null;

            string working_format = format.Safe().ToLower();

            if ( working_format == "yyyy-mm-dd") {
                ret = ToNullableDateTime(str, "-", 2, 1, 0);
            }

            if (working_format == "yyyy-dd-mm")
            {
                ret = ToNullableDateTime(str, "-", 1, 2, 0);
            } 

            if ( working_format == "dd/mm/yyyy") {
                ret = ToNullableDateTime(str, "/", 0, 1, 2);
            } 

            return ret;
        }


        #endregion

        #region INTEGER

        public static Nullable<int> 
        ToNullableInt ( this string str_int ) { 

            Nullable<int> ret = default(Nullable<int>);

            bool is_int = Validator.IsInteger( str_int.Safe() );
            if ( is_int ) { 

                ret = Convert.ToInt32( str_int );

            }

            return ret;
        }

        public static int 
        ToIntOrZero ( this string str_int ) { 

            int ret = str_int.ToNullableInt() ?? 0;
            return ret;
        }
        
        #endregion

        #region ENUM

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            T ret = defaultValue;

            if (value.IsNotEmpty())
            {
                T aux;
                if (Enum.TryParse<T>(value, true, out aux)) {
                    ret = aux;
                }
            }

            return ret;
        }

        #endregion

        #region FORMAT

        public static string 
        ApplyFormat ( this string text, params object[] args ) {

            string ret = (args != null && args.Length > 0) ? string.Format(text.Safe(), args) : text.Safe();
            return ret;
        }

        #endregion

        #region COMPARE

        public static bool
        SameText (this string me, string other) {

            bool ret = me.Safe().ToLower().Trim() == other.Safe().ToLower().Trim();
            return ret;
        }

        #endregion

        #region SPLIT

        public static string[] SafeSplit( this string str , string separator ) {

            return str.Safe().Split( 
                
                separator.ToCharArray() , 
                StringSplitOptions.RemoveEmptyEntries 

            );

        }

        #endregion

        #region GROUPS

        public static bool In( this string str , string[] group ) {

            bool ret = false;

            group.SafeForEach( item => { 

                if ( item.SameText( str.Safe() )) { 

                    ret = true;

                }

            });

            return ret;

        }


        #endregion
    }
}
