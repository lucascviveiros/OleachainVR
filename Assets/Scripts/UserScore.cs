using System.Collections;
using UnityEngine;

public class UserScore
{
    public string name;
    public int score;

    public string Name
    {
        get {return name; }
        set { name = value; }
    }
            
    public int Score
    {
        get {return score; }
        set { score = value; }
    }

    public UserScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
