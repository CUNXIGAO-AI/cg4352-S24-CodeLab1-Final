using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public GameObject failedUI;
    // Start is called before the first frame update
    void Start()
    {
        failedUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -25f)
        {
            failedUI.SetActive(true);
        }
        
        if (gameObject.transform.position.y < -100f)
        {
            GameManager.instance.LoadStartScene();
            failedUI.SetActive(false);
        }
    }
}
