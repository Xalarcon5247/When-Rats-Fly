using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float maxY = 120f;
    public float speed = 0.075f;
    public float currentYpos = 0f;


    // Update is called once per frame
    void Update()
    {
        currentYpos -= speed;
        this.gameObject.transform.position = new Vector3(0, currentYpos, 0);
        if (currentYpos < -maxY){
            this.gameObject.transform.position = new Vector3(0,0,0);
            currentYpos = 0;
        }
    }
}
