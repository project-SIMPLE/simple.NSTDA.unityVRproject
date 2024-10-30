using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seed : MonoBehaviour
{
    /// <summary>
    /// Apple = 1
    /// Orange = 2
    /// Mango = 3
    /// </summary>
    [SerializeField]
    private int seedID;
    [SerializeField]
    private bool pickUp = false;
    [SerializeField]
    private bool Detach = false;
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody rb;
    private Transform originTransform;

    private void Awake()
    {
        originTransform = this.gameObject.transform;
    }
    private void Start()
    {
        if (this.GetComponent<Rigidbody>() != null)
        {
            rb = this.GetComponent<Rigidbody>();
        }
    }
    private void OnDestroy()
    {
            
    }
    public int GetSeedID()
    {
        return seedID;
    }

    public void DetachSeed()
    {
        Detach = true;
    }
    public void Grabed(bool t)
    {
        pickUp = t;
    }

    public bool PickUpState()
    {
        return pickUp;
    }
    public bool DetachSeedStatus()
    {
        return Detach;
    }
    private void ResetSeedPosition()
    {
        this.gameObject.SetActive(true);
    }
    public void SeedCollected()
    {
        Destroy(this.gameObject);
        //this.gameObject.transform.position = originTransform.position;
        //this.gameObject.transform.rotation = originTransform.rotation;
        //this.gameObject.SetActive(false);
        
    }
    public void ActiveSeedPhysic()
    {
        if(rb != null)
        {
            Invoke("DelayAddPhysic",Random.Range(0.0f,0.5f));
            
        }
    }

    private void DelayAddPhysic()
    {
        rb.useGravity = true;
    }

    public void ActiveSeedObject()
    {
        Invoke("DealyActive", Random.Range(0.0f, 0.5f));
    }
    private void DealyActive()
    {
        this.gameObject.SetActive(true);
        if (rb != null)
        {
            DelayAddPhysic();
        }
    }
}
