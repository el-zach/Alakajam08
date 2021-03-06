﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    public int health=1000;
    public float damageMultiplier = 1f;
    public GameObject destroyOnDeath;
    public Color bleedColor = Color.white;
    public FloatEvent OnDamage;
    public UnityEvent OnDeath;

    public int Damage(int value)
    {
        value = Mathf.FloorToInt(value * damageMultiplier);
        OnDamage.Invoke(value);
        health -= value;
        if (health <= 0)
            Death();
        //Debug.Log("Got damaged for " + value);
        return value;
    }

    void Death()
    {
        OnDeath.Invoke();
        if (destroyOnDeath) Destroy(destroyOnDeath);
        DamageVFX.DeathAt(transform.position,-transform.forward);
    }


}
