using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedStorage : MonoBehaviour
{
    [SerializeField]
    string colliderTag = "Seed";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag.Equals(colliderTag, System.StringComparison.InvariantCultureIgnoreCase))
        {
            
            CollectSeed(collision.gameObject);
        }

    }
    private void CollectSeed(GameObject seed)
    {
        Seed seedScript = seed.GetComponent<Seed>();
        //Debug.Log(seedScript);

        
        if (!seedScript.PickUpState() && seedScript.DetachSeedStatus())
        {
            seedScript.SeedCollected();
        }
    }
}
