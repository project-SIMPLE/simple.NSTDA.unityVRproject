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

        
        if (collision.gameObject.tag.Equals(colliderTag, System.StringComparison.InvariantCultureIgnoreCase))
        {
            //Debug.Log("Collect!!!!");
            CollectSeed(collision.gameObject);
        }

    }
    private void CollectSeed(GameObject seed)
    {
        Seed seedScript = seed.GetComponent<Seed>();
        //Debug.Log(seedScript);

        
        if (!seedScript.PickUpState() && seedScript.DetachSeedStatus())
        {
            if (TestbedManager.instance != null)
            {
                TestbedManager.instance.SeedCollected(seedScript.GetSeedID());
            }
            if (OnlineModeGameManager.Instance != null)
            {
                OnlineModeGameManager.Instance?.SeedCollected(seedScript.GetSeedID());
            }
            seedScript.SeedCollected();
            soundEffect.Play();
            
        }
    }
}
