using UnityEngine;

public class SaveGameData
{
    public int LastScore;
    public int HighestScore;
    public int TotalCherriesCollected;
}

public class GameSaver : MonoBehaviour
{
    private const string LastScoreKey = "LastScore";
    private const string HighestScoreKey = "HighestScore";
    private const string TotalCherriesCollectedKey = "CherriesCollected";

    public SaveGameData CurrentSave { get; private set; }

    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        PlayerPrefs.SetInt(LastScoreKey, CurrentSave.LastScore);
        PlayerPrefs.SetInt(HighestScoreKey, CurrentSave.HighestScore);
        PlayerPrefs.SetInt(TotalCherriesCollectedKey, CurrentSave.TotalCherriesCollected);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        CurrentSave = new SaveGameData
        {
            LastScore = PlayerPrefs.GetInt(LastScoreKey, 0),
            HighestScore = PlayerPrefs.GetInt(HighestScoreKey, 0),
            TotalCherriesCollected = PlayerPrefs.GetInt(TotalCherriesCollectedKey, 0)
        };
    }
}
