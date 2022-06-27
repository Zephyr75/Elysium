using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu, settingsMenu;
    [SerializeField] private GameManager manager;


    
    public void OpenSettings()
    {
        StartCoroutine(OpenSettingsCoroutine());
    }
    IEnumerator OpenSettingsCoroutine(){
        yield return new WaitForSeconds(GameManager.transitionTime);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Resume()
    {
        manager.Resume();
    }

    public void BackToMenu()
    {
        manager.BackToMenu();
    }
    
}
