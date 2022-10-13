using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Ben Sultzer
// Purpose: Executes the Bounding circle collision detection
// method and gives information used by the CollisionManager
// script conveying whether or not a collision occurred
// Restrictions: None
public class CollisionDetection : MonoBehaviour
{
    // Fields
    bool possibleCollision;

    // Start is called before the first frame update
    void Start()
    {
        possibleCollision = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Determines if a collision occurred using the Bounding Circle
    /// collision detection method
    /// </summary>
    /// <param name="gameObj1">The first GameObject for which to detect
    /// a collision</param>
    /// <param name="gameObj2">The second GameObject with which the
    /// first can collide and vise versa</param>
    /// <returns>A Boolean corresponding to whether or not a collision
    /// occurred</returns>
    public bool CircleCollision(GameObject gameObj1, GameObject gameObj2)
    {
        // Get a reference to each of the GameObject's
        // SpriteInfo components
        SpriteInfo gameObj1Info = gameObj1.GetComponent<SpriteInfo>();
        SpriteInfo gameObj2Info = gameObj2.GetComponent<SpriteInfo>();

        // Store the radii of each GameObject's containing
        // cricle
        float gameObj1Rad = gameObj1.GetComponent<SpriteRenderer>().sprite.bounds.max.y;
        float gameObj2Rad = gameObj2.GetComponent<SpriteRenderer>().sprite.bounds.max.y;

        // Determine if the square distance between the centers 
        // of each GameObject is less than the total combined
        // square radii of their bounding circles
        // Calculate sqaure distance
        float squareDistance = Vector3.SqrMagnitude(gameObj1.transform.position - gameObj2.transform.position);

        // Calculate square combined radius
        float squaredCombinedRad = Mathf.Pow(gameObj1Rad + gameObj2Rad, 2);

        // Compare the two values to determine if a possible collision has
        // occurred
        if (squareDistance < squaredCombinedRad)
        {
            possibleCollision = true;
        }
        else
        {
            possibleCollision = false;
        }

        // Return the result
        return possibleCollision;
    }
}
