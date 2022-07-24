using UnityEngine;

public class Ghosts_Scatter : Ghosts_Behavior
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        /// Checks Direction
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirection.Count);

            /// Avoids Ghosts fromgoing back and forth
            if (node.availableDirection[index] == -this.ghost.movement.direction && node.availableDirection.Count > 1)
            {
                index++;

                if (index >= node.availableDirection.Count)
                { index = 0; }
            }
            this.ghost.movement.SetDirection(node.availableDirection[index]);
        }
    }
}
