using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Ben Sultzer
// Purpose: Calculates all the necessary information about
// a sprite for collision detection: minimum x-position,
// maximum x-position, minimum y-position, maximum y-position,
// and the center of the sprite
// Restrictions: None
public class SpriteInfo : MonoBehaviour
{
    // Fields
    Bounds bounds;
    float minX;
    float maxX;
    float minY;
    float maxY;
    Vector3 center;
    Vector3 size;

    /// <summary>
    /// Property for getting the x-position
    /// of the sprite's lower left corner
    /// </summary>
    public float MinX
    {
        get
        {
            return minX;
        }
    }

    /// <summary>
    /// Property for getting the x-position
    /// of the sprite's upper right corner
    /// </summary>
    public float MaxX
    {
        get
        {
            return maxX;
        }
    }

    /// <summary>
    /// Property for getting the y-position
    /// of the sprite's lower left corner
    /// </summary>
    public float MinY
    {
        get
        {
            return minY;
        }
    }

    /// <summary>
    /// Property for getting the y-position
    /// of the sprite's upper right corner
    /// </summary>
    public float MaxY
    {
        get
        {
            return maxY;
        }
    }

    /// <summary>
    /// Property for getting the size
    /// of the sprite
    /// </summary>
    public Vector3 Size
    {
        get
        {
            return size;
        }
    }

    /// <summary>
    /// Property for getting the center
    /// of the sprite image
    /// </summary>
    public Vector3 Center
    {
        get
        {
            return center;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bounds = gameObject.GetComponent<SpriteRenderer>().bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
        minY = bounds.min.y;
        maxY = bounds.max.y;
        center = bounds.center;
        size = bounds.size;
    }
}
