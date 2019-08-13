using BusMonitor.BLL.AEMET;
using BusMonitor.BLL.EMT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            EMTClient emt = new EMTClient();

            // Act
            string token = emt.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");
            //emt.TimeArrivalBus()

            // assert
            Assert.IsTrue(false);

        }
    }
}
