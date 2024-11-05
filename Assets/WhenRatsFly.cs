using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video; // Add this line
using UnityEngine.UI;

//Controls the gameplay, implements item functionality and overheads the time and conditions
public class WhenRatsFly : MonoBehaviour {
    [Header("Inscribed")]
    public GameObject ratPrefab;
    public TextMeshProUGUI staminaText; 
    private int stamina;  // Variable to store stamina value
    private float duration;
    //Audio and Video variables
    public VideoPlayer videoPlayer; 

    public RawImage rawImage; 
    public AudioClip nibblingAudio;
    public AudioClip squeakAudio;
    public AudioClip bananaSound;
    private AudioSource audioSource;

    // Store original speeds for resetting
    private float originalMoveSpeed;
    private float originalBackgroundSpeed;

    private float originalSpawnerDelay;

    // Variables for the banana effect
    private bool isBananaActive = false;  // Track if banana is active
    private float bananaEndTime = 0f;     // Time when the banana effect should end

    // Variables for Game Over timer, used to be able to play the game over viceo before reloading the scene
    private bool gameOver = false;
    private float gameOverTimer = 0f;
    public float gameOverDelay = 5f; // Delay before reloading the scene after gameover

    void Start() {
        // Reference the AudioSource component
        audioSource = GetComponent<AudioSource>();
        stamina = int.Parse(staminaText.text);  // Convert the stamina text to an integer
        // Make sure the gameoverclip is disabled initially
        rawImage.enabled = false;
        videoPlayer.Pause(); 
    }

    void Update() {
        // Continuously update the stamina value from the TextMeshPro text
        stamina = int.Parse(staminaText.text); 
        duration = Time.time;

        if (gameOver) {
            gameOverTimer += Time.deltaTime; // Increment the timer

            if (gameOverTimer >= gameOverDelay) {
                SceneManager.LoadScene("_Scene_0"); // Reload the scene after the delay
            }
        }
        
        // Check if the banana effect is active and should end
        if (isBananaActive && Time.time >= bananaEndTime) {
            endBanana();
        }

    }

    public void GameOver() {
        //Play the squeak(death) sound
        audioSource.PlayOneShot(squeakAudio);
        //Move the rat out of the game area (prevent weird behavior in the background while ending is playing)
        GameObject rat = GameObject.Find("Rat");
        rat.transform.position = new Vector3(1000f, 1000f, 0f);
        //Play the gameoverclip
        rawImage.enabled = true; 
        videoPlayer.Play(); 
        //Restart
        gameOver = true;
    }
    public void eatBanana(){
        //Play the banana eat sound
        audioSource.PlayOneShot(bananaSound);
        // Speeds up gameplay, increasing item drop frequency and speed of background and speed of rat side movement.
        GameObject Backrparent = GameObject.Find("Backrparent");
        Background backScript = Backrparent.GetComponentInChildren<Background>();
        GameObject Rat = GameObject.Find("Rat");
        Rat ratScript = Rat.GetComponentInChildren<Rat>();
        GameObject SpawnerManager = GameObject.Find("SpawnerManager");
        SpawnerManager spawnerManagerScript = SpawnerManager.GetComponentInChildren<SpawnerManager>();

        // Save the original speeds
        originalMoveSpeed = ratScript.moveSpeed;
        originalBackgroundSpeed = backScript.speed;
        originalSpawnerDelay = spawnerManagerScript.avgDropDelay;

        // Apply banana speed up
        ratScript.moveSpeed = 50f;
        backScript.speed = 0.175f;
        spawnerManagerScript.setDropDelay(0.3f);

        // Set the banana effect duration (4 seconds)
        isBananaActive = true;
        bananaEndTime = Time.time + 4f;

    } 
    public void endBanana(){
        GameObject Backrparent = GameObject.Find("Backrparent");
        Background backScript = Backrparent.GetComponentInChildren<Background>();
        GameObject Rat = GameObject.Find("Rat");
        Rat ratScript = Rat.GetComponentInChildren<Rat>();
        GameObject SpawnerManager = GameObject.Find("SpawnerManager");
        SpawnerManager spawnerManagerScript = SpawnerManager.GetComponentInChildren<SpawnerManager>();

        // Reset to original speeds
        ratScript.moveSpeed = originalMoveSpeed;
        backScript.speed = originalBackgroundSpeed;
        spawnerManagerScript.setDropDelay(originalSpawnerDelay);

        // Mark the banana effect as inactive
        isBananaActive = false;
    }
    public void eatPea(){
        //Play the eating sound
        audioSource.PlayOneShot(nibblingAudio);
        //Pea consumed, increase stamina with a cap at 100.
        GameObject canvas = GameObject.Find("Canvas"); 
        Stamina stamScript = canvas.GetComponentInChildren<Stamina>();
        if (stamScript.stamina < 100){
            stamScript.stamina += 10;
        }
    }
    public void eatBigPea(){
        //Play the eating sound
        audioSource.PlayOneShot(nibblingAudio);
        // BigPea consumed, largly increase the stamina, can go over 100 limit
        GameObject canvas = GameObject.Find("Canvas"); 
        Stamina stamScript = canvas.GetComponentInChildren<Stamina>();
        stamScript.stamina += 30;
    }
}
