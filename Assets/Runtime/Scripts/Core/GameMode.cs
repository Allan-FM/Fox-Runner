using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnimation;
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private float reloadGameDelay = 3;

    [SerializeField] 
    [Range(0,5)]
    private int startGameCountDown = 5;
    private void Awake()
    {
        player.enabled = false;
        mainHUD.ShowStartOvelay();
    }
    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
        private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator StartGameCor()
    {
        yield return StartCoroutine(mainHUD.PlayerStartGameCountdown(startGameCountDown));
        playerAnimation.PlayerStartGameAnimation();
    }
}
