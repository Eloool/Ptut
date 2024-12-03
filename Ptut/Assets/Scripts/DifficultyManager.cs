using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard, Personalized }
    public static DifficultyManager Instance { get; private set; }

    public DifficultyLevel CurrentDifficulty { get; private set; } = DifficultyLevel.Medium;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste � travers les sc�nes
        }
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        CurrentDifficulty = difficulty;
    }
}
