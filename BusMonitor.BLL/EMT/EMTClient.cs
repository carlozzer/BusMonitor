using BusMonitor.BLL.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor.BLL.EMT
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
            /*
            "label": "C1",
    "nameB": "SENTIDO 2",
    "line": "068"
  },
  {
    "startDate": "24/06/2019",
    "group": "110",
    "nameA": "SENTIDO 1",
    "endDate": "31/12/2999",
    "label": "C2",
    "nameB": "SENTIDO 2",
    "line": "069"
    */
            return ret;

        }

        public int TimeArrivalBus ( string stop , string line , string token ) {

            int ret = 999999;

            string url = $"https://openapi.emtmadrid.es/v1/transport/busemtmad/stops/{stop}/arrives/all/";

            var request = new RestRequest( url , Method.POST );
            request.AddHeader("accessToken", token);
            request.AddParameter("undefined", "{\n      \"statistics\":\"N\",\n      \"cultureInfo\":\"EN\",\n      \"Text_StopRequired_YN\":\"Y\",\n      \"Text_EstimationsRequired_YN\":\"Y\",\n      \"Text_IncidencesRequired_YN\":\"Y\",\n      \"DateTime_Referenced_Incidencies_YYYYMMDD\":\"20190702\"\n}", ParameterType.RequestBody);

            // Seconds to arrive bus (999999 = more than 20 minutes).

            IRestResponse response = _client.Execute(request);
            dynamic json = Parse(response.Content);

            foreach ( var arrive in json.data[0].Arrive ) {

                if ( line == (string)arrive.geometry.line)
                {
                    ret = (int)arrive.geometry.estimateArrive;
                }

            }

            return ret;

        }


    }
}
