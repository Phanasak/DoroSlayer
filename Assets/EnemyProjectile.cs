using UnityEngine;
using System;

public class EnemyProjectile : MonoBehaviour
{
    [HideInInspector] public float speed = 8f;
    [HideInInspector] public int damage = 1;
    public float lifetime = 3f;

    public event Action OnProjectileDestroyed;

    private Vector2 direction;
    private float lifeTimer;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;

        // หมุน Sprite ให้หันไปทางทิศทางยิง
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Update()
    {
        // เคลื่อนที่กระสุน
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // จับเวลาทำลายกระสุน
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            DestroyProjectile();
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        OnProjectileDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnProjectileDestroyed?.Invoke();
    }
}