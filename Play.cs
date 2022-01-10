using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
    }
}
