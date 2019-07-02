using BusMonitor.BLL.Templating;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusMonitor.BLL.Extensions
{
    public class DateHelper
    {
        public static bool EsNulaODefault(DateTime fecha)
        {
            bool ret = true;

            if (fecha != null)
            {
                DateTime defaultDate = new DateTime(1900, 1, 1, 0, 0, 0);
                ret = ((fecha == default(DateTime)) || (fecha == defaultDate));
            }

            return ret;
        }

        public static DateTime GetDate(string fecha)
        {
            DateTime ret = default(DateTime);

            try
            {
                ret = DateTime.ParseExact(fecha, "dd/MM/yyyy H:mm:ss", null);
            }
            catch (FormatException ex)
            {
                throw ex;
            }

            return ret;
        }

        public static string GetSpanishString(DateTime date)
        {
            string[] meses = new string[] { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
            return string.Format("{0} de {1} de {2}", date.Day, meses[date.Month - 1], date.Year);
        }

        public static string GetSpanishStringDayOfWeek(DateTime date)
        {
            string[] dias = new string[] { "lunes", "martes", "miercoles", "jueves", "viernes", "sabado", "domingo" };
            return string.Format("{0}, {1}", dias[((int)date.DayOfWeek) - 1], GetSpanishString(date));
        }

        public static string GetOrderFormat(DateTime fecha)
        {
            string ret = string.Empty;

            ret = string.Format("{0}{1:00}{2:00}", fecha.Year, fecha.Month, fecha.Day);

            return ret;
        }

        public static string ExcelFormat(DateTime date)
        {
            // 2014-10-21T00:00:00.000
            return string.Format("{0}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}.{6:000}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        public static Nullable<DateTime> TryParse(string date)
        {
            Nullable<DateTime> ret = null;

            if (!string.IsNullOrWhiteSpace(date))
            {
                DateTime res;
                if (DateTime.TryParse(date, out res))
                {
                    ret = res;
                }
            }

            return ret;
        }

        /// <summary>
        /// Formato dd/MM/yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Nullable<DateTime> TryParseES(string date)
        {
            Nullable<DateTime> ret = null;

            try {
                string[] parts = date.Safe().Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (parts != null && parts.Length == 3) {
                    int dia = Convert.ToInt16(parts[0].Trim());
                    int mes = Convert.ToInt16(parts[1].Trim());
                    int anno = Convert.ToInt16(parts[2].Trim());
                    ret = new DateTime(anno, mes, dia);
                }
            } catch {}

            return ret;
        }

        public static string TryFormat(string datetime, string format)
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(datetime) && !string.IsNullOrWhiteSpace(format))
            {
                try
                {
                    var dt = DateTime.Parse(datetime);
                    return dt.ToString(format);
                }
                catch
                {
                    ret = datetime;
                }
            }

            return ret;
        }

        public static DateTime 
        ParseExact ( string fecha, string format ) {

            DateTime ret = default(DateTime);

            Template.Try ( () => {
                ret = DateTime.ParseExact(fecha, format, CultureInfo.InvariantCulture);
            });

            return ret;
        }


        #region UNIX FORMAT

        public static DateTime FromUnixFormat(long fecha)
        {
            DateTime ret = default(DateTime);

            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            ret = start.AddMilliseconds(fecha);

            return ret;
        }

        public static Nullable<DateTime> FromUnixFormat(Nullable<long> fecha)
        {
            Nullable<DateTime> ret = null;

            if (fecha.HasValue)
            {
                ret = DateHelper.FromUnixFormat(fecha.Value);
            }

            return ret;
        }

        public static long ToUnixFormat(DateTime fecha)
        {
            long ret = default(long);

            ret = (long)(fecha.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))).TotalMilliseconds;

            return ret;
        }

        public static Nullable<long> ToUnixFormat(Nullable<DateTime> fecha)
        {
            Nullable<long> ret = null;

            if (fecha.HasValue)
            {
                ret = DateHelper.ToUnixFormat(fecha.Value);
            }

            return ret;
        }


        #endregion

        #region JSON

        public static string ToJson(DateTime dt)
        {
            string ret = string.Empty;

            string datestring = dt.ToUniversalTime().ToString("s") + "Z";
            ret = string.Format("'{0}'", datestring);

            return ret;
        }

        #endregion

        #region CHECK INBOUNDS

        public static bool CheckInBounds(int dia, int mes, int ano, int hora, int minuto, int segundo)
        {
            bool ok_so_far = true;

            ok_so_far = ok_so_far && mes.InBounds       (1, 12);
            ok_so_far = ok_so_far && ano.InBounds       (1, 9999);
            ok_so_far = ok_so_far && hora.InBounds      (0, 23);
            ok_so_far = ok_so_far && minuto.InBounds    (0, 59);
            ok_so_far = ok_so_far && segundo.InBounds   (0, 59);

            switch (mes)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    ok_so_far = ok_so_far && dia.InBounds(1, 31);
                    break;
                case 2:
                    ok_so_far = ok_so_far && DateTime.IsLeapYear(ano) ? dia.InBounds(1, 29) : dia.InBounds(1, 28);
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    ok_so_far = ok_so_far && dia.InBounds(1, 30);
                    break;
                default:
                    ok_so_far = false;
                    break;
            }


            return ok_so_far;
        }

        #endregion

        #region SHAREPOINT

        public static string SharePointFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static DateTime SharePointMinValue => new DateTime(1900,1,1);

        public static DateTime SharePointMaxValue => new DateTime(8900,12,31);

        #endregion

    }
}
