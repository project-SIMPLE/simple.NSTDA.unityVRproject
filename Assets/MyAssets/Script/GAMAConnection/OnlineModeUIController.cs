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
        
        
        
    }
    
    void Start()
    {

        OnlineModeGameManager.Instance.OnTutorialStart += HideGamePauseUI;
        //OnlineModeGameManager.Instance.OnGameStart += GameStartUI;
        OnlineModeGameManager.Instance.OnGameStop += ShowGamePauseUI;
    }
    private void OnDisable()
    {

        OnlineModeGameManager.Instance.OnTutorialStart -= HideGamePauseUI;
        //OnlineModeGameManager.Instance.OnGameStart -= GameStartUI;
        OnlineModeGameManager.Instance.OnGameStop -= ShowGamePauseUI;
    }
    private void ShowGamePauseUI()
    {
        pauseMenu.SetActive(true);
    }
    private void HideGamePauseUI()
    {
        pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
