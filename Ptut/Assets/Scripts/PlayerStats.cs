using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Header("Health")]
    public float maxHealth;
    private float currHealth;
    public float healthLossPerSecond;
    public Image healthBarFill;

    [Header("Hunger")]
    public float maxHunger;
    private float currHunger;
    public float hungerLossPerSecond;
    public Image hungerBarFill;

    [Header("Thirst")]
    public float maxThirst;
    private float currThirst;
    public float thirstLossPerSecond;
    public Image thirstBarFill;

    [Header("Armor Settings")]
    public float armorResistance; // 0 <= armorResistance <= 100
    private float damageMultiplicator;
    
    

    void Start()
    {
        currHealth = maxHealth;
        currHunger = maxHunger;
        currThirst = maxThirst;

        damageMultiplicator = (100 - 0.75f * armorResistance) / 100;
    }


    void Update()
    {
        UpdateHungerThirstBarsFill();

        if (currHealth <= 0) { Die(); }
    }


    void UpdateHealthBarFill()
    {
        healthBarFill.fillAmount = currHealth / maxHealth;
    }

    void UpdateHungerThirstBarsFill()
    {
        // decrease every second
        currHunger -= hungerLossPerSecond * Time.deltaTime;
        currThirst -= thirstLossPerSecond * Time.deltaTime;

        // hunger and thirst can't be negative
        if (currHunger < 0) { currHunger = 0; }
        if (currThirst < 0) { currThirst = 0; }

        // bars filling
        hungerBarFill.fillAmount = currHunger / maxHunger;
        thirstBarFill.fillAmount = currThirst / maxThirst;

        // if hunger or thirst = 0
        if (currHunger <= 0 || currThirst <= 0)
        {
            float healthDamage = healthLossPerSecond;
            if (currHunger <= 0 && currThirst <= 0) { healthDamage *= 2; }
            TakeDamage(healthDamage, true, true);
        }
    }

    public void TakeDamage(float damage, bool overTime = false, bool trueDamage = false)
    {
        if (!trueDamage) { damage *= damageMultiplicator; }
        if (overTime) { currHealth -= damage * Time.deltaTime; }
        else { currHealth -= damage; }

        UpdateHealthBarFill();
    }

    public void Die() // game over
    {
        // TODO

        UnityEditor.EditorApplication.isPlaying = false; // quits the game (temporary code)
    }
}
