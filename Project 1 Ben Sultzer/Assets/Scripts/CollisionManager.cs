using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Author: Ben Sultzer
// Purpose: Controls which collision detection method 
// to use and manages how the player switches between the
// two
// Restrictions: None
public class CollisionManager : MonoBehaviour
{
    // Create a public facing variable to store the
    // player car
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
    [SerializeField]
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
        collidableEnemies = enemyManager.GetComponent<EnemyManager>().Enemies;
        collidableProjectilesPlayer = player.GetComponent<ShipController>().Projectiles;
        for (int i = 0; i < collidableEnemies.Count; i++)
        {
            collidableProjectilesEnemies.Add(collidableEnemies[i].
                GetComponent<ShipController>().Projectiles);
        }

        for (int i = 0; i < collidableEnemies.Count; i++)
        {
            if (collisionDetection.CircleCollision(player, collidableEnemies[i]))
            {
                player.GetComponent<ShipController>().VehiclePosition = new Vector3(0, 0);
                break;
            }
        }

        for (int i = 0; i < collidableProjectilesPlayer.Count; i++)
        {
            for (int j = 0; j < collidableEnemies.Count; j++)
            {
                if (collisionDetection.CircleCollision(collidableProjectilesPlayer[i], 
                    collidableEnemies[j]))
                {
                    GameObject projectileToDestroy = collidableProjectilesPlayer[i];
                    collidableProjectilesPlayer.RemoveAt(i);
                    Destroy(projectileToDestroy);
                    GameObject enemyToDestroy = collidableEnemies[j];
                    collidableEnemies.RemoveAt(j);
                    Destroy(enemyToDestroy);
                    break;
                }
            }
        }
    }
}
