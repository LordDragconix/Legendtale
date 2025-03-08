using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int LV;
    public int EXP;
    public int maxHP;
    public int currentHP;
    public int ATK;
    public int DEF;
    public float SPD;
    public int ML;
    public int G;

    public string armor;
    public string weapon;

    public List<string> inventory;

    // Constructor to initialize everything to proper defaults
    public PlayerData()
    {
        LV = 1;
        EXP = 0;
        maxHP = 20;
        currentHP = 20;
        ATK = 0;
        DEF = 0;
        SPD = 5;
        ML = 0;
        G = 0;
        armor = "None";
        weapon = "None";

        inventory = new List<string>();
        for (int i = 0; i < 8; i++)
        {
            inventory.Add("None");
        }
    }
}
