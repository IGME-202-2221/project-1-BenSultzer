using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Ben Sultzer
// Purpose: Updates the gameplay HUD as the player
// destroys enemies, upgrades their hull, and/or
// gets hit and has their hull level demoted
// Restrictions: None
public class HUDManager : MonoBehaviour
{
    // Create a public-facing variable to store
    // the player and gain access to their score
    [SerializeField]
    GameObject player;

    // Create a public-facing variable to store
    // the "SCORE" label in the HUD
    [SerializeField]
    Text scoreLabel;

    // Create a public-facing variable to store the
    // "Ship Parts" label in the HUD
    [SerializeField]
    Text shipPartsLabel;

    // Create a public-facing variable to store
    // the first player life image in the HUD
    [SerializeField]
    Image lifeImageLeft;

    // Create a public-facing variable to store
    // the second player life image in the HUD
    [SerializeField]
    Image lifeImageCenter;

    // Create a public-facing variable to store
    // the last player life image in the HUD
    [SerializeField]
    Image lifeImageRight;

    // Create a public-facing variable to store the 
    // in-game collision manager (for gaining access to
    // the number of ship parts collected by the
    // player)
    [SerializeField]
    GameObject collisionManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.text = "SCORE: 0";
        shipPartsLabel.text = "Ship Parts: 0";

    }

    // Update is called once per frame
    void Update()
    {
        // Update the "SCORE" text in the HUD to
        // reflect the player's current score
        scoreLabel.text = "SCORE: " + player.GetComponent<Player>().Score;

        // Update the "Ship Parts" text in the HUD to
        // reflect the player's current number of collected
        // ship parts
        shipPartsLabel.text = "Ship Parts: " + 
            collisionManager.GetComponent<CollisionManager>().PlayerShipParts;

        // Create a new Color that is a white
        // overlay with an Alpha of 0
        Color hideColor = new Color(255, 255, 255, 0);

        // Create a new Color that is a white 
        // overlay with an Alpha of 255 (full)
        Color showColor = new Color(255, 255, 255, 255);

        // Determine how many of the life
        // images to hide, depending on how many
        // times the player has been hit.
        if (player.GetComponent<Player>().NumHits == 1)
        {
            // Set the right-most image's color to 
            // hideColor to hide it from the HUD
            lifeImageRight.color = hideColor;
        } else if (player.GetComponent<Player>().NumHits == 2)
        {
            // Set the right-most and center images' color to 
            // hideColor to hide both from the HUD
            lifeImageRight.color = hideColor;
            lifeImageCenter.color = hideColor;
        } else if (player.GetComponent<Player>().NumHits == 3)
        {
            // Set all images' colors to hideColor to hide them
            // from the HUD
            lifeImageRight.color = hideColor;
            lifeImageCenter.color = hideColor;
            lifeImageLeft.color = hideColor;
        } else
        {
            // Reset the images' colors (the player
            // has lost all lives and the game has restart)
            lifeImageRight.color = showColor;
            lifeImageCenter.color = showColor;
            lifeImageLeft.color = showColor;
        }
    }
}
