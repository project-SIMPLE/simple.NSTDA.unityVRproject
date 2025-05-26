using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem waterStream;
    [SerializeField]
    private bool isShoot;
    // Start is called before the first frame update
    void Start()
    {
        isShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootWater()
    {
        if (waterStream == null) return;
        
        if(!isShoot)
        {
         
            waterStream.Play();
            isShoot=true;
        }
    }
    public void StopShoot()
    {
        if (waterStream == null) return;

        if (isShoot)
        {
            waterStream.Stop();
            isShoot = false;
        }
    
    }
}
