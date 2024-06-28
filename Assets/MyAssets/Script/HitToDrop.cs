using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitToDrop : MonoBehaviour
{
    [SerializeField]
    string hitObjectTag = "Tools";

    [SerializeField]
    Rigidbody hitRigibody;


    // Start is called before the first frame update
    void Start()
    {
        if(hitRigibody == null)
        {
            hitRigibody = GetComponent<Rigidbody>();
        }
        hitRigibody.useGravity = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {

      
        if (collision.gameObject.tag.Equals(hitObjectTag, System.StringComparison.InvariantCultureIgnoreCase))
        {
            ActiveHit();
        }

    }
    public void ActiveHit()
    {
        hitRigibody.useGravity = true;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
