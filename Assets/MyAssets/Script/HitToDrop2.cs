using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitToDrop2 : MonoBehaviour
{
    [SerializeField]
    string hitObjectTag = "Tools";
    [SerializeField]
    string hitObjectTag2 = "Hand";
    [SerializeField]
    GameObject fruitPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.tag.Equals(hitObjectTag, System.StringComparison.InvariantCultureIgnoreCase)||
            other.gameObject.tag.Equals(hitObjectTag2, System.StringComparison.InvariantCultureIgnoreCase))
        {
            ActiveHit();
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("HiTTTTT1");
        if (collision.gameObject.tag.Equals(hitObjectTag, System.StringComparison.InvariantCultureIgnoreCase) ||
            collision.gameObject.tag.Equals(hitObjectTag2, System.StringComparison.InvariantCultureIgnoreCase))
        {
            ActiveHit();
            
        }

    }
    public void ActiveHit()
    {
        if(fruitPrefab != null)
        {
            Instantiate(fruitPrefab,this.transform.position,this.transform.rotation);
        }
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
