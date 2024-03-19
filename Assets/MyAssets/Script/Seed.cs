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

    void Start()
    {
        
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
}
