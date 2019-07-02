using BusMonitor.BLL.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] RemoveAtStart ( this byte[] buffer , int num_bytes ) {

            byte[] ret = null;

            if ( buffer != null && buffer.Length > num_bytes ) { 

                int clean_length = buffer.Length - num_bytes;
                ret = new byte[clean_length];
                Array.Copy ( buffer , 2 , ret , 0 , clean_length );

            }

            return ret;
        }

        public static byte[] Chop ( this byte[] buffer , int length ) { 

            byte[] ret = null;

            if ( buffer != null && length > 0 ) {

                Template.Try ( () => {

                    if ( buffer.Length > length ) {

                        ret = new byte[length];
                        Array.Copy( buffer , 0 , ret , 0 , length );

                    } else { 

                        ret = buffer;
                    }

                });

            }

            return ret;
        }

        //public static byte[] FromStream(ToByteArray(Stream input)
        //{
        //    byte[] ret = null;

        //    if (input != null)
        //    {
        //        input.Position = 0;
        //        byte[] buffer = new byte[16 * 1024];
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            int read;
        //            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                ms.Write(buffer, 0, read);
        //            }
        //            ret = ms.ToArray();
        //        }
        //    }

        //    return ret;
        //}
    }
}
