using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TitleSceneManager : MonoBehaviour
{
    [Header("IP address & Port")]
    [SerializeField] private string host = "127.0.0.1";
    [SerializeField] private string portWithMiddleware = "8080";
    private bool useMiddleWare = true;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField IP;
    [SerializeField] private TMP_InputField Port;

    [Header("Menu")]
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private UnityEngine.UI.Slider progressBar;

    public void OpenOnlinePannel()
    {
        useMiddleWare = true;
        if(!PlayerPrefs.HasKey("MIDDLEWARE")) PlayerPrefs.SetString("MIDDLEWARE", "Y");
        if (!PlayerPrefs.HasKey("IP"))
        {
            IP.text = host;
            PlayerPrefs.SetString("IP", host);
        }
        else
        {
            IP.text = PlayerPrefs.GetString("IP");
        }

        if (!PlayerPrefs.HasKey("PORT"))
        {
            Port.text = portWithMiddleware;
            PlayerPrefs.SetString("PORT", portWithMiddleware);
        }
        else
        {
            Port.text = PlayerPrefs.GetString("PORT");
        }

    }
    public void SetGame1OfflineStartingPeriod(int period)
    {
        
        PlayerPrefs.SetInt("SeedCollectionStage", period);
        PlayerPrefs.Save();
    }


    public void SaveOnlineDeta()
    {
        PlayerPrefs.SetString("IP", IP.text);
        PlayerPrefs.SetString("PORT", Port.text);
        PlayerPrefs.Save();
    }

    

    public void IPKeyboard(string key)
    {
        if (key == null || key.Length == 0) return;
        if (key == "DELETE")
        {
            if(IP.text.Length > 0) IP.text = IP.text.Substring(0, IP.text.Length - 1);
        }
        else
        {
            IP.text += key;
        }
    }
    public void PortKeyboard(string key)
    {
        if (key == null || key.Length == 0) return;
        if (key == "DELETE")
        {
            if (Port.text.Length > 0) Port.text = Port.text.Substring(0, Port.text.Length - 1);
        }
        else
        {
            Port.text += key;
        }
    }

    public void GotoScene(string name)
    {
        mainUI.SetActive(false);
        loadingUI.SetActive(true);
        //SceneManager.LoadScene(name);
        StartCoroutine(LoadingSceneAsync(name));
    }
    IEnumerator LoadingSceneAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress/0.9f);
            progressBar.value = progress;
            yield return null;
        }
    }
}
