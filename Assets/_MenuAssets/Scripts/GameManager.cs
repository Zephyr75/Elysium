using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;
public class GameManager : MonoBehaviour
{

    public static bool isPaused = false;
    private static int playerHealth = 10;

    public static float transitionTime = .4f;
    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        /*
        if (keyboard.escapeKey.wasPressedThisFrame || gamepad.selectButton.wasPressedThisFrame){
            if (isPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
        */
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        //Time.timeScale = 1;
        isPaused = false;
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        //Time.timeScale = 0;
        isPaused = true;
    }

    public void RemoveHealth(int damage){
        playerHealth -= damage;
        if (playerHealth <= 0){
            DeathPlayer();
        }
    }

    public void DeathPlayer(){
        print("Player died");
    }

    public void AddHealth(int health){
        playerHealth += health;
    }

    public int GetHealth(){
        return playerHealth;
    }

    public void BackToMenu(){
        StartCoroutine(BackToMenuCoroutine());
    }

    IEnumerator BackToMenuCoroutine(){
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("MainMenu");
    }
}
