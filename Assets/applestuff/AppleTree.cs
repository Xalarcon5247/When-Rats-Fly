using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AppleTree : MonoBehaviour 
{
    [Header("Inscribed")]

    // Prefabs for instantiating apples
    public GameObject applePrefab;
    public GameObject GoldenApplePrefab;
    public GameObject RottenApplePrefab;
    public GameObject SpeedApplePrefab;

    // Speed at which the AppleTree moves
    public float speed = 5f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;

    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    public float GoldenAppleDropChance = 0.1f;
    public float RottenAppleDropChance = 0.1f;
    public float SpeedAppleDropChance = 0.05f;  

    void Start() 
    {
        // Start dropping apples
        Invoke("DropApple", 2f); 
    }

    public void ChangeSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void ChangeDelay(float newDelay){
        appleDropDelay = newDelay;
    }

    void DropApple() 
    {
        float val = Random.value;

        if (val < RottenAppleDropChance) 
        {
            GameObject apple = Instantiate<GameObject>(RottenApplePrefab);      
            apple.transform.position = transform.position;        
        } 
        else if (val < RottenAppleDropChance + GoldenAppleDropChance) 
        {
            GameObject apple = Instantiate<GameObject>(GoldenApplePrefab);      
            apple.transform.position = transform.position;        
        }
        else if (val < RottenAppleDropChance + GoldenAppleDropChance + SpeedAppleDropChance) 
        {
            GameObject apple = Instantiate<GameObject>(SpeedApplePrefab);      
            apple.transform.position = transform.position;        
        }
        else 
        {
            GameObject apple = Instantiate<GameObject>(applePrefab);        
            apple.transform.position = transform.position;        
        }       

        Invoke("DropApple", appleDropDelay);  
    }

    void Update() 
    {
        // Update code for moving the AppleTree and dropping apples
        Vector3 pos = transform.position;                     
        pos.x += speed * Time.deltaTime;                      
        transform.position = pos;  
        
        if (pos.x < -leftAndRightEdge) {                    
            speed = Mathf.Abs(speed);   // Move right       
        } 
        else if (pos.x > leftAndRightEdge) {              
            speed = -Mathf.Abs(speed);  // Move left     
        }
    }

    void FixedUpdate() 
    {                                                   
        if (Random.value < changeDirChance) {               
            speed *= -1; // Change direction 
        }
    }
}
