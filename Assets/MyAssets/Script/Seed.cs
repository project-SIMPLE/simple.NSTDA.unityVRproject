using System.Collections;
using System.Collections.Generic;
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
        TestbedManager.instance.OnResetSeedPosition += ResetSeedPosition;
    }
    private void OnDestroy()
    {
        TestbedManager.instance.OnResetSeedPosition -= ResetSeedPosition;
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
        //Destroy(this.gameObject);
        this.gameObject.transform.position = originTransform.position;
        this.gameObject.transform.rotation = originTransform.rotation;
        this.gameObject.SetActive(false);
        
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
}
