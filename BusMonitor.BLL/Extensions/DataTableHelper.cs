using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.IO;
using BusMonitor.BLL.Templating;

namespace BusMonitor.BLL.Extensions
{
    public class DataTableHelper
    {
        #region ORDER

        public static System.Data.DataTable Order(System.Data.DataTable DtDisorder, string order)
        {
            System.Data.DataTable ret = DtDisorder;

            try
            {
                if (!string.IsNullOrWhiteSpace(order))
                {
                    System.Data.DataView dv = DtDisorder.DefaultView;
                    dv.Sort = order.ToLower().Trim();
                    ret = dv.ToTable();
                }
            }
            catch //(Exception ex)
            {
                //ex.Message
                //ExceptionHelper.Manage(ex);
            }

            return ret;
        }
        
        #endregion

        #region XML

        public static string ToXml(System.Data.DataTable dt, string RootElement, string ItemElement)
        {
            string ret = string.Empty;

            bool ok = dt != null && !string.IsNullOrEmpty(RootElement) && !string.IsNullOrEmpty(ItemElement);

            if (ok)
            {
                // prepare the supplied DataTable  
                if (dt != null) dt.TableName = ItemElement;
                System.Data.DataSet ds = new System.Data.DataSet(RootElement);
                if (dt != null) ds.Tables.Add(dt);
                // XML con atributos, no elementos
                if (ds.Tables.Count > 0)
                {
                    foreach (System.Data.DataColumn column in ds.Tables[0].Columns)
                    {
                        column.ColumnMapping = System.Data.MappingType.Attribute;
                    }
                }
                ret = ds.GetXml();
            }

            return ret;
        }

        public static System.Data.DataTable
        FromXml ( string xml ) {

            System.Data.DataTable ret = null;

            if ( xml.IsNotEmpty() ) {

                using (StringReader reader = new StringReader( xml ) ) {

                    DataSet ds = new DataSet();

                    Template.Try( () => {

                        ds.ReadXml(reader);
                        if (ds.Tables != null && ds.Tables.Count > 0) {
                            ret = ds.Tables[0];
                        }

                    });

                }

            }

            return ret;
        }
        
        #endregion

    }
}
