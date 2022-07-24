using UnityEngine;

public class Ghosts : MonoBehaviour
{
    
    
    public Movement movement { get; private set; }
    public Ghosts_Home home { get; private set; }
    public Ghosts_Scatter scatter { get; private set; }
    public Ghosts_Chase chase{ get; private set; }
    public Ghosts_Frightened frightened { get; private set; }

    public Ghosts_Behavior initialBehavior;

    public Transform target;

    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<Ghosts_Home>();
        this.scatter = GetComponent<Ghosts_Scatter>();
        this.chase = GetComponent<Ghosts_Chase>();
        this.frightened = GetComponent<Ghosts_Frightened>();
    }

    private void Start()
    {
        ResetState();
    }


    public void ResetState()
    {
        this.gameObject.SetActive(true);
        

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision) // OnCollisionEnter2D for normal Colliders
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PACMAN"))
        {
            
            if (this.frightened.enabled)
            {
               
                FindObjectOfType<Game_manager>().GhostsEaten(this);
            }
            else
            {
              
               
                FindObjectOfType<Game_manager>().PacmanEaten();
            }
        }
    }



}
