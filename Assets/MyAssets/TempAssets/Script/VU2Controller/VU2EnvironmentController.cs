using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2EnvironmentController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backgrounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowEnvironment(int i)
    {
        HideAllEnv();
        backgrounds[i-1].SetActive(true);    
    }
    private void HideAllEnv()
    {
        foreach (GameObject env in backgrounds)
        {
            env.SetActive(false);
        }
    }
}
