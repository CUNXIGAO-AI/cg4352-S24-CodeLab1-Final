using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Demos;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Rigidbody[] sphereRigidbodies;
    public AudioSource AlmostSound;
    private void Awake()
    {
        foreach (var rb in sphereRigidbodies)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {

            GameManager.instance.LoadEndScene();

        }
        
        if (other.CompareTag("Sphere"))
        {
            foreach (var rb in sphereRigidbodies)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
        
        if (other.CompareTag("Almost"))
        {
            AlmostSound.Play();
        }
    }
}
