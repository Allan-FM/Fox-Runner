using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    private void Awake()
    {
        ShowHudOverlay();
    }
    private void LateUpdate()
    {
        scoreText.text = $"Score : {player.Score}";    
        distanceText.text = $"{Mathf.RoundToInt(player.TravelleDistance)}m";
    }
    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }
    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowHudOverlay();
    } 

    private void ShowHudOverlay()
    {
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(true);
    }
    private void ShowPauseOverlay()
    {
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
    }
}
