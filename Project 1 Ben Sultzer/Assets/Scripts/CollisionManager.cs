using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Author: Ben Sultzer
// Purpose: Controls resolution for collisions between
// the player and an enemy ship, a player's projectile
// and an enemy ship, and an enemy's projectile and the
// player
// Restrictions: None
public class CollisionManager : MonoBehaviour
{
    // Create a public-facing variable to store the
    // player
    [SerializeField]
    GameObject player;

    // Create a public-facing variable to store the 
    // enemy manager
    [SerializeField]
    GameObject enemyManager;

    // Create a variable to track the time and 
    // flash the player's sprite invisible when they
    // are hit
    private float flashTracker;

    // Create a List to store the collidable enemies
    private List<GameObject> collidableEnemies;

    // Create a List to store the collidable projectiles
    // of the player
    private List<GameObject> collidableProjectilesPlayer;

    // Create a List to store the collidable projectiles
    // of each enemy
    private List<List<GameObject>> collidableProjectilesEnemies;

    // Create a CollisionDetection variable to gain access to the
    // Bounding Circle collision calculation method.
    private CollisionDetection collisionDetection;

    // Start is called before the first frame update
    void Start()
    {
        collisionDetection = new CollisionDetection();
        collidableProjectilesEnemies = new List<List<GameObject>>();
        flashTracker = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize the lists of collidable enemies and collidable projectiles
        // for the player and enemies
        collidableEnemies = enemyManager.GetComponent<EnemyManager>().Enemies;
        collidableProjectilesPlayer = player.GetComponent<ShipController>().Projectiles;
        for (int i = 0; i < collidableEnemies.Count; i++)
        {
            collidableProjectilesEnemies.Add(collidableEnemies[i].
                GetComponent<ShipController>().Projectiles);
        }

        // Test for collisions between any of the player's projectiles and
        // any of the enemy ships
        for (int i = 0; i < collidableProjectilesPlayer.Count; i++)
        {
            for (int j = 0; j < collidableEnemies.Count; j++)
            {
                if (collisionDetection.CircleCollision(collidableProjectilesPlayer[i],
                    collidableEnemies[j]))
                {
                    // Increment the player's score when they hit an enemy
                    player.GetComponent<Player>().Score += 100 * player.GetComponent<Player>().HullLevel;

                    // Get the projectile that hit an enemy ship, the enemy ship that was
                    // hit, and create a variable to store each of the projectiles fired
                    // by the hit enemy
                    GameObject playerProjectileToDestroy = collidableProjectilesPlayer[i];
                    GameObject enemyToDestroy = collidableEnemies[j];
                    GameObject enemyProjectileToDestroy;

                    // Destroy all leftover projectiles fired by the hit enemy, remove
                    // them from that enemy's List of projectiles
                    for (int k = collidableProjectilesEnemies[j].Count - 1; k >= 0; k--)
                    {
                        enemyProjectileToDestroy = collidableProjectilesEnemies[j][k];
                        collidableProjectilesEnemies[j].RemoveAt(k);
                        Destroy(enemyProjectileToDestroy);
                    }

                    // Remove the current enemy's projectile List from the overall List of enemy
                    // projectiles
                    collidableProjectilesEnemies.RemoveAt(j);

                    // Remove from their respective Lists and destroy, the player projectile
                    // that hit an enemy and the enemy that was hit
                    collidableProjectilesPlayer.RemoveAt(i);
                    Destroy(playerProjectileToDestroy);
                    collidableEnemies.RemoveAt(j);
                    Destroy(enemyToDestroy);
                    break;
                }
            }
        }

        // Test for collisions between any of the enemy's projectiles and the player
        for (int i = 0; i < collidableProjectilesEnemies.Count; i++)
        {
            for (int j = 0; j < collidableProjectilesEnemies[i].Count; j++)
            {
                if (collisionDetection.CircleCollision(collidableProjectilesEnemies[i][j],
                    player))
                {
                    // Turn the player invisible when they are hit
                    player.GetComponent<SpriteRenderer>().color = Color.clear;

                    // Increment the number of times the player has been hit
                    player.GetComponent<Player>().NumHits++;

                    // Test for the game over condition
                    if (player.GetComponent<Player>().NumHits == 3 &&
                        player.GetComponent<Player>().HullLevel == 1)
                    {
                        // The payer has run out of lives and is at the lowest hull level
                        // so reset the game
                        player.GetComponent<Player>().NumHits = 0;
                        player.GetComponent<ShipController>().VehiclePosition = new Vector3(0, 0);
                    }

                    // Remove the projectile that hit the player from the projectile List of the
                    // corresponding enemy and destroy that projectile
                    GameObject enemyProjectileToDestroy = collidableProjectilesEnemies[i][j];
                    collidableProjectilesEnemies[i].RemoveAt(j);
                    Destroy(enemyProjectileToDestroy);
                    break;
                }
            }
        }

        // Update the flash tracker
        if (flashTracker < 0.25f)
        {
            flashTracker += Time.deltaTime;
        // Turn the player visible again after a quarter of
        // a second and reset the flash tracker
        } else
        {
            flashTracker = 0.0f;
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
