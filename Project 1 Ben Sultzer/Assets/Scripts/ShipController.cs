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
    public List<GameObject> projectiles;

    // Create Vector3 variables to store the
    // position, velocity, and movement direction
    // of the vehicle
    private Vector3 vehiclePosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        projectiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to position
        vehiclePosition += velocity;

        // Store half the height of the camera's viewport
        // to correctly measure if the vehicle's position relative
        // to the world origin is at the top or bottom edge of the
        // screen
        float halfCamHeight = camera.GetComponent<Camera>().orthographicSize;

        // Store half the width of the camera's viewport
        // to correctly measure if the vehicle's position relative
        // to the world origin is at the left or right edge of the
        // screen  
        float halfCamWidth = halfCamHeight * camera.GetComponent<Camera>().aspect;

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

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameObject newProjectile = Instantiate(projectile);

            Projectile projectileComp = newProjectile.GetComponent<Projectile>();

            projectileComp.ProjectilePosition = transform.position;
            projectileComp.Direction = direction;
            projectileComp.Camera = camera;

            projectiles.Add(newProjectile);
        }
    }
}
