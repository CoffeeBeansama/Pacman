
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] /// calling SpriteRenderer

public class Animated_sprite : MonoBehaviour
{
    /// CHANGE SPRITE
    
    public SpriteRenderer spriteRenderer { get; private set; } ///Setting Sprite Renderer
    public Sprite[] sprites;

    public float animationTime = 0.25f;
    public int animationFrame { get; private set; } /// for easy referencing
    public bool loop = true;



    public void Awake() /// Awake function gets called after Initialization
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);


    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }

        this.animationFrame++;
        if (this.animationFrame >= this.sprites.Length && this.loop)  /// if image_index  is > number of sprites
        {
            this.animationFrame = 0; /// repeats the animation to zero
        }

        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) /// to avoid image_index errors
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart() /// Manually restarting the animation
    {
        this.animationFrame = -1;
        Advance();
    }

}
