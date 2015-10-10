namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// Ф:Перечисления всех улиц игры
    /// </summary>
    public enum Street : byte
    {
        Preflop = 1,
        Flop = 2,
        Turn = 3,
        River = 4,
        Showdown = 5,
        Null = 0
    }
}