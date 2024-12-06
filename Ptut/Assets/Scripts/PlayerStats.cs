using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int maxHealth;
    public int currHealth;

    private int maxHunger;
    public int currHunger;

    private int maxThirst;
    public int currThirst;

    public float hungerTimer;
    public float loseHungerTimer;

    public float thirstTimer;
    public float loseThirst;

    void Start()
    {
        maxHealth = 100;
        currHealth = maxHealth;

        maxHunger = 100;
        currHunger = maxHunger;

        maxThirst = 100;
        currThirst = maxThirst;

        hungerTimer = 10;
        loseHungerTimer = 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(25);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currHunger = 3;
            currThirst = 3;
        }

        if (currHealth < 0)
        {
            Die();
        }

        if (hungerTimer > 0)
        {
            hungerTimer -= Time.deltaTime;
        }
        else
        {
            if (currHunger - 1 < 0)
            {
                Hungry();
            }
            
            hungerTimer = 10;
        }

    }

    public void TakeDamage(int damage)
    {
        if (currHealth - damage < 0)
        {
            currHealth = 0;
            return;
        }
        currHealth -= damage;
    }

    public void Heal(int health)
    {
        if (currHealth + health > maxHealth)
        {
            currHealth = 100;
            return;
        }
        currHealth += health;
    }

    public void Hungry()
    {
        if (loseHungerTimer > 0)
        {
            loseHungerTimer -= Time.deltaTime;
        }
        else
        {
            currHealth -= 1;
            loseHungerTimer = 5;
        }
    }



    public void Die()
    {
        // game over
    }
}
