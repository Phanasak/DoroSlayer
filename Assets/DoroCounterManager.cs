using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoroCounterManager : MonoBehaviour
{
    public static DoroCounterManager Instance;

    [Header("UI")]
    public TextMeshProUGUI counterText; // ลาก Text object มาใส่ใน Inspector
    public Animator counterAnimator;    // optional: animation ตอนเพิ่ม

    private int doroCount = 0;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(counterText.transform.root.gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() => UpdateUI();

    public void AddDoro()
    {
        doroCount++;
        UpdateUI();
        if (counterAnimator) counterAnimator.SetTrigger("Pop"); // ถ้ามี anim
    }

    void UpdateUI()
    {
        if (counterText)
            counterText.text = $"Doro Collected: {doroCount}";
    }
}