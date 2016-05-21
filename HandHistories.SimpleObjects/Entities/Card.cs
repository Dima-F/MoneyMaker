namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// Ф:Перечисление битовых мастей
    /// </summary>
    public enum Suit : byte
    {
        Unknown = 0,
        Clubs = 0x10,
        Diamonds = 0x20,
        Hearts = 0x30,
        Spades = 0x40,
    }

    /// <summary>
    /// Ф:Перечисление битовых рангов
    /// </summary>
    public enum Rank : byte
    {
        Unknown = 0,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _T = 0xA,
        _J = 0xB,
        _Q = 0xC,
        _K = 0xD,
        _A = 0xE,
    }

    /// <summary>
    /// Ф:Перечисление битовых карт
    /// </summary>

    public enum Card : byte
    {
        Unknown = 0,

        _2c = Suit.Clubs | Rank._2,

        _3c = Suit.Clubs | Rank._3,

        _4c = Suit.Clubs | Rank._4,

        _5c = Suit.Clubs | Rank._5,

        _6c = Suit.Clubs | Rank._6,

        _7c = Suit.Clubs | Rank._7,

        _8c = Suit.Clubs | Rank._8,

        _9c = Suit.Clubs | Rank._9,

        _Tc = Suit.Clubs | Rank._T,

        _Jc = Suit.Clubs | Rank._J,

        _Qc = Suit.Clubs | Rank._Q,

        _Kc = Suit.Clubs | Rank._K,

        _Ac = Suit.Clubs | Rank._A,

        _2s = Suit.Spades | Rank._2,

        _3s = Suit.Spades | Rank._3,

        _4s = Suit.Spades | Rank._4,

        _5s = Suit.Spades | Rank._5,

        _6s = Suit.Spades | Rank._6,

        _7s = Suit.Spades | Rank._7,

        _8s = Suit.Spades | Rank._8,

        _9s = Suit.Spades | Rank._9,

        _Ts = Suit.Spades | Rank._T,

        _Js = Suit.Spades | Rank._J,

        _Qs = Suit.Spades | Rank._Q,

        _Ks = Suit.Spades | Rank._K,

        _As = Suit.Spades | Rank._A,

        _2h = Suit.Hearts | Rank._2,

        _3h = Suit.Hearts | Rank._3,

        _4h = Suit.Hearts | Rank._4,

        _5h = Suit.Hearts | Rank._5,

        _6h = Suit.Hearts | Rank._6,

        _7h = Suit.Hearts | Rank._7,

        _8h = Suit.Hearts | Rank._8,

        _9h = Suit.Hearts | Rank._9,

        _Th = Suit.Hearts | Rank._T,

        _Jh = Suit.Hearts | Rank._J,

        _Qh = Suit.Hearts | Rank._Q,

        _Kh = Suit.Hearts | Rank._K,

        _Ah = Suit.Hearts | Rank._A,

        _2d = Suit.Diamonds | Rank._2,

        _3d = Suit.Diamonds | Rank._3,

        _4d = Suit.Diamonds | Rank._4,

        _5d = Suit.Diamonds | Rank._5,

        _6d = Suit.Diamonds | Rank._6,

        _7d = Suit.Diamonds | Rank._7,

        _8d = Suit.Diamonds | Rank._8,

        _9d = Suit.Diamonds | Rank._9,

        _Td = Suit.Diamonds | Rank._T,

        _Jd = Suit.Diamonds | Rank._J,

        _Qd = Suit.Diamonds | Rank._Q,

        _Kd = Suit.Diamonds | Rank._K,

        _Ad = Suit.Diamonds | Rank._A,
    }
}
