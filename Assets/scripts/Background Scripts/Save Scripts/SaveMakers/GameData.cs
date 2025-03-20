using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int FUN = 1; //Current Fun Value of the game

    public string lastSavePoint = "None"; // Last checkpoint/save point

    public int ruinsKills = 0; // How many monsters killed in ruins
}
