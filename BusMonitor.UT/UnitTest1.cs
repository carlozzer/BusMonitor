using BusMonitor.BLL.Clients;
using BusMonitor.BLL.Json;
using BusMonitor.BLL.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace BusMonitor.UT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadTempTest()
        {
            // ARRAnge
            AEMETClient met = new AEMETClient();

            // Act
            met.ReadTemp();

            // assert
            Assert.IsTrue(false);

        }

        [TestMethod]
        public void ReadBusLine()
        {
            // ARRAnge
            TimeTable model = TablesBLL.ModelWithToken( "navas" );
            

            // Act
            model = TablesBLL.ArrivalTimes( "navas" , model.EMTToken );//emt.TimeArrivalBus()

            // assert
            Assert.IsTrue(false);

        }




        dynamic Parse( string json ) {

            dynamic ret = default;

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            Lexer lex = new Lexer();
            Token[] tokens = lex.ParseBytes(bytes);
            Parser par = new Parser(tokens);
            ret = par.Parse();

            return ret;
        }


        [TestMethod]
        public void JsonReadTest () {

            // Arrange
            string jsonstr = "{\"code\": \"00\", \"description\": \"Data recovered  OK, (lapsed: 639 millsecs)\", \"datetime\": \"2019-08-17T10:58:55.444142\", \"data\": [{\"Arrive\": [{\"DistanceBus\": 3269, \"line\": \"36\", \"stop\": \"878\", \"destination\": \"ATOCHA\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.76836992060355, 40.3952538060125]}, \"isHead\": \"False\", \"estimateArrive\": 284, \"positionTypeBus\": \"1\", \"bus\": \"2032\"}, {\"DistanceBus\": 5146, \"line\": \"39\", \"stop\": \"878\", \"destination\": \"PLAZA ESPA\\u00d1A\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.78525696696616, 40.3831032500272]}, \"isHead\": \"False\", \"estimateArrive\": 608, \"positionTypeBus\": \"1\", \"bus\": \"2042\"}, {\"DistanceBus\": 2970, \"line\": \"31\", \"stop\": \"878\", \"destination\": \"PLAZA MAYOR\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.75016567014969, 40.3956889746163]}, \"isHead\": \"False\", \"estimateArrive\": 660, \"positionTypeBus\": \"1\", \"bus\": \"2100\"}, {\"DistanceBus\": 3829, \"line\": \"65\", \"stop\": \"878\", \"destination\": \"BENAVENTE\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.76759403384015, 40.398099496803]}, \"isHead\": \"False\", \"estimateArrive\": 750, \"positionTypeBus\": \"1\", \"bus\": \"2108\"}, {\"DistanceBus\": 4094, \"line\": \"33\", \"stop\": \"878\", \"destination\": \"PRINCIPE PIO\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.76062873207781, 40.4036338182461]}, \"isHead\": \"False\", \"estimateArrive\": 807, \"positionTypeBus\": \"1\", \"bus\": \"4230\"}, {\"DistanceBus\": 7458, \"line\": \"39\", \"stop\": \"878\", \"destination\": \"PLAZA ESPA\\u00d1A\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.77432573230778, 40.3789158268607]}, \"isHead\": \"False\", \"estimateArrive\": 999999, \"positionTypeBus\": \"1\", \"bus\": \"2089\"}, {\"DistanceBus\": 8485, \"line\": \"36\", \"stop\": \"878\", \"destination\": \"ATOCHA\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.73300976540884, 40.411908249594]}, \"isHead\": \"False\", \"estimateArrive\": 999999, \"positionTypeBus\": \"1\", \"bus\": \"2034\"}, {\"DistanceBus\": 5493, \"line\": \"31\", \"stop\": \"878\", \"destination\": \"PLAZA MAYOR\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.75991573006624, 40.3859313194445]}, \"isHead\": \"False\", \"estimateArrive\": 999999, \"positionTypeBus\": \"1\", \"bus\": \"2093\"}, {\"DistanceBus\": 8658, \"line\": \"33\", \"stop\": \"878\", \"destination\": \"PRINCIPE PIO\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.72998848068588, 40.4130391754875]}, \"isHead\": \"False\", \"estimateArrive\": 999999, \"positionTypeBus\": \"1\", \"bus\": \"4235\"}, {\"DistanceBus\": 10478, \"line\": \"65\", \"stop\": \"878\", \"destination\": \"BENAVENTE\", \"geometry\": {\"type\": \"Point\", \"coordinates\": [-3.74777143296318, 40.4065297597203]}, \"isHead\": \"False\", \"estimateArrive\": 999999, \"positionTypeBus\": \"1\", \"bus\": \"2106\"}], \"StopInfo\": [{\"Label\": \"878\", \"Description\": \"Paseo de Extremadura-Alb\\u00e9niz\", \"Direction\": \"P\\u00ba de Extremadura, 125\", \"StopLines\": {\"Data\": [{\"Label\": \"31\", \"Description\": \"PLAZA MAYOR - ALUCHE\"}, {\"Label\": \"33\", \"Description\": \"PRINCIPE PIO - CASA DE CAMPO\"}, {\"Label\": \"36\", \"Description\": \"ATOCHA - CAMPAMENTO\"}, {\"Label\": \"39\", \"Description\": \"PLAZA ESPA\\u00d1A - SAN IGNACIO\"}, {\"Label\": \"65\", \"Description\": \"BENAVENTE - GRAN CAPITAN\"}, {\"Label\": \"N19\", \"Description\": \"CIBELES - SAN IGNACIO\"}]}}], \"ExtraInfo\": []}, {\"Incident\": {\"lastBuildDate\": \"17 Aug 2019 09:18:30 GMT\", \"ListaIncident\": {\"Data\": [{\"Description\": \"El 17 de agosto de 16:30 a 18:30 horas aproximadamente, las l\\u00edneas 3, 6, 26, 32, 50, 65 y M1, tendr\\u00e1n retenciones y modificaciones en sus itinerarios en Atocha, plaza Jacinto Benavente, plaza Provincia, Mayor y plaza Puerta del Sol por manifestaci\\u00f3n, seg\\u00fan su desarrollo y de acuerdo con las restricciones de tr\\u00e1fico que realice Polic\\u00eda Municipal.  Ver m\\u00e1s detalle en documento adjunto.<p><img src='http://feeds.emtmadrid.es:8082/images/00-Logo-RSS_Corporativo.png'/></p>\", \"Audio\": null}, {\"Description\": \"Desde las 9:00 horas del 12 de agosto a fin de obras, las l\\u00edneas 36 y 62, trasladan provisionalmente la parada 2313, ubicada en paseo Imperial frente al n\\u00famero 53, a la plaza Francisco Morano (unos metros antes). Ver m\\u00e1s detalle en documento adjunto.<p><img src='http://feeds.emtmadrid.es:8082/images/00-Logo-RSS_Corporativo.png'/></p>\", \"Audio\": null}, {\"Description\": \"Desde el inicio del servicio del 5 de agosto a fin de obras, las l\\u00edneas 3, 25, 39, 138, 148 y N16, tendr\\u00e1n retenciones y modificaciones en sus itinerarios en plaza Espa\\u00f1a, Ferraz y Bail\\u00e9n. Ver m\\u00e1s detalle en documento adjunto.<p><img src='http://feeds.emtmadrid.es:8082/images/00-Logo-RSS_Corporativo.png'/></p>\", \"Audio\": null}]}}}]}";


            // act
            dynamic json = Parse(jsonstr);

            // Assert


        }

    }
}
