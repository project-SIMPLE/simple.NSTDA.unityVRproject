using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    [SerializeField]
    private bool rotate1Axis = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position, Vector3.up);
        if (!rotate1Axis)
        {
            this.transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
        else
        {
            var camPos = Camera.main.transform.position;
            camPos.y = transform.position.y;
            this.transform.LookAt(camPos, Vector3.up);
        }
       
    }
}
