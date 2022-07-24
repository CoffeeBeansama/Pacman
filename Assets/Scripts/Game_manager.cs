using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour
{

    public Ghosts[] ghosts;
   
    
    [SerializeField] private AudioSource  GhostsEat;
    public Pacman pacman;
   

    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }

    public int lives { get; private set; }

    public void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown) { NewGame(); }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        Resetstate();
    }

     private void SetScore(int score)
    {
        this.score = score;
      
    }

    private void Resetstate()
    {
        ResetGhostsMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
       SceneManager.LoadScene("GameOver");
    }

   

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostsEaten(Ghosts ghosts)
    {
        GhostsEat.Play();
        int points = ghosts.points * this.ghostMultiplier;
        SetScore(this.score + ghosts.points);
        this.ghostMultiplier++;

    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
    
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].chase.Disable();
        }

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(Resetstate), 3.0f);
        }
        else { Invoke(nameof(GameOver), 2.0f); }
    }

    /// Pellet
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);


        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    /// Power Pellet
    public void PowerPelletEaten(Power_Pellet pellet)
    {
        
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);

        }
        CancelInvoke();
        Invoke(nameof(ResetGhostsMultiplier), pellet.duration);
        PelletEaten(pellet);

    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostsMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
