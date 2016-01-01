using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Configuration;

namespace HandHistories.Parser.MoneyMaker.EntityFramework
{
    /// <summary>
    /// Ф:Инициализатор БД Entity Framework. Кроме этого выполнят парсинг рук, а также использует методики для
    /// повышения быстродействия заполнения и сохранения контекста большим обемом данных.
    /// </summary>
    public class HandHistoryInitializer : CreateDatabaseIfNotExists<HandHistoryContext>
    {
        private Stopwatch _stopwatch;

        #region Config params
        private readonly int _commitCount;
        private readonly string _handHistoryFloder;
        private readonly bool _manualInitializing ;
        #endregion

        public HandHistoryInitializer()
        {
            _handHistoryFloder = SettingsConfig.GetConfig("HandHistoryFolder");
            _commitCount = Int32.Parse(SettingsConfig.GetConfig("CommitCount"));
            _manualInitializing = Boolean.Parse(SettingsConfig.GetConfig("IsManualInitializing"));
            _stopwatch = new Stopwatch();
        }

        protected override void Seed(HandHistoryContext context)
        {
            var dirInfo = new DirectoryInfo(_handHistoryFloder);
            var files = dirInfo.GetFiles();
            var allText = ReadAllFilesIntoOneText(files);
            _stopwatch.Start();
            if (_manualInitializing)
            {
                ManualSeed(context);
            }
            else
            {
                HandHistoryTextSeed(context, allText);
            }
            Debug.WriteLine("Time for adding and saving context:{0}", Math.Round(_stopwatch.Elapsed.TotalSeconds, 2));
            _stopwatch.Reset();
            base.Seed(context);
        }

        private  void ManualSeed(HandHistoryContext context)
        {
            var game = new Game
            {
                GameNumber = 3859038,
                DateOfHand = DateTime.Now,
                TableName = "Zagdidi",
                SeatType = SeatType._FullRing_9Handed,
                GameType = GameType.NoLimitHoldem,
                ButtonPosition = 1,
                NumberOfPlayers = 4,
                //не соответствует парсерным данным
                HandText = @" *** 27 12 2014 18:17:16
                            Table Zugdidi 9 Max (Real Money)
                            Seat 10 is the button
                            Total number of players : 9
                            Seat 1: mattymitch69 ( $1.18 )
                            Seat 2: VipNeborak ( $2 )
                            Seat 3: henriquepp ( $4.86 )
                            Seat 4: schabo24 ( $1.94 )
                            Seat 5: hen66ry ( $1.38 )
                            Seat 6: can4a ( $0.61 )
                            Seat 7: ceregapityx ( $3.03 )
                            Seat 9: dicko2206 ( $2.07 )
                            Seat 10: Llollopup ( $2.27 )
                            mattymitch69 posts small blind [$0.01]
                            VipNeborak posts big blind [$0.02]
                            ** Dealing down cards **
                            Dealt to VipNeborak [ Jc, Qd ]
                            henriquepp folds
                            schabo24 calls [$0.02]
                            hen66ry folds
                            can4a folds
                            ceregapityx folds
                            dicko2206 raises [$0.10]
                            Llollopup folds
                            mattymitch69 folds
                            VipNeborak folds
                            schabo24 folds
                            ** Summary **
                            dicko2206 did not show his hand
                            dicko2206 collected [ $0.07 ]",
                Hero = "VipNeborak",
                PlayerHistories = new List<PlayerHistory>(),
                HandActions = new List<HandAction>()
            };
            var playerHistories = new List<PlayerHistory>
            {
                new PlayerHistory
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    PlayerName = "mattymitch69",
                    StartingStack = (decimal) 1.18,
                    SeatNumber = 1
                },
                new PlayerHistory
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    PlayerName = "VipNeborak",
                    HoleCards = new[] {(byte) Card._Jc, (byte) Card._Qd},
                    StartingStack = (decimal) 2.00,
                    SeatNumber = 2
                },
                new PlayerHistory
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    PlayerName = "henriquepp",
                    StartingStack = (decimal) 4.86,
                    SeatNumber = 3
                },
                new PlayerHistory
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    PlayerName = "can4a",
                    StartingStack = (decimal) 0.81,
                    SeatNumber = 4
                }
            };
            game.PlayerHistories.AddRange(playerHistories);

            var preflophandAcitons = new List<HandAction>
            {
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    HandActionType = HandActionType.DEALT_HERO_CARDS,
                    Source = "VipNeborak",
                    Street = Street.Preflop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "VipNeborak",
                    HandActionType = HandActionType.SMALL_BLIND,
                    Amount = (decimal) 0.01,
                    Street = Street.Preflop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "henriquepp",
                    HandActionType = HandActionType.BIG_BLIND,
                    Amount = (decimal) 0.02,
                    Street = Street.Preflop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "can4a",
                    HandActionType = HandActionType.FOLD,
                    Street = Street.Preflop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "mattymitch69",
                    HandActionType = HandActionType.CALL,
                    Amount = (decimal) 0.02,
                    Street = Street.Preflop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "VipNeborak",
                    HandActionType = HandActionType.CALL,
                    Amount = (decimal) 0.01,
                    Street = Street.Preflop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "henriquepp",
                    HandActionType = HandActionType.CHECK,
                    Street = Street.Preflop
                }
            };
            game.HandActions.AddRange(preflophandAcitons);
            game.BoardCards = new[]
            {
                (byte) Card._2d,
                (byte) Card._Jh,
                (byte) Card._Ts
            };
            var flopHandActions = new List<HandAction>
            {
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "VipNeborak",
                    HandActionType = HandActionType.BET,
                    Amount = (decimal) 0.04,
                    Street = Street.Flop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "henriquepp",
                    HandActionType = HandActionType.FOLD,
                    Street = Street.Flop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "mattymitch69",
                    HandActionType = HandActionType.RAISE,
                    Amount = (decimal) 0.20,
                    Street = Street.Flop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "VipNeborak",
                    HandActionType = HandActionType.FOLD,
                    Street = Street.Flop
                },
                new HandAction
                {
                    Game = game,
                    GameNumber = game.GameNumber,
                    Source = "mattymitch69",
                    HandActionType = HandActionType.WINS,
                    Amount = (decimal) 0.30,
                    Street = Street.Flop
                }
            };
            game.HandActions.AddRange(flopHandActions);
            context.Games.Add(game);
            context.SaveChanges();
        }

        private  void HandHistoryTextSeed(DbContext context, string allText)
        {
            IHandHistoryParser parser=new Poker888CashParser();
            var games = parser.ParseGames(allText);
            try
            {
                int count = 0;
                foreach (var game in games)
                {
                    count++;
                    context = HighSpeedAdd(context,game, count, _commitCount, true);
                }
                context.SaveChanges();
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }
        }

        private DbContext HighSpeedAdd(DbContext context, Game game, int count, int commitCount, bool recreateContext)
        {
            ((HandHistoryContext) context).Games.Add(game);
            if (count % commitCount == 0)
            {
                context.SaveChanges();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new HandHistoryContext();
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                }
            }
            return context;
        }

        private string ReadAllFilesIntoOneText(IEnumerable<FileInfo> files)
        {
            var builder = new StringBuilder();
            foreach (var fs in files.Select(fileInfo => fileInfo.OpenRead()))
            {
                using (var reader = new StreamReader(fs, Encoding.Default))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        builder.AppendLine(line);
                    }
                }
                builder.Append("\n\n");
                fs.Close();
            }
            return builder.ToString();
        }
    }
}
