using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedCutter : MonoBehaviour
{
    [SerializeField]
    private bool isOnHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsItemOnPlayerHand()
    {
        return isOnHand;
    }

    public void OnPlayerGrabbed(bool grabbed)
    {
        isOnHand = grabbed;
    }
}
