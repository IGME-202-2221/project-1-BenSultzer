using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Create a variable to store the player's score
    [SerializeField]
    private int score;

    // Create a variable to store the current hull level of
    // the given ship
    private int hullLevel;

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
