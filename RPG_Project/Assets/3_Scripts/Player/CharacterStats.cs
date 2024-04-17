using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int currentHealth;
    public int currentStamina;

    public Stats damage;
    public Stats maxHealth;
    public Stats strength;
    public Stats maxStamina;

    public float HealthPercentage
    {
        get
        {
            int health = currentHealth;
            int maxHealth = this.maxHealth.GetValue();

            return (maxHealth > 0 ? ((float)health / (float)maxHealth) : 0f);
        }
    }

    public float StaminaPercentage
    {
        get
        {
            int stamina = currentStamina;
            int maxStamina = this.maxStamina.GetValue();

            return (maxStamina > 0 ? ((float)stamina / (float)maxStamina) : 0f);
        }
    }


    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
        currentStamina = maxStamina.GetValue();
    }

}
