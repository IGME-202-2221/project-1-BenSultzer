using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Ben Sultzer
// Purpose: Define the data for a projectile
// in the game
// Restrictions: None
public class Projectile : MonoBehaviour
{
    // Create a public-facing variable
    // to store the speed of the vehicle
    [SerializeField]
    float speed = 1f;

    // Create a variable to store the camera to
    // find it's viewport width and height, each
    // divided by two, to aid with screen wrapping
    private GameObject camera;

    // Create Vector3 variables to store the
    // position, velocity, and movement direction
    // of the vehicle
    private Vector3 projectilePosition;
    private Vector3 direction;
    private Vector3 velocity;

    /// <summary>
    /// Property for getting and setting
    /// the position of the Projectile
    /// </summary>
    public Vector3 ProjectilePosition
    {
        get
        {
            return projectilePosition;
        }

        set
        {
            projectilePosition = value;
        }
    }

    /// <summary>
    /// Property for getting and setting
    /// the movement direction of the Projectile
    /// </summary>
    public Vector3 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    /// <summary>
    /// Property for getting and setting
    /// the camera for screen wrapping
    /// </summary>
    public GameObject Camera
    {
        get
        {
            return camera;
        }

        set
        {
            camera = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to position
        projectilePosition += velocity;

        // "Draw" this vehicle at that position
        transform.position = projectilePosition;
    }
}
