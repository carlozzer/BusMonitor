using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor
{
    public class EMTClient
    {
        RestClient _client;
        public EMTClient() {

             _client = new RestClient();

        }

        public string Login(string email, string password) {

            string ret = string.Empty;

            //Connect with API
            
            var request = new RestRequest("https://openapi.emtmadrid.es/v1/mobilitylabs/user/login/",Method.GET);

            request.AddHeader( "password" , password );
            request.AddHeader( "email"    , email    );

            IRestResponse response = _client.Execute(request);
            JObject json = JObject.Parse ( response.Content );
            ret = (string)json["data"][0]["accessToken"];

            return ret;
        }

        

        public string Lines ( string token)
        {

            string ret = string.Empty;

            var request = new RestRequest($"https://openapi.emtmadrid.es/v1/transport/busemtmad/lines/info/{DateTime.Now.ToString("yyyyMMdd")}/", Method.GET);

            request.AddHeader("accessToken", token);

            IRestResponse response = _client.Execute(request);
            JObject json = JObject.Parse(response.Content);
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

        public string TimeArrivalBus ( string stop , string line , string token ) {

            string ret = string.Empty;

            string url = $"https://openapi.emtmadrid.es/v1/transport/busemtmad/stops/{stop}/arrives/{line}/"; 

            var request = new RestRequest( url , Method.POST );
            request.AddHeader("accessToken", token);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody( new { 
                statistics = "N", 
                cultureInfo = "ES", 
                Text_StopRequired_YN="Y", 
                Text_EstimationsRequired_YN="Y", 
                Text_IncidencesRequired_YN="Y", 
                DateTime_Referenced_Incidencies_YYYYMMDD=DateTime.Now.ToString("yyyyMMDD") 
            }); 

            //request.AddParameter ( "Body" , "{  \"statistics\":\"N\", \"cultureInfo\":\"ES\",\n      \"Text_StopRequired_YN\":\"Y\",\n      \"Text_EstimationsRequired_YN\":\"Y\",\n      \"Text_IncidencesRequired_YN\":\"Y\",\n      \"DateTime_Referenced_Incidencies_YYYYMMDD\":\"" + DateTime.Now.ToString("yyyyMMDD") + "\"\n}", ParameterType.RequestBody);

            IRestResponse response = _client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            return ret;

        }


    }
}
