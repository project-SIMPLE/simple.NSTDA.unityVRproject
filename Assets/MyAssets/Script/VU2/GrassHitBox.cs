using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrassHitBox : MonoBehaviour
{
    public UnityEvent onGrassRemove;
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
        if (collision.gameObject.tag == "Tools" || collision.gameObject.tag == "Fire")
        {
            GrassRemove();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tools" || other.gameObject.tag == "Fire")
        {
            GrassRemove();
        }
    }
    private void GrassRemove()
    {
        onGrassRemove.Invoke();
        this.gameObject.SetActive(false);
        
    }
}
