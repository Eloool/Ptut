using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingFood : MonoBehaviour
{
    private Hand hand;
    private int healthFood;
    private Item food;
    public int healthFoodStart;
    public int healthFoodLostEachTick;

    public float timeBetweenTick;
    private float currentTimeBetweenTicks = 0.0f;


    private void Start()
    {
        hand = GetComponent<Hand>(); 
    }
    private void Update()
    {
        if (food != null && healthFood > 0)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                LoseHealth();
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                ResetHealth();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ResetHealth();
            }
        }
        else if(food != null && healthFood <= 0)
        {
            EatFood();
            ResetHealth();
        }
    }

    public void SetFood(Item food)
    {
        this.food = food;
        if (food != null)
        {
            FoodStat foodStat;
            ThirstStat thirstStat;
            if (food.TryGetStat<FoodStat>(out foodStat) || food.TryGetStat<ThirstStat>(out thirstStat))
            {
                Debug.Log("Peut être bu ou manger");
                healthFood = healthFoodStart;
            }
            else
            {
                Debug.LogError("Ne peut pas être bu ou manger alors que son type et de food");
                this.food = null;
            }
        }
    }

    private void LoseHealth()
    {
        currentTimeBetweenTicks += Time.deltaTime;
        while (currentTimeBetweenTicks > timeBetweenTick)
        {
            currentTimeBetweenTicks -= timeBetweenTick;
            healthFood -= healthFoodLostEachTick;
        }
    }

    private void EatFood()
    {
        FoodStat foodStat;
        ThirstStat thirstStat;
        if(food.TryGetStat<FoodStat> (out foodStat))
        {
            PlayerStats.instance.Hunger(foodStat.foodStat);
        }
        if(food.TryGetStat<ThirstStat>(out thirstStat))
        {
            PlayerStats.instance.Thirst(thirstStat.thristStat);
        }
        food.MinusOne();
        Inventory.instance.ActionBar.Reload3DObjects();
    }

    private void ResetHealth()
    {
        healthFood = healthFoodStart;
        currentTimeBetweenTicks = 0.0f;
    }
}
