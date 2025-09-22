using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VU2PlantableTree : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private XRGrabInteractable grabScript;
    [SerializeField]
    private bool onSocket = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlantSocket"))
        {
            onSocket = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlantSocket"))
        {
            onSocket = false;
        }
    }
    public void OnReleaseObject()
    {
        if (onSocket)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            grabScript.enabled = false;
        }
    }
}
