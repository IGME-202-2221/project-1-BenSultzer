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

    // Create a public-facing variable
    // to store the speed of the vehicle
    [SerializeField]
    float speed = 1f;

    // Create a public-facing variable to store the
    // projectile for the player
    [SerializeField]
    GameObject projectile;

    // Create a List to store all of the player's
    // fired projectiles
    List<GameObject> projectiles;

    // Create Vector3 variables to store the
    // position, velocity, and movement direction
    // of the vehicle
    private Vector3 vehiclePosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    // Create a variable to store the current hull level of
    // the given ship
    private int hullLevel;

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
    /// Property for getting half the height of the 
    /// viewport
    /// </summary>
    public float HalfCamHeight
    {
        get
        {
            return halfCamHeight;
        }
    }

    /// <summary>
    /// Property for getting half the width of the 
    /// viewport
    public float HalfCamWidth
    {
        get
        {
            return halfCamWidth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        projectiles = new List<GameObject>();
        hullLevel = 1;
        halfCamHeight = camera.GetComponent<Camera>().orthographicSize;
        halfCamWidth = halfCamHeight * camera.GetComponent<Camera>().aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to position
        vehiclePosition += velocity;

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
}
