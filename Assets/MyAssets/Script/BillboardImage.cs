using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardImage : MonoBehaviour
{
    private void OnEnable()
    {
        
        if (!GetComponent<Renderer>().isVisible)
        {
            enabled = false;
        }
    }

    private void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform.position, Vector3.up);
        var camPos = Camera.main.transform.position;
        camPos.y = transform.position.y;
        transform.LookAt(camPos, Vector3.up);
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }
}
