using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBaseOnSeasons : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectListInSeasons;

    private void OnEnable()
    {
        OnlineModeGameManager.Instance.OnSetSeason += ActiveObjectBaseOnSeason;
    }
    private void OnDisable()
    {
        OnlineModeGameManager.Instance.OnSetSeason -= ActiveObjectBaseOnSeason;
    }

    private void ActiveObjectBaseOnSeason(int i)
    {
        foreach( GameObject obj in objectListInSeasons)
        {
            obj.SetActive(false);
        }
        if (objectListInSeasons[i]!= null)
        {
            objectListInSeasons[i].SetActive(true);
        }
    }
}
