using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spawned")
        {
            Destroy(other.gameObject);
        }
    }
}
