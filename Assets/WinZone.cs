using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าวัตถุที่ชนคือผู้เล่น
        if (collision.CompareTag("Player"))
        {
            // เรียกเหตุการณ์ชนะเกม
            GameManager.Instance.PlayerWin();
        }
    }
}