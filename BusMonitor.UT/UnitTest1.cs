using BusMonitor.BLL.Clients;
using BusMonitor.BLL.Tables;
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
            TimeTable model = TablesBLL.ModelWithToken( "navas" );
            

            // Act
            model = TablesBLL.ArrivalTimes( "navas" , model.EMTToken );//emt.TimeArrivalBus()

            // assert
            Assert.IsTrue(false);

        }
    }
}
