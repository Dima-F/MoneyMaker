namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// Перечисление типа игры (напр. безлимитный холдем, потлимит холдем, потлимит омаха)
    /// </summary>

    public enum GameType : byte
    {
        Unknown = 0,
        NoLimitHoldem = 1,
        FixedLimitHoldem = 2,
        PotLimitOmaha = 3,
        PotLimitHoldem = 4,
        PotLimitOmahaHiLo = 5,
        CapNoLimitHoldem = 6,
        CapPotLimitOmaha = 7,
        FiveCardPotLimitOmaha = 8,
        FiveCardPotLimitOmahaHiLo = 9,
        NoLimitOmaha = 10,
        NoLimitOmahaHiLo = 11,
        FixedLimitOmaha = 12,
        FixedLimitOmahaHiLo = 13,
        Any = 31,
    }
}
