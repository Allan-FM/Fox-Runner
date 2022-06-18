using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void LateUpdate()
    {
        scoreText.text = $"Score : {player.Score}";    
    }
}
