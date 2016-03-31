using System;
using System.Collections.Generic;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleParser.Poker888
{
    public class Poker888TournamentParser:Poker888Parser
    {
        protected override bool IsTournament
        {
            get { return true; }
        }

        public override IDictionary<string, string> GetMainInfo(string path)
        {
            throw new NotImplementedException();
        }

        protected override string FindTableName(IEnumerable<string> hand)
        {
            throw new NotImplementedException();
        }

        protected override SeatType FindSeatType(IEnumerable<string> hand)
        {
            throw new NotImplementedException();
        }
    }
}
