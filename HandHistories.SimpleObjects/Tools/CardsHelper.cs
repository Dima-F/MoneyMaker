using System;
using System.Text;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.SimpleObjects.Tools
{
    /// <summary>
    /// Ф:Класс методов разширения для переобразования строковых представлений карт в битовые и наоборот.
    /// </summary>
    public static class CardsHelper
    {
        public static byte ConvertStringCardToByte(this string card)
        {
            Rank rank;
            Suit suit;
            switch (card[0].ToString().ToLower())
            {
                case "2":
                    rank = Rank._2;
                    break;
                case "3":
                    rank = Rank._3;
                    break;
                case "4":
                    rank = Rank._4;
                    break;
                case "5":
                    rank = Rank._5;
                    break;
                case "6":
                    rank = Rank._6;
                    break;
                case "7":
                    rank = Rank._7;
                    break;
                case "8":
                    rank = Rank._8;
                    break;
                case "9":
                    rank = Rank._9;
                    break;
                case "t":
                    rank = Rank._T;
                    break;
                case "j":
                    rank = Rank._J;
                    break;
                case "q":
                    rank = Rank._Q;
                    break;
                case "k":
                    rank = Rank._K;
                    break;
                case "a":
                    rank = Rank._A;
                    break;
                default:
                    throw new Exception("Exception! Rank of the card is empty");
            }
            switch (card[1].ToString().ToLower())
            {
                case "d":
                    suit = Suit.Diamonds;
                    break;
                case "s":
                    suit = Suit.Spades;
                    break;
                case "c":
                    suit = Suit.Clubs;
                    break;
                case "h":
                    suit = Suit.Hearts;
                    break;
                default:
                    throw new Exception("Exception! Suit of the card is empty");
            }
            return (byte)((byte)suit | (byte)rank);
        }

        public static string ConvertByteCardToString(this byte byteCard)
        {
            var card = (Card)byteCard;
            return Enum.GetName(typeof (Card), card).Replace('_', ' ').Trim();
        }

        public static string ConvertByteCardsToString(this byte[] byteCards)
        {
            var b = new StringBuilder();
            foreach (var byteCard in byteCards)
            {
                b.Append(byteCard.ConvertByteCardToString());
            }
            return b.ToString().Insert(2, ",");
        }

        public static void InitializeNewCards(this byte[] oldCards, byte[] newCards)
        {
            if(oldCards.Length<newCards.Length)
                throw new ArgumentOutOfRangeException("Board cards must be more than flop cards");
            for (var i = 0; i < newCards.Length; i++)
            {
                oldCards[i] = newCards[i];
            }
        }
    }
}
