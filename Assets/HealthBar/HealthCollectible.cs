using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Health playerHealth = collision.GetComponent<Health>();

            // ✅ เพิ่ม: Track ก่อน AddHealth เพื่อเก็บ HP ก่อนเก็บของ
            if (DoroAnalyticsManager.Instance != null)
                DoroAnalyticsManager.Instance.TrackHealthCollected(healthValue, playerHealth.currentHealth);

            playerHealth.AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
