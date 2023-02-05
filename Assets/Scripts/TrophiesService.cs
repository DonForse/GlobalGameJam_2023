using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public static class TrophiesService
{
    private const int WinTrophiesAmount = 3;
    private static readonly Dictionary<PlayerEnum, int> Trophies = new();

    public static UnityEvent<PlayerEnum> SomeoneWon = new();
    public static void AddTrophy(Player player)
    {
        var playerEnum = player.IsOpponent ? PlayerEnum.Npc : PlayerEnum.Player;
        if (!Trophies.ContainsKey(playerEnum))
            Trophies.Add(playerEnum, 0);
        Trophies[playerEnum]++;
        if (Trophies.Any(x => x.Value >= WinTrophiesAmount))
            SomeoneWon.Invoke(playerEnum);
    }
}