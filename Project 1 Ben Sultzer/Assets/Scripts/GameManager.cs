using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Create a public facing variable to 
    // store the player
    [SerializeField]
    GameObject player;

    // Create a public-facing variable for storing 
    // the collision manager
    [SerializeField]
    GameObject collisionManager;

    // Create a variable for storing the CollisionManager
    // script on the Collision Manager GameObject
    private CollisionManager collManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        collManagerScript = collisionManager.GetComponent<CollisionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead, reset the game
        if (collManagerScript.PlayerDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // If the player has reached the correct amount of points and 
        // hull level 2, the win!
        if (player.GetComponent<Player>().HullLevel == 2 && 
            player.GetComponent<Player>().Score > 25000)
        {
            // Load the scene with the winning screen
            SceneManager.LoadScene("WinScreen");
        }
    }
}
