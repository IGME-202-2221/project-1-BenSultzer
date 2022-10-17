using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Author: Ben Sultzer
// Purpose: Controls the movement and firing of 
// the player and enemy ships
// Restrictions: None
public class ShipController : MonoBehaviour
{
    // Create a public-facing variable to
    // store the camera to find it's viewport
    // width and height, each divided by two, to
    // aid with screen wrapping
    [SerializeField]
    GameObject camera;

    // Create a public-facing variable to store the
    // projectile for the player
    [SerializeField]
    GameObject projectile;

    // Create a variable to store the acceleration
    // of the vehicle
    float acceleration = 10f;

    // Create a variable to store the 
    // maximum speed of the vehicle
    private float maxSpeed = 5f;

    // Create a List to store all of the player's
    // fired projectiles
    private List<GameObject> projectiles;

    // Create Vector3 variables to store the
    // position, velocity, and movement direction
    // of the vehicle
    private Vector3 vehiclePosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    // Create a variable to store half the height of the camera's
    // viewport to correctly measure if the vehicle's position
    // relative to the world origin is at the top or bottom edge
    // of the screen
    private float halfCamHeight;

    // Create a variable to store half the width of the camera's
    // viewport to correctly measure if the vehicle's position
    // relative to the world origin is at the left or right edge
    // of the screen  
    private float halfCamWidth;

    /// <summary>
    /// Property for getting and setting the current ship's 
    /// position
    /// </summary>
    public Vector3 VehiclePosition
    {
        get
        {
            return vehiclePosition;
        }

        set
        {
            vehiclePosition = value;
        }
    }

    /// <summary>
    /// Property for getting and setting the current ship's 
    /// direction
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
    /// Property for getting and setting the
    /// camera (used to pass the Main Camera 
    /// from the EnemyManager to each newly-
    /// created enemy, so that each newly-
    /// created enemy can then in turn pass 
    /// the camera to their fired projectiles)
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

    /// <summary>
    /// Property for getting a ship's list of projectiles
    /// </summary>
    public List<GameObject> Projectiles
    {
        get
        {
            return projectiles;
        }

        set
        {
            projectiles = value;
        }
    }

    /// <summary>
    /// Property for getting and setting the
    /// velocity of the vehicle (specifically
    /// for slowing the speed of a ship part
    /// when it is spawned)
    /// </summary>
    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        projectiles = new List<GameObject>();
        halfCamHeight = camera.GetComponent<Camera>().orthographicSize;
        halfCamWidth = halfCamHeight * camera.GetComponent<Camera>().aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity with acceleration is direction * acceleration
        // * deltaTime added to the current velocity.
        velocity += direction * acceleration * Time.deltaTime;

        // Prevent the vehicle from accelerating past the max speed
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // Add velocity to position
        vehiclePosition += velocity * Time.deltaTime;

        // Check to see if the player has reached the edges
        // of the screen (in both the x- and y-directions)
        // and if so, wrap them to the other side
        if (vehiclePosition.x > halfCamWidth)
        {
            vehiclePosition.x = -halfCamWidth;
        }
        else if (vehiclePosition.x < -halfCamWidth)
        {
            vehiclePosition.x = halfCamWidth;
        }

        if (vehiclePosition.y > halfCamHeight)
        {
            vehiclePosition.y = -halfCamHeight;
        }
        else if (vehiclePosition.y < -halfCamHeight)
        {
            vehiclePosition.y = halfCamHeight;
        }

        // "Draw" this vehicle at that position
        transform.position = vehiclePosition;

        // Check to see if any of the projectiles have left the 
        // screen bounds and if so, remove that projectile from
        // the List and destroy it
        for (int i = 0; i < projectiles.Count; i++)
        {
            // Store the x- and y-position of the current projectile's
            // Projectile Component
            float currProjectileX = projectiles[i].GetComponent<Projectile>().ProjectilePosition.x;
            float currProjectileY = projectiles[i].GetComponent<Projectile>().ProjectilePosition.y;

            if (currProjectileX > halfCamWidth ||
                currProjectileX < -halfCamWidth ||
                currProjectileY > halfCamHeight ||
                currProjectileY < -halfCamHeight)
            {
                GameObject projectileToDestroy = projectiles[i];
                projectiles.RemoveAt(i);
                Destroy(projectileToDestroy);
            }
        }
    }

    /// <summary>
    /// Purpose: Change the direction the vehicle is moving, 
    /// as well as the rotation of the vehicle asset, based on the currently 
    /// detected input
    /// Restrictions: None
    /// </summary>
    /// <param name="context">Information about the detected input. 
    /// Recieved from the Player Input Component of this 
    /// GameObject</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }

    /// <summary>
    /// Fire a projectile in the direction the player is facing
    /// when the left mouse button is clicked
    /// </summary>
    /// <param name="context">Information about the detected input. 
    /// Recieved from the Player Input Component of this 
    /// GameObject</param>
    public void OnFire(InputAction.CallbackContext context)
    {
        // Fire the projectile only when the context enters the 
        // "performed" phase (prevents a click event from firing
        // twice as many projectiles as desired)
        if (context.phase == InputActionPhase.Performed)
        {
            // Instantiate the Projectile prefab
            GameObject newProjectile = Instantiate(projectile);

            // Get the currently fired projectile's Projectile 
            // Component
            Projectile projectileComp = newProjectile.GetComponent<Projectile>();

            // Set the currently fired projectile's necessary movement
            // data
            projectileComp.ProjectilePosition = transform.position;
            projectileComp.Direction = transform.up;
            projectileComp.Camera = camera;

            // Add the currently fired projectile to the List of
            // active projectiles
            projectiles.Add(newProjectile);
        }
    }

    /// <summary>
    /// Create a separate firing method for enemies that does not
    /// rely on player input
    /// </summary>
    public void EnemyFire()
    {
        // Instantiate the Projectile prefab
        GameObject newProjectile = Instantiate(projectile);

        // Get the currently fired projectile's Projectile 
        // Component
        Projectile projectileComp = newProjectile.GetComponent<Projectile>();

        // Set the currently fired projectile's necessary movement
        // data
        newProjectile.transform.position = transform.position;
        projectileComp.ProjectilePosition = transform.position;
        projectileComp.Direction = transform.up;
        projectileComp.Camera = camera;

        // Add the currently fired projectile to the List of
        // active projectiles
        projectiles.Add(newProjectile);
    }

    /// <summary>
    /// Create a separate firing method for upgraded enemies that does not
    /// rely on player input
    /// </summary>
    public void UpgradedEnemyFire()
    {
        // Instantiate two Projectile prefabs
        GameObject newProjectile1 = Instantiate(projectile);
        GameObject newProjectile2 = Instantiate(projectile);

        // Get both fired projectiles' Projectile Component
        Projectile projectileComp1 = newProjectile1.GetComponent<Projectile>();
        Projectile projectileComp2 = newProjectile2.GetComponent<Projectile>();

        // Set the necessary movement data for both of the fired projectiles
        newProjectile1.transform.position = transform.position;
        projectileComp1.ProjectilePosition = transform.position;
        projectileComp1.Direction = transform.up;
        projectileComp1.Camera = camera;
        newProjectile2.transform.position = transform.position;
        projectileComp2.ProjectilePosition = transform.position;
        projectileComp2.Direction = transform.up;
        projectileComp2.Camera = camera;

        // Add both fired projectiles to the List of
        // active projectiles
        projectiles.Add(newProjectile1);
        projectiles.Add(newProjectile2);
    }
}
