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

    private float HungerDecrease;
    private float thirstDecrease;

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

    private void setHungerDecrease()
    {
        if (_gameDifficulty == Difficulty.Easy) HungerDecrease = 0.1f;
        else if (_gameDifficulty == Difficulty.Normal) HungerDecrease = 0.2f;
        else HungerDecrease = 0.3f;
    }

    public float getHungerDecrease()
    {
        setHungerDecrease();
        return HungerDecrease;
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

    private void setDamage()
    {
        if (_gameDifficulty == Difficulty.Easy) Damage = 85.0f;
        else if (_gameDifficulty == Difficulty.Normal) Damage = 100.0f;
        else Damage =115.0f;
    }

    public float getDamage()
    {
        setDamage();
        return Damage;
    }
}
