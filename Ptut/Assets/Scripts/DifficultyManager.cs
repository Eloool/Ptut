using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard, Personalized }
    public static DifficultyManager InstanceDM { get; private set; }

    public DifficultyLevel CurrentDifficulty { get; private set; } = DifficultyLevel.Easy;

    private void Awake()
    {
        if (InstanceDM != null && InstanceDM != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceDM = this;
            DontDestroyOnLoad(gameObject); // Persiste � travers les sc�nes
        }
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        CurrentDifficulty = difficulty;
    }

    public DifficultyManager.DifficultyLevel GetDifficulty()
    {
        return DifficultyManager.InstanceDM.CurrentDifficulty;
    }

    public float GetDamageMultiplier()
    {
        return CurrentDifficulty switch
        {
            DifficultyLevel.Easy => 0.5f,
            DifficultyLevel.Medium => 1.0f,
            DifficultyLevel.Hard => 1.5f,
            _ => 1.0f,
        };
    }

    public float GetHungerLossPerSecond()
    {
        return CurrentDifficulty switch
        {
            DifficultyLevel.Easy => 0.5f,
            DifficultyLevel.Medium => 1.0f,
            DifficultyLevel.Hard => 1.5f,
            _ => 1.0f,
        };
    }
}
