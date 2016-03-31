using System;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoneyMakerTests.Parsing
{
    [TestClass]
    public class RegexHelperTest
    {
        [TestMethod]
        public void FindGameNumber()
        {
            const string line = "#Game No : 428831194";
            var gameNumber = line.FindGameNumber();
            Assert.AreEqual(428831194,gameNumber);
        }

        [TestMethod]
        public void FindDateOfGame()
        {
            const string line = "0,01$/0,02$ Blinds No Limit Holdem - *** 18 11 2015 10:53:20";
            var date = line.FindDateOfGame();
            var realDate = new DateTime(2015, 11, 18, 10, 53, 20);
            Assert.AreEqual(date, realDate);
        }

        [TestMethod]
        public void FindTableName()
        {
            const string line = "Table Andria 9 Max (Real Money)";
            var tableName = line.FindTableName();
            Assert.AreEqual("Andria", tableName);
        }

        [TestMethod]
        public void FindGameType()
        {
            const string line = "0,01$/0,02$ Blinds No Limit Holdem - *** 18 11 2015 10:53:20";
            var gameType = line.FindGameType();
            Assert.AreEqual(LimitType.NoLimit, gameType);
        }

        [TestMethod]
        public void FindSeatType()
        {
            const string line = "Table Andria 9 Max (Real Money)";
            var seatType = line.FindSeatType();
            Assert.AreEqual(SeatType._FullRing_9Handed, seatType);
        }

        [TestMethod]
        public void FindButtonPosition()
        {
            const string line = "Seat 4 is the button";
            var position = line.FindButtonPosition();
            Assert.AreEqual(position, 4);
        }

        [TestMethod]
        public void FindNumberOfPlayers()
        {
            const string line = "Total number of players : 5";
            var plNum = line.FindNumberOfPlayers();
            Assert.AreEqual(plNum, 5);
        }

        [TestMethod]
        public void FindPlayerName()
        {
            const string line = "Seat 4: riba40k ( 0,44$ )";
            var name = line.FindPlayerName();
            Assert.AreEqual(name, "riba40k");
        }

        [TestMethod]
        public void FindSeatNumber()
        {
            const string line = "Seat 5: greener60 ( 0,65$ )";
            var number = line.FindSeatNumber();
            Assert.AreEqual(number, 5);
        }

        [TestMethod]
        public void FindPlayerStartingStack()
        {
            const string line = "Seat 9: speedcamel ( 5,15$ )";
            decimal stack = line.FindPlayerStartingStack();
            Assert.AreEqual(stack, 5.15m);
        }

        [TestMethod]
        public void FindActionAmountBlinds()
        {
            const string line = "speedcamel posts big blind [0,02$]";
            decimal amount = line.FindActionAmount();
            Assert.AreEqual(amount, 0.02m);
        }

        [TestMethod]
        public void FindActionAmountRaise()
        {
            const string line = "greener60 raises [0,63$]";
            decimal amount = line.FindActionAmount();
            Assert.AreEqual(amount, 0.63m);
        }

        [TestMethod]
        public void FindActionAmountRaise2()
        {
            const string line = "greener60 raises [ $0,63 ]";
            decimal amount = line.FindActionAmount();
            Assert.AreEqual(amount, 0.63m);
        }

        [TestMethod]
        public void FindActionAmountWin()
        {
            const string line = "greener60 collected [ 0,23$ ]";
            decimal amount = line.FindActionAmount();
            Assert.AreEqual(amount, 0.23m);
        }

        [TestMethod]
        public void FindActionAmountDeadBlind()
        {
            const string line = "greener60 posts dead blind [ 0,01$ + 0,02$ ]";
            decimal amount = line.FindActionAmount();
            Assert.AreEqual(amount, 0.01m + 0.02m);
        }

        [TestMethod]
        public void FindActionAmountDeadBlind2()
        {
            const string line = "greener60 posts dead blind [ $0,01 + $0,02 ]";
            decimal amount = line.FindActionAmount();
            Assert.AreEqual(amount, 0.01m + 0.02m);
        }

        [TestMethod]
        public void FindFlopCards()
        {
            const string line = "** Dealing flop ** [ 7d, 4d, Ad ]";
            var flopCards = line.FindFlopCards();
            Assert.AreEqual(flopCards[0], (byte)Card._7d);
            Assert.AreEqual(flopCards[1], (byte)Card._4d);
            Assert.AreEqual(flopCards[2], (byte)Card._Ad);
        }

        [TestMethod]
        public void FindHeroCards()
        {
            const string line = "Dealt to VipNeborak [ Kd, 5h ]";
            var heroCards = line.FindHeroCards();
            Assert.AreEqual(heroCards[0], (byte)Card._Kd);
            Assert.AreEqual(heroCards[1], (byte)Card._5h);
        }

        [TestMethod]
        public void FindTurnCard()
        {
            const string line = "** Dealing turn ** [ 3s ]";
            var turn = line.FindTurnCard();
            Assert.AreEqual(turn, (byte)Card._3s);
        }

        [TestMethod]
        public void FindRiverCard()
        {
            const string line = "** Dealing river ** [ Ts ]";
            var river = line.FindRiverCard();
            Assert.AreEqual(river, (byte)Card._Ts);
        }

        [TestMethod]
        public void FindShowdounCards()
        {
            const string line = "Porno69Star shows [ 4d, As ]";
            var showCards = line.FindShowdounCards();
            Assert.AreEqual(showCards[0], (byte)Card._4d);
            Assert.AreEqual(showCards[1], (byte)Card._As);
        }

        [TestMethod]
        public void FindMuckCards()
        {
            const string line = "ZUBR7771 mucks [ Kd, 2d ]";
            var showCards = line.FindMuckCards();
            Assert.AreEqual(showCards[0], (byte)Card._Kd);
            Assert.AreEqual(showCards[1], (byte)Card._2d);
        }

        [TestMethod]
        public void FindBadHand()
        {
            const string hand = @"#Game No : 400637826
                                ***** 888poker Hand History for Game 400637826 *****
                                $0.01/$0.02 Blinds No Limit Holdem - *** 09 02 2015 09:15:01
                                Table Ramat Gan 9 Max (Real Money)
                                Seat 6 is the button
                                Total number of players : 9
                                Seat 1:  ( $1.87 )
                                Seat 2:  ( $1.98 )
                                Seat 3:  ( $2.02 )
                                Seat 4:  ( $2.43 )
                                Seat 5:  ( $2 )
                                Seat 6:  ( $1.59 )
                                Seat 7:  ( $0.38 )
                                Seat 9:  ( $2.98 )
                                Seat 10:  ( $4.76 )
                                posts small blind [$0.01]
                                posts big blind [$0.02]
                                ** Dealing down cards **
                                Dealt to  [ Kh, Ah ]
                                folds
                                calls [$0.02]
                                folds
                                folds
                                folds
                                folds
                                raises [$0.06]
                                folds
                                folds
                                folds
                                ** Summary **
                                did not show his hand
                                collected [ $0.07 ]";
            var isBadHand = hand.FindBadHand();
            Assert.IsTrue(isBadHand);
        }

        [TestMethod]
        public void FindBadHand2()
        {
            const string hand = @"#Game No : 428831883
                                ***** 888poker Hand History for Game 428831883 *****
                                0,01$/0,02$ Blinds No Limit Holdem - *** 18 11 2015 11:11:28
                                Table Andria 9 Max (Real Money)
                                Seat 4 is the button
                                Total number of players : 7
                                Seat 1: dilasr ( 2$ )
                                Seat 2: yerlan16 ( 0,40$ )
                                Seat 4: gogy_mogy ( 0,40$ )
                                Seat 5: Porno69Star ( 1,36$ )
                                Seat 6: Romanist87 ( 0,77$ )
                                Seat 7: ussta ( 2,15$ )
                                Seat 10: VipNeborak ( 1,06$ )
                                Porno69Star posts small blind [0,01$]
                                Romanist87 posts big blind [0,02$]
                                ** Dealing down cards **
                                Dealt to VipNeborak [ As, Qd ]
                                ussta folds
                                VipNeborak raises [0,06$]
                                dilasr folds
                                yerlan16 folds
                                gogy_mogy folds
                                Porno69Star folds
                                Romanist87 folds
                                ** Summary **
                                VipNeborak collected [ 0,05$ ]";
            var isBadHand = hand.FindBadHand();
            Assert.IsFalse(isBadHand);
        }
    }
}
