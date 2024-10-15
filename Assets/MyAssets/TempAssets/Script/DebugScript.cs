using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestHoverON()
    {
        Debug.Log("Hover on Something");
    }
    public void TestHoverExit()
    {
        Debug.Log("Exit Hovering on Something");
    }
    public void ChangeMaterial(Material m)
    {
        if (this.gameObject.transform.GetComponent<MeshRenderer>() == null) return;
        this.gameObject.transform.GetComponent<MeshRenderer>().material = m;
    }

}
