using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {
    private static float bottomY = -16f;

    void Update() {
        if (transform.position.y < bottomY) {
            // Destroy the object if it falls below bottomY
            Destroy(this.gameObject);
        }
    }
}
