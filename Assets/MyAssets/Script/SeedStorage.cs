using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedStorage : MonoBehaviour
{
    [SerializeField]
    string colliderTag = "Seed";
    [SerializeField]
    private AudioSource soundEffect;
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
        //Debug.Log("Hit collision+++");

        if (collision.gameObject.tag.Equals(colliderTag, System.StringComparison.InvariantCultureIgnoreCase))
        {
            //Debug.Log("Hit Seed Storage");
            CollectSeed(collision.gameObject);
        }

    }
    private void CollectSeed(GameObject seed)
    {
        Seed seedScript = seed.GetComponent<Seed>();

        
        if (!seedScript.PickUpState() && seedScript.DetachSeedStatus())
        {
            if (TestbedManager.instance != null)
            {
                //Debug.Log("Call Manager 1");
                TestbedManager.instance.SeedCollected(seedScript.GetSeedID());
            }
            if (OnlineModeGameManager.Instance != null)
            {
                Debug.Log("Call Manager 2");
                OnlineModeGameManager.Instance.SeedCollected(seedScript.GetSeedID());
            }
            
            seedScript.SeedCollected();
            soundEffect.Play();
            
        }
    }
}
