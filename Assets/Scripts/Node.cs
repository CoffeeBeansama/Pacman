using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirection { get; private set; }

    private void Start()
    {
        this.availableDirection = new List<Vector2>(); // add "using System.Collections.Generic" for creating lists


        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1f, this.obstacleLayer); /// checks the collision
        
        if (hit.collider == null) // checks if you hit something
        {
            this.availableDirection.Add(direction); // add direction from list
        }

    }

}
