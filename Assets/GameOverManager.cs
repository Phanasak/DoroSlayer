using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Health playerHealth;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        playerHealth.OnDeath += ShowGameOver;
    }

    public void PlayerWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;

        // ✅ เพิ่มบรรทัดนี้
        if (DoroAnalyticsManager.Instance != null)
            DoroAnalyticsManager.Instance.TrackPlayerWin(playerHealth.currentHealth);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        // ✅ เพิ่มบรรทัดนี้
        if (DoroAnalyticsManager.Instance != null)
            DoroAnalyticsManager.Instance.TrackPlayerDeath(playerHealth.currentHealth);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}