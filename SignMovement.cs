using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.z < -3.0f){
            Destroy(this.gameObject);
        }
        
    }

    void OnCollisionEnter(Collision other) {
        // Debug.Log(other.gameObject.tag);
        // Destroy the sign that was collided with
        Destroy(this.gameObject);
    }
    
}
