using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string gameName;
    public int timePlayed;
    public int score;
    public string timeJsonMade;
    public string end;

    public GameData(string gameName, int timePlayed, int score, System.DateTime timeJsonMade, string end)
    {
        this.gameName = gameName;
        this.timePlayed = timePlayed;
        this.score = score;
        this.timeJsonMade = timeJsonMade.ToString();
        this.end = end;
    }
}
