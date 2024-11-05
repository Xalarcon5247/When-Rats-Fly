using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
    public static float bottomY = -20f;

    void Update() {
        if (transform.position.y < bottomY) {
            // Destroy the object if it falls below bottomY
            Destroy(this.gameObject);
            
            // Get a reference to the ApplePicker component of the Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            
            // Call the public AppleMissed() method of apScript
            apScript.AppleMissed();
        }
    }
}
