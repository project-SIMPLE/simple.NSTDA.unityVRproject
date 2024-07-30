using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DegradableObject : MonoBehaviour
{
    [SerializeField]
    private int degradeLevel;

    // Start is called before the first frame update
    void Start()
    {
        degradeLevel = 0;
    }

    public void ResetdegradeLevel()
    {
        degradeLevel = 0;
        DeactiveAllChild();
        UpdateEnvironment();
    }
    public void IncreadeDegradeLevel()
    {
        DeactiveAllChild();
        degradeLevel++;
        UpdateEnvironment();
    }
    private void UpdateEnvironment()
    {
        this.gameObject.transform.GetChild(degradeLevel).gameObject.SetActive(true);
    }
    private void DeactiveAllChild()
    {
        for(int i = 0; i< this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
