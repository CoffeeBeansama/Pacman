using UnityEngine;



public class Power_Pellet : Pellet // Referencing the parent Script
{
    public float duration = 8.0f;

    protected override void Eat()
    {
        FindObjectOfType<Game_manager>().PowerPelletEaten(this);
    }
}
