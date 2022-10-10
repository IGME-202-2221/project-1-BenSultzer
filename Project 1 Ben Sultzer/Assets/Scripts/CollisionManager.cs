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

        // Test for collisions between the player and any of the enemy
        // ships
        for (int i = 0; i < collidableEnemies.Count; i++)
        {
            if (collisionDetection.CircleCollision(player, collidableEnemies[i]))
            {
                player.GetComponent<ShipController>().VehiclePosition = new Vector3(0, 0);
            }
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
                    // Get the projectile that hit an enemy ship, the enemy ship that was
                    // hit, and create a variable to store each of the projectiles fired
                    // by the hit enemy
                    GameObject playerProjectileToDestroy = collidableProjectilesPlayer[i];
                    GameObject enemyToDestroy = collidableEnemies[j];
                    GameObject enemyProjectileToDestroy;

                    // Destroy all leftover projectiles fired by the hit enemy, remove
                    // them from that enemy's List of projectiles, and remove that enemy's
                    // projectile List from the overall List of enemy projectiles
                    for (int k = 0; k < collidableProjectilesEnemies[j].Count; k++)
                    {
                        enemyProjectileToDestroy = collidableProjectilesEnemies[j][k];
                        collidableProjectilesEnemies[j].RemoveAt(k);
                        collidableProjectilesEnemies.RemoveAt(j);
                        Destroy(enemyProjectileToDestroy);
                    }

                    // Remove from their respective Lists and destroy, the player projectile
                    // that hit an enemy and the enemy that was hit
                    collidableProjectilesPlayer.RemoveAt(i);
                    Destroy(playerProjectileToDestroy);
                    collidableEnemies.RemoveAt(j);
                    Destroy(enemyToDestroy);
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
                    // Reset the player's position to the center of the screen if they were hit
                    player.GetComponent<ShipController>().VehiclePosition = new Vector3(0, 0);

                    // Remove the projectile that hit the player from the projectile List of the
                    // corresponding enemy and destroy that projectile
                    GameObject enemyProjectileToDestroy = collidableProjectilesEnemies[i][j];
                    collidableProjectilesEnemies[i].RemoveAt(j);
                    Destroy(enemyProjectileToDestroy);
                }
            }
        }
    }
}
