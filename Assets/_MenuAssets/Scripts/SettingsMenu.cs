using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu, settingsMenu;

    public void CloseSettings()
    {
        StartCoroutine(CloseSettingsCoroutine());
    }

    IEnumerator CloseSettingsCoroutine(){
        yield return new WaitForSeconds(GameManager.transitionTime);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
