using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineModeUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    // Start is called before the first frame update

    private void OnEnable()
    {
        OnlineModeGameManager.Instance.OnGameStart += GameStartUI;
        OnlineModeGameManager.Instance.OnGameStop += GameStopUI;
    }
    private void OnDisable()
    {
        OnlineModeGameManager.Instance.OnGameStart -= GameStartUI;
        OnlineModeGameManager.Instance.OnGameStop -= GameStopUI;
    }
    void Start()
    {
        
    }
    private void GameStopUI()
    {
        pauseMenu.SetActive(true);
    }
    private void GameStartUI()
    {
        pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
