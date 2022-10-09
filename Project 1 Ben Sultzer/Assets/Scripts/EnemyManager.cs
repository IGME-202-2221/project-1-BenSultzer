using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Ben Sultzer
// Purpose: Manages the movement and collision
// resolution of all currently active enemies
// Restrictions: None
public class EnemyManager : MonoBehaviour
{
    // Create a public facing variable to store the
    // player and gain access to their hull level
    [SerializeField]
    GameObject player;

    // Create a public facing variable to store the
    // enemy prefab
    [SerializeField]
    GameObject enemy;

    // Create a List to store all currently active
    // enemies
    private List<GameObject> enemies;

    // Create a variable to track the time between 
    // when an enemy should change direction
    private float timeBeforeDirChange;

    // Create a variable to track the time between 
    // when a new wave of enemies should spawn for the
    // current hull level
    private float timeBeforeWaveSpawn;

    // Create a variable to store the set of available 
    // movement directions
    Vector2[] movementDir;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        timeBeforeDirChange = 0f;
        timeBeforeWaveSpawn = 0f;
        movementDir = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    }

    // Update is called once per frame
    void Update()
    {
        // Assess what level the player is currently at and 
        // whether enough time has passed, to determine if a new
        // wave of enemies should be spawn
        if (player.GetComponent<ShipController>().HullLevel == 1 &&
            timeBeforeWaveSpawn > 5)
        {
            SpawnLevel1Enemies();
        } else
        {
            // Add the currently elapsed game time to the time tracker
            // before the next wave spawn
            timeBeforeWaveSpawn += Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawns the set of enemies to kill at a hull level of 1
    /// </summary>
    private void SpawnLevel1Enemies()
    {
        // Instantiate the enemy prefab
        GameObject newEnemy = Instantiate(enemy);

        // Get the current enemy's ShipController Component
        ShipController shipControlComp = newEnemy.GetComponent<ShipController>();

        // Set the current enemy's necessary movement data
        shipControlComp.VehiclePosition = new Vector3(
            Random.Range(-shipControlComp.HalfCamWidth, shipControlComp.HalfCamWidth), 
            Random.Range(-shipControlComp.HalfCamHeight, shipControlComp.HalfCamHeight));
        shipControlComp.Direction = movementDir[Random.Range(0, 4)];

        // Add the current enemy to the List of active enemies
        enemies.Add(newEnemy);
    }
}
