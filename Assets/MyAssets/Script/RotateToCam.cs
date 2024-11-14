using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position, Vector3.up);
        this.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
