using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2PlantableItem : MonoBehaviour
{
    [SerializeField]
    private bool isReleaseFromHand;
    [SerializeField]
    private int PlantableID;
    [SerializeField]
    private bool DestroyAfterPass = false;
    private float timeTilDestroy = 60f;

    private void OnEnable()
    {
        isReleaseFromHand = false;
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private float timeCount = 0;
    private void FixedUpdate()
    {
        if(DestroyAfterPass)
        {
            if(timeCount >= timeTilDestroy)
            {
                timeCount = 0;
                Destroy(gameObject);
            }
            timeCount += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

    public void OnReleaseByHand()
    {
        isReleaseFromHand = true;
    }

    public bool CanItPlaceInSocket()
    {
        return isReleaseFromHand;
    }
    public int GetPlantableID()
    {
        return PlantableID;
    }
}
