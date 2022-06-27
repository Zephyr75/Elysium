using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameManager manager;

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
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
