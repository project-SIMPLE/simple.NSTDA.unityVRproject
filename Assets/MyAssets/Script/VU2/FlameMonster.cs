using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameMonster : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider flameHitBox;
    [SerializeField]
    private ParticleSystem[] fireRingParticles;

    [SerializeField]
    private float auraSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        auraSize += 0.1f * Time.deltaTime;
        IncreaseFlameSize();
    }

    private void IncreaseFlameSize()
    {
        flameHitBox.radius = auraSize * 2;
        foreach (ParticleSystem p in fireRingParticles)
        {
            var shape = p.shape;
            shape.radius = auraSize;
        }
    }
}
