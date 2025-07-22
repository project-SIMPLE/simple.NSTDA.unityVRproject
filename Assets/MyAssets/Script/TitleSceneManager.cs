using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private string host = "127.0.0.1";
    [SerializeField] private string portWithMiddleware = "8080";
    private bool useMiddleWare = true;

    [SerializeField] private TMP_InputField IP;
    [SerializeField] private TMP_InputField Port;

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

    public void SaveOnlineDeta()
    {
        PlayerPrefs.SetString("IP", IP.text);
        PlayerPrefs.SetString("PORT", Port.text);
        PlayerPrefs.Save();
    }

    public void GotoScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
