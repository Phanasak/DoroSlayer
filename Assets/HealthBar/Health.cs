using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {  get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private IEnumerable<Behaviour> components;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public event Action OnDeath;

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if(!dead)
            {
                

                anim.SetTrigger("Death");

                //Player
                if (GetComponent<HeroKnight>() != null)
                    GetComponent<HeroKnight>().enabled = false;
                
                //Enemy
                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                 GetComponent<MeleeEnemy>().enabled = false;
                // เพิ่มส่วนจัดการ RangeEnemy
                if (GetComponent<RangeEnemy>() != null)
                {
                    GetComponent<RangeEnemy>().OnEnemyDeath(); // เรียกเมธอดเมื่อตาย
                    GetComponent<RangeEnemy>().enabled = false;
                }


                dead = true;

                OnDeath?.Invoke();
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
       
        anim.ResetTrigger("Death");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    // ในสคริปต์ที่จัดการสุขภาพศัตรู (เช่นเมื่อ HP <= 0)
    
}
