using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSelector : MonoBehaviour
{
    private void Start()
    {
        int id = PlayerPrefs.GetInt("LocalKey", 0);
        
        ChangeLocale(id);
    }

    private bool active = false;
    public void ChangeLocale(int ID)
    {
        if (active) return;
        StartCoroutine(SetLocale(ID));
    }

    /*
     * 0 english
     * 1 Thai
     * */
    IEnumerator SetLocale(int localID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localID];
        PlayerPrefs.SetInt("LocalKey",localID);
        active = false;
    }

}
