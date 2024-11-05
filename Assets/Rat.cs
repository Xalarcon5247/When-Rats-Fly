using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed of movement
    private bool isFacingRight = true; // To keep track of which direction the sprite is facing
    public ScoreCounter scoreCounter;
    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        // Find a GameObject named ScoreCounter in the Scene Hierarchy
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Get the ScoreCounter (Script) component of scoreGO
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
        // Get the Rigidbody2D and SpriteRenderer components
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

         // Move the player by modifying transform position directly
         transform.position += new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);

        // Flip the sprite based on direction
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    // Function to flip the sprite
    void Flip()
    {
        isFacingRight = !isFacingRight;

        // Multiply the sprite's x scale by -1 to flip it
        Vector3 scaler = transform.localScale;
        scaler.y *= -1;
        transform.localScale = scaler;
    }
    
    void OnCollisionEnter(Collision coll) {
        // Find out what hit the rat
        GameObject collidedWith = coll.gameObject;
        // Get a reference to the WhenRatsFly component of the Main Camera
        WhenRatsFly wrfScript = Camera.main.GetComponent<WhenRatsFly>();
        if (collidedWith.CompareTag("Pea")) {
            // Destroy the object if it has the "Pea" tag
            Destroy(collidedWith);
            // Increase the score
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE( scoreCounter.score);

            // Call the public eatPea() method of the wrf script
            wrfScript.eatPea();
        }
        else if (collidedWith.CompareTag("BigPea")) {
            // Destroy the object if it has the "Pea" tag
            Destroy(collidedWith);
            // Increase the score
            scoreCounter.score += 200;
            HighScore.TRY_SET_HIGH_SCORE( scoreCounter.score);

            // Call the public eatPea() method of the wrf script
            wrfScript.eatBigPea();
        }
        else if (collidedWith.CompareTag("Bat")) {
            //Destroy the object if it has the "Bat" tag
            Destroy(collidedWith);
            // Call the public GameOver() method of the wrfscript
            wrfScript.GameOver();
        }
        else if (collidedWith.CompareTag("Banana")) {
            // Destroy the object if it has the "Banana" tag
            Destroy(collidedWith);
            // Call the public eatBanana method of the wrfscript
            wrfScript.eatBanana();
        }
    }
}
