using System;

namespace HandHistories.SimpleObjects.Entities
{
    /// <summary>
    /// Разширенное перечисление всех действий
    /// </summary>
    public enum HandActionType
    {
        FOLD,
        CALL,
        CHECK,
        ANTE,
        SHOW,
        SHOWS_FOR_LOW,
        WINS,
        WINS_THE_LOW,
        WINS_SIDE_POT,
        DEALT_HERO_CARDS,
        TIES, //ничья, вииграш части банка
        TIES_SIDE_POT, //ничья, вииграш части побочного банка
        RAISE,
        BET,
        SMALL_BLIND,
        BIG_BLIND,
        UNCALLED_BET,//it can be uncalled bet, raise or all in
        REQUEST_TIME,
        FIFTEEN_SECONDS_LEFT,
        FIVE_SECONDS_LEFT,
        MUCKS,
        POSTS,//взнос фишек в размере бб чтобы сесть за стол
        DISCONNECTED,
        RECONNECTED,
        STANDS_UP,
        SITS_DOWN,
        SITTING_OUT,
        ERROR,
        ADDS,
        TIMED_OUT,
        RETURNED,
        SECONDS_TO_RECONNECT,
        CHAT,
        FEELING_CHANGE,
        ALL_IN,
        GAME_CANCELLED,
        RABBIT,
        JACKPOTCONTRIBUTION,
        DIDNT_SHOW_HAND,
        DEALING_FLOP,
        DEALING_TURN,
        DEALING_RIVER,
        SUMMARY,
        UNKNOWN
    }
}