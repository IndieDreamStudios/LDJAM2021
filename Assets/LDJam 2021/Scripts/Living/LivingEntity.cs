using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{

    [SerializeField] protected float health = 100f;
    protected float damageValue = 25f;
    protected float speed = 2f;
    protected bool isDead = false;

    public virtual void Damage(float amount)
    {
        if (health - amount > 0)
            health -= amount;
        else
        {
            health = 0;
            isDead = true;
        }
    }

    public float GetHealth() => health;
}
