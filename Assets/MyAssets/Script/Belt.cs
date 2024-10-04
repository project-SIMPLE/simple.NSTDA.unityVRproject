using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public GameObject HMD;
    //public GameObject[] itemSlot;

    [Range(0.01f,1f)]
    public float heightRation;
    private Vector3 cHMDPos;
    private Quaternion cHMDRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cHMDPos = HMD.transform.position;
        cHMDRot = HMD.transform.rotation;
        UpdateBelt();
    }
    /*
    private void UpdateitemSlot(GameObject item)
    {
        item.gameObject.transform.position = new Vector3(item.gameObject.transform.position.x,
            item.gameObject.transform.position.y * heightRation,
            item.gameObject.transform.position.z);
    }*/
    private void UpdateBelt()
    {
        this.transform.position = new Vector3(cHMDPos.x,cHMDPos.y*heightRation, cHMDPos.z);
        this.transform.rotation = new Quaternion(this.transform.rotation.x, cHMDRot.y,this.transform.rotation.z,cHMDRot.w);
    }
}
