using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointSystem
{
    private static readonly Single START_MAX_HEALTH_POINT = 100;
    private int Addition = 0;
    private int HealthPoint = (int)START_MAX_HEALTH_POINT;
   

    public void Attacked(int d)
    {
        HealthPoint -= d;
        if (HealthPoint<=0)
        {
            Debug.Log("Player_is_Dad");
            HealthPoint = 0;
        }
    }
    public void Portion(int Level,int healamount)
    {
        //체력이 다참
        HealthPoint += ((int)START_MAX_HEALTH_POINT + ((Level - 1) * 20)+ Addition) / 100 * healamount;
        if (HealthPoint >= START_MAX_HEALTH_POINT + ((Level-1) * 20)+ Addition)
        {
            HealthPoint = (int)START_MAX_HEALTH_POINT + ((Level - 1) * 20) + Addition;
        }
    }
    public void LevelUp(int Level)
    {
        HealthPoint = (int)START_MAX_HEALTH_POINT + ((Level-1) * 20)+ Addition;
    }
    public int GetHp()
    {
        return HealthPoint;
    }
    public int GetAddition()
    {
        return Addition;
    }
    public void SetAddition(int add)
    {
        Addition += add;
    }
}
