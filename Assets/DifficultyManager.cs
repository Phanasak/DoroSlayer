using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty CurrentDifficulty = Difficulty.Easy;

    // HP ของ Doro แต่ละโหมด
    public int EasyHP = 100;  // ใช้ค่าเดิมที่ทำอยู่
    public int NormalHP = 200;
    public int HardHP = 350;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public int GetDoroHP()
    {
        return CurrentDifficulty switch
        {
            Difficulty.Easy => EasyHP,
            Difficulty.Normal => NormalHP,
            Difficulty.Hard => HardHP,
            _ => EasyHP
        };
    }
}