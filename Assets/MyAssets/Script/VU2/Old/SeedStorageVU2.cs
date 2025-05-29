using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedStorageVU2 : MonoBehaviour
{
    [SerializeField]
    string colliderTag = "Seed";
    
    [SerializeField]
    private AudioSource soundEffect;
    [SerializeField]
    private int count;
    [SerializeField]
    private int storageSize = 10;
    [SerializeField]
    private bool isFull;

    [SerializeField]
    private GameObject basketFullModel;

    // Start is called before the first frame update
    void Start()
    {
        isFull = false;
        count = 0;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("SeedCollectionZone", System.StringComparison.InvariantCultureIgnoreCase))
        {
            SendSeedsToZone2();
        }
    }

    private void CollectSeed(GameObject seed)
    {
        Seed seedScript = seed.GetComponent<Seed>();

        if (!isFull)
        {

            //VU2ForestProtectionEventManager.Instance?.PutFruitIntoBucket(seed.name);
            //seedScript.SeedCollected();
            soundEffect.Play();

            count++;
            if(storageSize == count)
            {
                isFull=true;
                basketFullModel.SetActive(true);
            }
        }
        
    }

    private void SendSeedsToZone2()
    {
        //VU2ForestProtectionEventManager.Instance?.LoadSeedToNextZone();

        count = 0;
        isFull = false;
        basketFullModel.SetActive(false);
    }
}
