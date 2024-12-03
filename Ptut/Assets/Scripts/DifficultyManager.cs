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
            DontDestroyOnLoad(gameObject); // Persiste à travers les scènes
        }
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        CurrentDifficulty = difficulty;
    }
}
