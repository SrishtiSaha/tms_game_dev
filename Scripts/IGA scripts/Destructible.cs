using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Destructible : MonoBehaviour
{
    //The destructible knows how to take damage and "die"
    public int maximumHitPoints = 3;
    public int health;
    private int currentHitPoints;

    public Slider slider;

    public static event Action OnPlayerDeath;

    public int GetCurrentHitPoints()
    {
        return currentHitPoints;
    }


    void Start()
    {
        health = maximumHitPoints;
        currentHitPoints = maximumHitPoints;
        slider.maxValue = maximumHitPoints;
        slider.value = currentHitPoints;
    }

    //This function gets called by other scripts when its time to take damage
    public void TakeDamage(int damageAmount)
    {
        ModifyHitPoints(-damageAmount);
        slider.value = currentHitPoints;
    }

    //We can do the same thing, but positive, to heal us
    public void HealDamage(int healAmount)
    {
        ModifyHitPoints(healAmount);
        slider.value = currentHitPoints;
    }

    //This function adds or subtracts health
    private void ModifyHitPoints( int modAmount )
    {
        currentHitPoints += modAmount;
        slider.value = currentHitPoints;

        if ( currentHitPoints > maximumHitPoints )
        {
            currentHitPoints = maximumHitPoints;
        }

        if( currentHitPoints <= 0 )
        {
            Die();
            //Display our game over screen
            OnPlayerDeath?.Invoke();
        }
    }

    //This function is called when our health is 0
    private void Die()
    {
        //Could add animation here!
        Destroy(gameObject);
        OnPlayerDeath?.Invoke();

    }
}
