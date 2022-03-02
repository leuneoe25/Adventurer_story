using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin
{
    private int coin;
    public int GetCoin()
    {
        return coin;
    }
    public void SetCoin(int _coin)
    {
        coin += _coin;
    }
    public int NeedLevelUpCoin(int level)
    {
        if (level <= 10)
            return 5;
        else if (level <= 20)
            return 8;
        else if (level <= 30)
            return 10;
        else
            return 12;

    }
}
