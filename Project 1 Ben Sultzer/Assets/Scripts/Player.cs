using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Create a public-facing variable to store 
    // the upgraded player ship sprite for the
    // player
    [SerializeField]
    Sprite upgradedPlayerSprite;

    // Create a variable to store the player's score
    private int score;

    // Create a variable to store the current hull level of
    // the given ship
    private int hullLevel;

    // Create a variable to store how many times the player has been
    // hit
    private int numHits;

    // Create a List to store the collected ship parts of
    // the player
    private List<GameObject> shipParts;

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

    /// <summary>
    /// Property for getting the List of
    /// ship parts collected by the player
    /// </summary>
    public List<GameObject> ShipParts
    {
        get
        {
            return shipParts;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hullLevel = 1;
        shipParts = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
