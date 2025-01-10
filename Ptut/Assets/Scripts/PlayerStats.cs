using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Header("Health")]
    public float maxHealth;
    public float currHealth;
    public float healthLossPerSecond;
    public Image healthBarFill;

    [Header("Hunger")]
    public float maxHunger;
    public float currHunger;
    public float hungerLossPerSecond;
    public Image hungerBarFill;

    [Header("Thirst")]
    public float maxThirst;
    public float currThirst;
    public float thirstLossPerSecond;
    public Image thirstBarFill;

    [Header("Stamina")]
    public float maxStamina;
    public float currStamina;
    public float staminaLossPerSecond;
    public Image staminaBar;
    public Image staminaBarFill;
    public bool enableSprint;

    [Header("Armor Settings")]
    public float armorResistance; // 0 <= armorResistance <= 100
    public float damageMultiplicator;
    private float difficultyMultiplicator;

    public static PlayerStats instance;

    public bool isDying = false; // Flag to check if the player is already dying


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        currHealth = maxHealth;
        currHunger = maxHunger;
        currThirst = maxThirst;
        currStamina = maxStamina;

        enableSprint = true;

        difficultyMultiplicator = 1;
        damageMultiplicator = ((100 - 0.75f * armorResistance) / 100) * difficultyMultiplicator;
    }


    void Update()
    {
        UpdateHungerThirstBarsFill();
        UpdateStaminaBarFill();

        if (currHealth <= 0 && !isDying)
        {
            StartCoroutine(Die());
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(10, false, true);
            Debug.Log("damage");
        }
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

    void UpdateStaminaBarFill()
    {
        // stamina decrease
        if (Input.GetKey(KeyCode.LeftShift) &&
            (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) // à changer
        {
            staminaBar.gameObject.SetActive(true);
            currStamina -= staminaLossPerSecond * Time.deltaTime;
        }
        else if (enableSprint) // stamina increase
        {
            currStamina += staminaLossPerSecond * Time.deltaTime;
        }

        // stamina can't be negative or over maxStamina
        if (currStamina < 0)
        {
            currStamina = 0;
            enableSprint = false;
            StartCoroutine(LowStamina());
        }

        if (currStamina > maxStamina)
        {
            currStamina = maxStamina;
            staminaBar.gameObject.SetActive(false);
        }

        // bar filling
        staminaBarFill.fillAmount = currStamina / maxStamina;
    }

    //  
    IEnumerator LowStamina()
    {
        yield return new WaitForSeconds(3);
        enableSprint = true;
    }

    public void TakeDamage(float damage, bool overTime = false, bool trueDamage = false)
    {

        if (!trueDamage) { damage *= damageMultiplicator; }
        if (overTime) { currHealth -= damage * Time.deltaTime; }
        else { currHealth -= damage; }

        UpdateHealthBarFill();

    }

    IEnumerator Die() // game over
    {
        if (isDying) yield break; // Exit the coroutine if already dying
        isDying = true; // Set the flag to true

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("isDying");
        yield return new WaitForSeconds(3f);
        UnityEditor.EditorApplication.isPlaying = false; // quits the game (temporary code)
    }

}