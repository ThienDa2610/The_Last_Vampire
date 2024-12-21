using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;

    }

    void Update()
    {

    }
    public void HasDamage(float damage)
    {
        if (damage <= 0) { return; }
       
        currentHealth -= damage;

    }
   
}
