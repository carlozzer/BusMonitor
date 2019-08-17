using BusMonitor.BLL.Extensions;
using BusMonitor.BLL.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor.BLL.Clients
{
    public class AEMETClient
    {
        RestClient _client;
        public AEMETClient() {

             _client = new RestClient();

        }

        #region JSN

        dynamic Parse( string json ) {

            dynamic ret = default;

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            Lexer lex = new Lexer();
            Token[] tokens = lex.ParseBytes(bytes);
            Parser par = new Parser(tokens);
            ret = par.Parse();

            return ret;
        }

        #endregion


        public float ReadTemp( )
        {
            float ret = -273;

            string api_key = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJjYXJsb3p6ZXJAZ21haWwuY29tIiwianRpIjoiNTRjMjExMzQtZTNmMi00NjEzLTkwNjAtM2QyZmRkZjE1NmU2IiwiaXNzIjoiQUVNRVQiLCJpYXQiOjE1NjI4Mzg4MjksInVzZXJJZCI6IjU0YzIxMTM0LWUzZjItNDYxMy05MDYwLTNkMmZkZGYxNTZlNiIsInJvbGUiOiIifQ.SvNzq-P7HRlG2uS0xAYVgIY2jiWPqoXsMdUsJL8CctI";
            string url = $"https://opendata.aemet.es/opendata/api/observacion/convencional/todas/?api_key={api_key}";

            var request = new RestRequest ( url , Method.GET );
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = _client.Execute(request);
            dynamic json = Parse(response.Content);

            string url_datos = json.datos;
            
            if ( url_datos.IsNotEmpty() ) {

                var request_datos = new RestRequest(url_datos, Method.GET);
                request.AddHeader("cache-control", "no-cache");

                IRestResponse response_datos = _client.Execute(request_datos);
                dynamic datos = Parse ( $"{{\"datos\":{response_datos.Content}}}" );

                foreach ( var estacion in datos.datos ) { 

                    string idema = (string) estacion.idema;
                    if ( idema.Safe().SameText("3195") ) { // Madrid Retiro

                        ret = (float) estacion.ta;

                    }

                }
            }

            return ret;

        }






    }
}
