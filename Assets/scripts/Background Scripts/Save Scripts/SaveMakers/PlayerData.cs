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
    public float INV;
    public int ML;
    public int G;

    public string armor;
    public string weapon;

    public List<string> inventory;

    // Constructor to initialize everything to proper defaults
    public PlayerData()
    {
        LV = 1; //Love
        EXP = 0; //EXecution Points
        maxHP = 20; //Max Healthpoints
        currentHP = 20; //Current Healthpoints
        ATK = 0; //Attack
        DEF = 0; //Defense
        SPD = 5; //Speed
        INV = 20; //Invulnerable
        ML = 0; //Murder Level
        G = 0; //Gold
        armor = "None";
        weapon = "None";

        inventory = new List<string>();
        for (int i = 0; i < 8; i++)
        {
            inventory.Add("None");
        }
    }
}
