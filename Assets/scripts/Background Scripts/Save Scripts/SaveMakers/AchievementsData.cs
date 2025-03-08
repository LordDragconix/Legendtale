using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementEntry
{
    public string name;
    public string description;
    public bool unlocked;

    public AchievementEntry(string name, string description)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
    }
}

[Serializable]
public class AchievementsData
{
    public List<AchievementEntry> normalAchievementsList = new List<AchievementEntry>();
    public List<AchievementEntry> funAchievementsList = new List<AchievementEntry>();
    public List<AchievementEntry> specialAchievementsList = new List<AchievementEntry>();

    // Constructor: Initializes achievements and categorizes them
    public AchievementsData()
    {
        // Normal Achievements
        normalAchievementsList.Add(new AchievementEntry("Gotta Start Somewhere", "Finish the prologue"));

        normalAchievementsList.Add(new AchievementEntry("Keeper of the ruins", "Meet Toriel"));

        // Fun Achievements (Placeholder)
        funAchievementsList.Add(new AchievementEntry("Fun Achievement Placeholder", "This is a placeholder for fun achievements."));

        // Special Achievement (Unlocks when all others are earned)
        specialAchievementsList.Add(new AchievementEntry("Legendary Player", "Obtain all achievements."));
    }
}
