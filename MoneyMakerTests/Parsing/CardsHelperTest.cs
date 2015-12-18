using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoneyMakerTests.Parsing
{
    [TestClass]
    public class CardsHelperTest
    {
        [TestMethod]
        public void ConvertByteCardToString()
        {
            var byteCard = (byte)Card._Ac;
            var stringCard = byteCard.ConvertByteCardToString();
            Assert.AreEqual(stringCard,"Ac");
        }

        [TestMethod]
        public void ConvertByteCardsToString()
        {
            var byteCardsArray = new byte[]
            {
                (byte) Card._Ac,
                (byte) Card._Kd
            };
            var stringCards = byteCardsArray.ConvertByteCardsToString();
            Assert.AreEqual(stringCards, "Ac,Kd");
        }

        [TestMethod]
        public void ConvertStringCardToByte()
        {
            const string stringCard = "Ac";
            var byteCard = stringCard.ConvertStringCardToByte();
            Assert.AreEqual(byteCard,(byte)Card._Ac);
        }

    }
}
