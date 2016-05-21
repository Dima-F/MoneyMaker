namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// Ф:Цифровая нумерация названий позиций, а также правила "урезания" могут отличаться от общепринятых
    /// (сделано с целью упрощения вычислений статов).
    /// </summary>
    public enum PositionType:byte
    {
        SB,
        BB,
        UTG,//Early position
        UTG2,//Early position
        UTG3,//Early position
        MP,//Middle position
        MP2,//Middle position
        MP3,//Middle position
        CO,//Late position
        B//Late position
    }
}
