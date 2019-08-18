using BusMonitor.BLL.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor.BLL.Clients
{
    public class EMTClient
    {
        RestClient _client;
        public EMTClient() {

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

        public string Login(string email, string password) {

            string ret = string.Empty;

            //Connect with API
            
            var request = new RestRequest("https://openapi.emtmadrid.es/v1/mobilitylabs/user/login/",Method.GET);

            request.AddHeader( "password" , password );
            request.AddHeader( "email"    , email    );

            IRestResponse response = _client.Execute(request);
            dynamic json = Parse ( response.Content );
            ret = json.data[0].accessToken;

            return ret;
        }

        

        public string Lines ( string token)
        {

            string ret = string.Empty;

            var request = new RestRequest($"https://openapi.emtmadrid.es/v1/transport/busemtmad/lines/info/{DateTime.Now.ToString("yyyyMMdd")}/", Method.GET);

            request.AddHeader("accessToken", token);

            IRestResponse response = _client.Execute(request);
            dynamic json = Parse(response.Content);
            
            return ret;

        }

        #region TIME ARRIVAL

        IRestResponse BusStopRequest ( string stop , string token ) {

            string url = $"https://openapi.emtmadrid.es/v1/transport/busemtmad/stops/{stop}/arrives/all/";

            var request = new RestRequest( url , Method.POST );
            request.AddHeader("accessToken", token);
            
            request.AddJsonBody(new {

                statistics = "N",
                cultureInfo = "EN",
                Text_StopRequired_YN = "Y",
                Text_EstimationsRequired_YN = "Y",
                Text_IncidencesRequired_YN = "Y",
                DateTime_Referenced_Incidencies_YYYYMMDD = DateTime.Now.ToString("yyyyMMdd")
                
            });
            
            IRestResponse response = _client.Execute(request);
            return response;
        }

        public int TimeArrivalBus ( string stop , string line , string token ) {

            // Seconds to arrive bus (999999 = more than 20 minutes).
            int ret = 999999;

            IRestResponse response = BusStopRequest ( stop , token );
            dynamic json = Parse ( response.Content );

            Console.Out.WriteLine( response.Content );

            var datos = json.data[0];

            bool go_on = datos != null;
                 go_on = go_on && ( datos.GetType().IsArray ? datos.Length > 0 : true );

            if ( go_on ) {

                // No está recorriendo todas las líneas que hay en datos
                foreach ( var arrive in datos.Arrive ) {

                    Console.Out.WriteLine( $"Checking line {arrive.line}" );

                    if ( line == (string) arrive.line ) {

                        ret = (int)arrive.geometry.estimateArrive;

                    }

                }
            }

            return ret;

        }

        public Dictionary<string,int> TimeArrivalBus ( string stop , string[] lines , string token ) {

            Dictionary<string,int> ret = new Dictionary<string, int>();

            IRestResponse response = BusStopRequest ( stop , token );
            dynamic json = Parse(response.Content);

            Console.Out.WriteLine( response.Content );

            var datos = json.data[0];

            bool go_on = datos != null;
                 go_on = go_on && ( datos.GetType().IsArray ? datos.Length > 0 : true );

            if ( go_on ) {

                foreach ( var arrive in datos.Arrive ) {

                    string current_line = (string) arrive.line;
                    bool match_line = Array.IndexOf( lines , current_line ) > -1; 
                    if ( match_line  ) {

                        int secs = (int) arrive.estimateArrive;

                        if ( secs != 999999 ) {

                            ret.Add( current_line , secs );
                        }

                    }

                }
            }

            return ret;

        }

        #endregion


        

    }
}
