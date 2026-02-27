using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixYPos : MonoBehaviour
{
    [SerializeField]
    private float offSetValue;

    // T = On Ground
    // F= On Top of object
    [SerializeField]
    private bool isOnGround;
    [SerializeField] 
    private bool isFixedRotation;
    [SerializeField]
    private GameObject parentObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private Vector3 newPos;
    private void FixedUpdate()
    {
        if (isOnGround)
        {
            newPos = new Vector3(parentObj.transform.position.x, 0f+offSetValue, parentObj.transform.position.z);
            this.gameObject.transform.position = newPos;
        }
        else
        {
            newPos = new Vector3(parentObj.transform.position.x, parentObj.transform.position.y + offSetValue, parentObj.transform.position.z);
        }
        this.gameObject.transform.position = newPos;

        if (isFixedRotation)
        {
            transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
