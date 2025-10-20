using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2EnvironmentController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backgrounds;

    [SerializeField]
    private Material[] skybox;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += OnRaining;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= OnRaining;
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
    private void OnRaining(bool isRain)
    {
        if(isRain)
        {
            ChangeSkyBox(1);
        }
        else
        {
            ChangeSkyBox(0);
        }
    }

    private void ChangeSkyBox(int id)
    {
        RenderSettings.skybox = skybox[id];
        DynamicGI.UpdateEnvironment();
    }
}
