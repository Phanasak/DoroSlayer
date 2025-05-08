using UnityEngine;
using System.Collections.Generic;

public class RangeEnemy : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackCooldown = 2f;
    public float detectionRange = 5f;
    public int damage = 1;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 8f;

    [Header("Death Animation")]
    [SerializeField] private string deathTrigger = "Death"; // ���� Trigger � Animator

    private float cooldownTimer;
    private Transform player;
    private List<GameObject> activeProjectiles = new List<GameObject>();
    private Animator anim;
    private bool isDead = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cooldownTimer = attackCooldown;
    }

    private void Update()
    {
        if (isDead) return; // ��ش�ӧҹ��ҵ������

        cooldownTimer += Time.deltaTime;

        if (PlayerInRange() && cooldownTimer >= attackCooldown)
        {
            Shoot();
            cooldownTimer = 0f;
        }
    }

    private bool PlayerInRange()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null || player == null)
        {
            Debug.LogError("Missing references in RangeEnemy!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        activeProjectiles.Add(projectile);

        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            projectileScript.SetDirection(direction);
            projectileScript.speed = projectileSpeed;
            projectileScript.damage = damage;

            projectileScript.OnProjectileDestroyed += () =>
            {
                activeProjectiles.Remove(projectile);
            };
        }
        else
        {
            Debug.LogError("EnemyProjectile script missing on projectile prefab!");
        }
    }

    public void OnEnemyDeath()
    {
        if (isDead) return;

        isDead = true;

        // ��� Animation �͹���
        if (anim != null && !string.IsNullOrEmpty(deathTrigger))
        {
            anim.SetTrigger(deathTrigger);
        }

        // ����¡���ع������
        foreach (var projectile in activeProjectiles.ToArray())
        {
            if (projectile != null)
            {
                Destroy(projectile);
            }
        }
        activeProjectiles.Clear();

        // �Դ��÷ӧҹ�ͧʤ�Ի��
        enabled = false;

        // ������ѵ����ѧ�ҡ��� Animation ���� (�Ҩ�� Animation Event ᷹)
        // Destroy(gameObject, 1f); // ź�͡��ѧ�ҡ 1 �Թҷ�
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}