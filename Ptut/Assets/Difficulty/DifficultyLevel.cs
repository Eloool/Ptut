using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DifficultyLevel", menuName = "Custom Objects/DifficultyLevel")]
public class DifficultyLevel : ScriptableObject
{
    public enum Difficulty { Easy, Normal, Hard, Custom }

    [SerializeField] private Difficulty _gameDifficulty;


    private float Health;

    private float Damage;

    private float healthDecrease;
    private float hungerDecrease;
    private float thirstDecrease;
    private float staminaDecrease;

    private void setHealth()
    {
        if (_gameDifficulty == Difficulty.Easy) Health = 150.0f;
        else if (_gameDifficulty == Difficulty.Normal) Health = 100.0f;
        else Health = 75.0f;
    }

    public float getHealth()
    {
        setHealth();
        return Health;
    }

    private void setHealthDecrease()
    {
        if (_gameDifficulty == Difficulty.Easy) healthDecrease = 0.1f;
        else if (_gameDifficulty == Difficulty.Normal) healthDecrease = 0.2f;
        else healthDecrease = 0.3f;
    }

    public float getHealthDecrease()
    {
        setHealthDecrease();
        return healthDecrease;
    }

    private void setHungerDecrease()
    {
        if (_gameDifficulty == Difficulty.Easy) hungerDecrease = 0.1f;
        else if (_gameDifficulty == Difficulty.Normal) hungerDecrease = 0.2f;
        else hungerDecrease = 0.3f;
    }

    public float getHungerDecrease()
    {
        setHungerDecrease();
        return hungerDecrease;
    }

    private void setThirstDecrease()
    {
        if (_gameDifficulty == Difficulty.Easy) thirstDecrease = 0.1f;
        else if (_gameDifficulty == Difficulty.Normal) thirstDecrease = 0.2f;
        else thirstDecrease = 0.3f;
    }

    public float getThirstDecrease()
    {
        setThirstDecrease();
        return thirstDecrease;
    }

    private void setStaminaDecrease()
    {
        if (_gameDifficulty == Difficulty.Easy) staminaDecrease = 0.3f;
        else if (_gameDifficulty == Difficulty.Normal) staminaDecrease = 0.65f;
        else staminaDecrease = 1.0f;
    }

    public float getStaminaDecrease()
    {
        setStaminaDecrease();
        return staminaDecrease;
    }

    private void setDamage()
    {
        if (_gameDifficulty == Difficulty.Easy) Damage = 0.85f;
        else if (_gameDifficulty == Difficulty.Normal) Damage = 1.0f;
        else Damage = 1.15f;
    }

    public float getDamage()
    {
        setDamage();
        return Damage;
    }
}



