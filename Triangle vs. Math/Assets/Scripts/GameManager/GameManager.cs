using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    public GameObject MainMenuScreen;
    public GameObject ControlsScreen;
    public PlayerManager playerManager;
    public MrMath mrMath;



    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void GameWon()
    {
        gameWonScreen.SetActive(true);
    }
    
    public void DestroyEnemy()
    {
        Destroy(mrMath.gameObject);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void DiePlayer()
    {
        Destroy(playerManager.gameObject);
    }

    public void StopEnemyAttack()
    {
        mrMath.StopAttacking();
    }

    public void OnClickControls()
    {
        MainMenuScreen?.SetActive(false);
        ControlsScreen.SetActive(true);
    }

    public void OnClickBackFromControl()
    {
        ControlsScreen.SetActive(false);
        MainMenuScreen?.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
