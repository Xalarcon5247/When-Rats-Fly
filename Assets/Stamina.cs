using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // This line enables use of uGUI classes like Text.

public class Stamina : MonoBehaviour {
    [Header("Dynamic")]

    public int stamina = 100;
    private TextMeshProUGUI uiText;
    private float decreaseInterval = 0.60f; // Time interval for decreasing stamina 
    private float nextDecreaseTime = 0f; // Time to track when stamina should decrease

    private float changeDecreaseInterval = 10f;
    private float delay = 3f; //Wait a few seconds for the game to start

    void Start() {
        uiText = GetComponent<TextMeshProUGUI>();
        nextDecreaseTime = Time.time + delay;
    }

    void Update() {
        //Changes difficulty over time, increasing the rate of stamina decay.
        if (Time.time >= changeDecreaseInterval){
            decreaseInterval -= 0.01f;
            changeDecreaseInterval = Time.time + 15f;
        }
        // Check if it's time to decrease stamina
        if (Time.time >= nextDecreaseTime) {
            // Decrease stamina
            stamina -= 10;

            // Update the UI text to show the current stamina
            uiText.text = stamina.ToString("#,0");

            // Set the time for the next decrease
            nextDecreaseTime = Time.time + decreaseInterval;
        }

        if (stamina <= 0) {
            //Game over, out of stamina, end game
            Destroy(this.gameObject);
            // Get a reference to the WhenRatsFly component of the Main Camera
            WhenRatsFly wrfScript = Camera.main.GetComponent<WhenRatsFly>();
            //Make stamina a high value to prevent strange behavior while game is ending
            stamina = 10000;
            // Call the public GameOver() method of the wrfscript
            wrfScript.GameOver();
        }
    } 
    
}
