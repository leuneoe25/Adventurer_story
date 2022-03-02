using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private static readonly Single START_EXPERIENCE = 20;
    private int Level = 1;
    private int Exp = 0;

    public void AddExp()
    {
        Level++;

    }

    public int GetLevel()
    {
        return Level;
    }
    public int GetExp()
    {
        return Exp;
    }
}
