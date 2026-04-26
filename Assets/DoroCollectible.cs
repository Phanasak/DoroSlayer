using UnityEngine;

public class DoroCollectible : MonoBehaviour
{
    private bool canCollect = false;

    public void OnDoroDied()
    {
        canCollect = true;
        var col = gameObject.AddComponent<CircleCollider2D>();
        col.radius = 0.6f;
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canCollect) return;
        if (other.CompareTag("Player"))
        {
            DoroCounterManager.Instance.AddDoro();
            Destroy(gameObject);
        }
    }
}