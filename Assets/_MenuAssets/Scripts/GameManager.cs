using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool isPaused = false;
    private int playerHealth = 10;
    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void RemovePlayer(int damage){
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
        SceneManager.LoadScene("Menu");
    }
}
