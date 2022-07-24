using UnityEngine;

public class Ghosts_Frightened : Ghosts_Behavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    [SerializeField] private AudioSource GhostsSiren;
    [SerializeField] private AudioSource PPEaten;
 
    public bool PlaySound = false;
    public bool eaten { get; private set; }

    public override void Enable(float duration) // custom function vs "OnEnabled"
    {
       
        
       

        
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;
       
        
        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
      GhostsSiren.Play();
        
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<Animated_sprite>().Restart();
        }

    }    

    private void Eaten()
    {
        
      
        this.eaten = true;
        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;
 
        this.ghost.home.Enable(this.duration);



        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;

    }

    private void OnEnable()
    {
         PPEaten.Play();
        
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
        this.PPEaten.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision) // OnCollisionEnter2D for normal Colliders
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PACMAN"))
        {
             
            if (this.enabled)
            {
                
                Eaten();
            }
            
        }
    }


    //Avoids ghosts collision and makes the ghosts runaway as farthest as possible to pacman
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Node node = other.GetComponent<Node>(); 


        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirection)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }
    }


}
