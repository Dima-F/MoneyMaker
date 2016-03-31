using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;
using HandHistories.SimpleParser.Poker888;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoneyMakerTests.Parsing
{
    [TestClass]
    public class Poker888ParserTest
    {
        private  string _inputText;

        private IHandHistoryParser _parser;

        private  List<Game> _games; 

        [TestInitialize]
        public void Initialize()
        {
            _parser = new Poker888CashParser();
            _inputText = @"#Game No : 431598730
***** 888poker Hand History for Game 431598730 *****
0,01$/0,02$ Blinds No Limit Holdem - *** 15 12 2015 22:57:07
Table Canberra 9 Max (Real Money)
Seat 2 is the button
Total number of players : 7
Seat 1: DriveRaver ( 0,79$ )
Seat 2: newlad1972 ( 2,20$ )
Seat 3: dilasr ( 4,98$ )
Seat 4: suzie235 ( 0,43$ )
Seat 5: denisakov80 ( 2,45$ )
Seat 6: VipNeborak ( 1,20$ )
Seat 10: Hohoho274 ( 0,39$ )
dilasr posts small blind [0,01$]
suzie235 posts big blind [0,02$]
VipNeborak posts big blind [0,02$]
** Dealing down cards **
Dealt to VipNeborak [ 3d, 9d ]
denisakov80 folds
VipNeborak checks
Hohoho274 calls [0,02$]
DriveRaver folds
newlad1972 folds
dilasr folds
suzie235 checks
** Dealing flop ** [ Kh, 4d, 7c ]
suzie235 bets [0,02$]
VipNeborak folds
Hohoho274 folds
** Summary **
suzie235 collected [ 0,07$ ]



#Game No : 431598815
***** 888poker Hand History for Game 431598815 *****
0,01$/0,02$ Blinds No Limit Holdem - *** 15 12 2015 22:57:52
Table Canberra 9 Max (Real Money)
Seat 3 is the button
Total number of players : 6
Seat 1: DriveRaver ( 0,79$ )
Seat 2: newlad1972 ( 2,20$ )
Seat 3: dilasr ( 4,97$ )
Seat 4: suzie235 ( 0,48$ )
Seat 6: VipNeborak ( 1,18$ )
Seat 10: Hohoho274 ( 0,37$ )
suzie235 posts small blind [0,01$]
VipNeborak posts big blind [0,02$]
** Dealing down cards **
Dealt to VipNeborak [ 5h, 4c ]
Hohoho274 folds
DriveRaver folds
newlad1972 folds
dilasr raises [0,05$]
suzie235 folds
VipNeborak folds
** Summary **
dilasr collected [ 0,05$ ]";
            _games = _parser.ParseGames(_inputText);
        }

        [TestMethod]
        public void TestGamesCount()
        {
            Assert.AreEqual(_games.Count,2);
        }

        [TestMethod]
        public void TestPlayerHistoriesCount()
        {
            Assert.AreEqual(_games[0].PlayerHistories.Count, 7);
        }

        [TestMethod]
        public void ParseFirstPlayer()
        {
            Assert.AreEqual(_games[0].PlayerHistories.First().PlayerName, "DriveRaver");
        }

        [TestMethod]
        public void ParseLastPlayer()
        {
            Assert.AreEqual(_games[1].PlayerHistories.Last().PlayerName, "Hohoho274");
        }
        
        [TestMethod]
        public void TestHandActionsCount()
        {
            Assert.AreEqual(_games[1].HandActions.Count, 11);
        }

    }
}
