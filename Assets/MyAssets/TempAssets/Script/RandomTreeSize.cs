using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTreeSize : MonoBehaviour
{
    [SerializeField]
    private float weightRation = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        RandomSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void RandomSize()
    {
        float rNum = Random.Range(0f, 1f);
        if(rNum <= weightRation)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
