using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotating : MonoBehaviour
{
    private float rotateSpd = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Control the camera rotation in the start scene
        transform.Rotate(0, rotateSpd * Time.deltaTime, 0);
    }
}
