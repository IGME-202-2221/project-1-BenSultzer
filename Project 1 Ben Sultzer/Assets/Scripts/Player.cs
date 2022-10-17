using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Create a variable to store the player's score
    private int score;

    // Create a variable to store the current hull level of
    // the given ship
    private int hullLevel;

    // Create a variable to store how many times the player has been
    // hit
    private int numHits;

    /// <summary>
    /// Property for getting the current hull level
    /// of the given ship
    /// </summary>
    public int HullLevel
    {
        get
        {
            return hullLevel;
        }

        set
        {
            hullLevel = value;
        }
    }

    /// <summary>
    /// Property for getting and setting the score of 
    /// the player
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    /// <summary>
    /// Property for getting and setting the number
    /// of times the player has been hit
    /// </summary>
    public int NumHits
    {
        get
        {
            return numHits;
        }

        set
        {
            numHits = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hullLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
