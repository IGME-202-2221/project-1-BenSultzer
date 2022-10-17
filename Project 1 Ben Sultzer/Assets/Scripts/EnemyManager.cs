using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Ben Sultzer
// Purpose: Manages the spawning, movement, and firing
// of all currently active enemies
// Restrictions: None
public class EnemyManager : MonoBehaviour
{
    // Create a public facing variable to store the
    // player and gain access to their hull level
    [SerializeField]
    GameObject player;

    // Create a public facing variable to store the
    // Basic Enemy prefab
    [SerializeField]
    GameObject enemy;

    // Create a public facing variable to store the
    // camera to allow each new enemy to pass that 
    // camera to their respective projectiles
    [SerializeField]
    GameObject camera;

    // Create a public-facing variable to store the
    // upgraded enemy sprite image file for an upgraded enemy
    [SerializeField]
    Sprite upgradedEnemySprite;

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

    // Create a variable to track the time between
    // an enemy firing a projectile
    private float timeBeforeFireNextProjectile;

    // Create a variable to store the set of available 
    // movement directions
    private Vector2[] movementDir;

    // Create a variable to store half the height of the camera's
    // viewport to determine the random y-position spawnpoint of 
    // an enemy
    private float halfCamHeight;

    // Create a variable to store half the width of the camera's
    // viewport to determine the random x-position spawnpoint of 
    // an enemy  
    private float halfCamWidth;

    // Create a variable to store the chance of an
    // upgraded enemy spawning at a hull level of 2
    private float upgradedEnemyChance;

    /// <summary>
    /// Property for getting the List of all active
    /// enemies
    /// </summary>
    public List<GameObject> Enemies
    {
        get
        {
            return enemies;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        timeBeforeDirChange = 0f;
        timeBeforeWaveSpawn = 0f;
        movementDir = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left,
                                      new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), 
                                      new Vector2(-1, -1)};
        halfCamHeight = camera.GetComponent<Camera>().orthographicSize;
        halfCamWidth = halfCamHeight * camera.GetComponent<Camera>().aspect;
        upgradedEnemyChance = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        // Assess what level the player is currently at and 
        // whether enough time has passed, to determine if a new
        // wave of enemies should be spawned
        if (player.GetComponent<Player>().HullLevel == 1 &&
            timeBeforeWaveSpawn > 10)
        {
            SpawnLevel1Enemies();
            timeBeforeWaveSpawn = 0f;
        } else
        {
            // Add the currently elapsed game time to the time tracker
            // before the next wave spawn
            timeBeforeWaveSpawn += Time.deltaTime;
        }

        // Assess whether enough time has passed to change the
        // movement direction of each enemy
        for (int i = 0; i < enemies.Count; i++)
        {
            if (timeBeforeDirChange > 2)
            {
                enemies[i].GetComponent<ShipController>().Direction =
                    movementDir[Random.Range(0, 8)];
                enemies[i].transform.rotation = Quaternion.LookRotation(
                    Vector3.back, enemies[i].GetComponent<ShipController>().Direction);
                timeBeforeDirChange = 0f;
            } else
            {
                // Add the currently elapsed game time to the time tracker
                // before the next direction change
                timeBeforeDirChange += Time.deltaTime;
            }
        }

        // Allow each enemy to fire after a random amount of time has
        // passed within a range of 1 to 5 seconds
        for (int i = 0; i < enemies.Count; i++)
        {
            if (timeBeforeFireNextProjectile > Random.Range(1f, 5f))
            {
                enemies[i].GetComponent<ShipController>().EnemyFire();
                timeBeforeFireNextProjectile = 0f;
            } else
            {
                // Add the currently elapsed game time to the time tracker
                // before the next weapon fire
                timeBeforeFireNextProjectile += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Spawns the set of enemies to kill at a hull level of 1
    /// </summary>
    private void SpawnLevel1Enemies()
    {
        for (int i = 0; i < 3; i++)
        {
            // Instantiate the Basic Enemy prefab
            GameObject newEnemy = Instantiate(enemy);

            // Get the current enemy's ShipController Component
            ShipController shipControlComp = newEnemy.GetComponent<ShipController>();

            // Initialize that enemy's projectile list
            shipControlComp.Projectiles = new List<GameObject>();

            // Set the current enemy's necessary movement data
            shipControlComp.VehiclePosition = new Vector3(
                Random.Range(-halfCamWidth, halfCamWidth),
                Random.Range(-halfCamHeight, halfCamHeight));
            shipControlComp.Direction = movementDir[Random.Range(0, 8)];
            newEnemy.transform.rotation = Quaternion.LookRotation(
                Vector3.back, shipControlComp.Direction);
            shipControlComp.Camera = camera;

            // Add the current enemy to the List of active enemies
            enemies.Add(newEnemy);
        }
    }

    /// <summary>
    /// Spawns the set of enemies to kill at a hull level of 2
    /// </summary>
    private void SpawnLevel2Enemies()
    {
        // Create a variable to store the probability result
        // for spawning an upgraded enemy
        float spawnUpgradedEnemy = Random.Range(0.0f, 1.0f);

        for (int i = 0; i < 5; i++)
        {
            // Generate a random number to determine if an upgraded
            // enemy should be spawned
            spawnUpgradedEnemy = Random.Range(0.0f, 1.0f);

            // Instantiate the Basic Enemy prefab
            GameObject newEnemy = Instantiate(enemy);

            // If an upgraded enemy should be spawned, change it's sprite
            // to the upgraded enemy sprite
            if (spawnUpgradedEnemy < upgradedEnemyChance)
            {
                newEnemy.GetComponent<SpriteRenderer>().sprite = upgradedEnemySprite;
            }

            // Get the current enemy's ShipController Component
            ShipController shipControlComp = newEnemy.GetComponent<ShipController>();

            // Initialize that enemy's projectile list
            shipControlComp.Projectiles = new List<GameObject>();

            // Set the current enemy's necessary movement data
            shipControlComp.VehiclePosition = new Vector3(
                Random.Range(-halfCamWidth, halfCamWidth),
                Random.Range(-halfCamHeight, halfCamHeight));
            shipControlComp.Direction = movementDir[Random.Range(0, 8)];
            newEnemy.transform.rotation = Quaternion.LookRotation(
                Vector3.back, shipControlComp.Direction);
            shipControlComp.Camera = camera;

            // Add the current enemy to the List of active enemies
            enemies.Add(newEnemy);
        }
    }
}
