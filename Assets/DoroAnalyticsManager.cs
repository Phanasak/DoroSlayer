using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class DoroAnalyticsManager : MonoBehaviour
{
    public static DoroAnalyticsManager Instance;

    // ตัวแปรเก็บสถิติระหว่างเกม
    public int enemiesKilled { get; private set; } = 0;
    private float gameStartTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log("[Analytics] Ready!");
    }

    // ========== game_start ==========
    public void TrackGameStart()
    {
        enemiesKilled = 0;
        gameStartTime = Time.time;

        CustomEvent evt = new CustomEvent("game_start");
        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
        Debug.Log("[Analytics] game_start sent");
    }

    // ========== player_death ==========
    public void TrackPlayerDeath(float currentHp)
    {
        float timeSurvived = Time.time - gameStartTime;

        CustomEvent evt = new CustomEvent("player_death")
        {
            { "time_survived", timeSurvived },
            { "enemies_killed", enemiesKilled }
        };
        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
        Debug.Log($"[Analytics] player_death | time={timeSurvived:F1}s | kills={enemiesKilled}");
    }

    // ========== player_win ==========
    public void TrackPlayerWin(float currentHp)
    {
        float timeToWin = Time.time - gameStartTime;

        CustomEvent evt = new CustomEvent("player_win")
        {
            { "time_to_win", timeToWin },
            { "enemies_killed", enemiesKilled },
            { "hp_remaining", currentHp }
        };
        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
        Debug.Log($"[Analytics] player_win | time={timeToWin:F1}s | kills={enemiesKilled} | hp={currentHp}");
    }

    // ========== enemy_killed ==========
    public void TrackEnemyKilled(string enemyType, float playerHp)
    {
        enemiesKilled++;

        CustomEvent evt = new CustomEvent("enemy_killed")
        {
            { "enemy_type", enemyType },
            { "player_hp_at_kill", playerHp }
        };
        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
        Debug.Log($"[Analytics] enemy_killed | type={enemyType} | playerHp={playerHp}");
    }

    // ========== health_collected ==========
    public void TrackHealthCollected(float healthValue, float playerHpBefore)
    {
        CustomEvent evt = new CustomEvent("health_collected")
        {
            { "health_value", healthValue },
            { "player_hp_before", playerHpBefore }
        };
        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
        Debug.Log($"[Analytics] health_collected | value={healthValue} | hpBefore={playerHpBefore}");
    }

    // ========== player_took_damage ==========
    public void TrackPlayerTookDamage(float damageAmount, float hpRemaining)
    {
        CustomEvent evt = new CustomEvent("player_took_damage")
        {
            { "damage_amount", damageAmount },
            { "hp_remaining", hpRemaining }
        };
        AnalyticsService.Instance.RecordEvent(evt);
        AnalyticsService.Instance.Flush();
        Debug.Log($"[Analytics] player_took_damage | dmg={damageAmount} | hpLeft={hpRemaining}");
    }
}