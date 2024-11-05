using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    [Header("Inscribed")]

    // Prefab for instantiating peas
    public GameObject peaPrefab;
    public GameObject bigPeaPrefab;

    public GameObject batPrefab;
    public GameObject bananaPrefab;

    // Seconds between pea instantiations
    public float avgDropDelay = 0.5f;


    void Start() 
    {
        // Start spawning objects
        float variance = Random.Range(0, 2f); 
        float dropDelay = avgDropDelay + variance;
        Invoke( "Drop", dropDelay); 
    }
    void Drop() {       
        // Create variability in the spawning of objects in each lane. 
        float variance = Random.Range(0, 2f); 
        float dropDelay = avgDropDelay + variance;
        // Spawn the various objects based on set probabilities with a random value. 
        float chance = Random.Range(0, 1f);
        // 82.5% chance to drop a pea
        if (chance <= 0.825f){
            dropPea();
        }
        //5% chance to drop a bigpea
        else if (chance > 0.825f && chance <= 0.875f){
            dropBigPea();
        }
        //2.5% chance to drop a banana
        else if (chance > 0.875f && chance <= 0.90f){
            dropBanana();
        }
        //10% chance to drop a bat
        else if (chance > 0.90f){
            dropBat();
        }
        

        Invoke( "Drop", dropDelay);                                      
        }
    
    void dropPea() {
        GameObject pea = Instantiate<GameObject>(peaPrefab);        
        pea.transform.position = transform.position;        
    }
    void dropBigPea(){
        GameObject bigPea = Instantiate<GameObject>(bigPeaPrefab);
        bigPea.transform.position = transform.position;
    }
    void dropBat(){
        GameObject bat = Instantiate<GameObject>(batPrefab);
        bat.transform.position = transform.position;
    }
    void dropBanana(){
        GameObject banana = Instantiate<GameObject>(bananaPrefab);
        banana.transform.position = transform.position;
    }
}
