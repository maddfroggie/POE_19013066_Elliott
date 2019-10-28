using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField]
    protected float startingHealth = 3f;

    protected float health;
    protected bool isDead;


    public delegate void VoidAction();
    public event VoidAction OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
        isDead = false;
    }

    protected virtual void Update()
    {

    }

    protected virtual void Die()
    {
        isDead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }

    //=============== IDamageable functions ===============//
    public void TakeDamage(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

}