using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private bool pickUp = false;
    [SerializeField]
    private bool Detach = false;
    // Start is called before the first frame update
    private Rigidbody rb;

    void Start()
    {
        if (this.GetComponent<Rigidbody>() != null)
        {
            rb = this.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void SeedCollected()
    {
        Destroy(this.gameObject);
    }
    public void ActiveSeedPhysic()
    {
        if(rb != null)
        {
            Invoke("DelayAddPhysic",Random.Range(0.0f,0.75f));
            
        }
    }

    private void DelayAddPhysic()
    {
        rb.useGravity = true;
    }
}
