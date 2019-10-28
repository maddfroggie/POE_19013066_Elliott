using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBuilding : MonoBehaviour
{
    [SerializeField]
    private float health = 3;

    public delegate void LivingEntitySpawned(LivingEntity spawned);
    public event LivingEntitySpawned OnSpawned;

    private bool isDead = false;

    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void PostSpawnEvent(LivingEntity spawned)
    {
        OnSpawned(spawned);
    }

    public virtual void QueueUnit()
    {

    }

    private void Die()
    {
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


