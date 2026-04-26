using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SetEasy() => SetDifficulty(DifficultyManager.Difficulty.Easy);
    public void SetNormal() => SetDifficulty(DifficultyManager.Difficulty.Normal);
    public void SetHard() => SetDifficulty(DifficultyManager.Difficulty.Hard);

    void SetDifficulty(DifficultyManager.Difficulty diff)
    {
        DifficultyManager.Instance.CurrentDifficulty = diff;
        Debug.Log($"Difficulty set: {diff}");
    }

    public void Play()
    {
        DoroAnalyticsManager.Instance.TrackGameStart(); // ✅ เพิ่มบรรทัดนี้
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}