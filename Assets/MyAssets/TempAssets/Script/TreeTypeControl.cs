using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTypeControl : MonoBehaviour
{
    [SerializeField]
    private bool haveFruit;
    [SerializeField]
    private bool haveMultiHeight;

    [SerializeField]
    private GameObject currentActiveType;
    // Start is called before the first frame update
    void Start()
    {
        if (haveFruit)
        {
            currentActiveType = this.gameObject.transform.GetChild(0).gameObject;
        }
        else
        {
            currentActiveType = this.gameObject.transform.GetChild(1).gameObject;
        }
        currentActiveType.SetActive(true);

        if(haveMultiHeight)
        {
            RandomActiveType();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void RandomActiveType()
    {
        int randomIndex = Random.Range(0,currentActiveType.transform.childCount);
        currentActiveType.transform.GetChild(randomIndex).gameObject.SetActive(true);
    }
}
