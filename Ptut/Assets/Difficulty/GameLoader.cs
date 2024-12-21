using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public DifficultyLevel modeEasy, modeNormal, modeHard, modeCustom;

    public PlayerStats player;

    [SerializeField]
    private GameObject canvaDifficulty, canvaCustom;

    // Champs pour entrer les valeurs custom
    [SerializeField] private InputField customHealthInput;
    [SerializeField] private InputField customFoodInput;
    void Start()
    {
        Time.timeScale = 0f;
        canvaDifficulty.SetActive(true);
        canvaCustom.SetActive(false);
    }
    public void LoadModeEasy()
    {

        player.maxHealth = modeEasy.getHealth(); 

        player.hungerLossPerSecond = modeEasy.getHungerDecrease();
        player.thirstLossPerSecond = modeEasy.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        Time.timeScale = 1f;//Set le time scale à 1 car on le met a 0 dans le Start
    }

    public void LoadModeNormal()
    {
        player.maxHealth = modeNormal.getHealth();

        player.hungerLossPerSecond = modeNormal.getHungerDecrease();
        player.thirstLossPerSecond = modeNormal.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadModeHard()
    {
        player.maxHealth = modeHard.getHealth();

        player.hungerLossPerSecond = modeHard.getHungerDecrease();
        player.thirstLossPerSecond = modeHard.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadModeCustom()
    {
        // Récupère les valeurs des champs InputField
        float customHealth = float.Parse(customHealthInput.text);
        float customFood = float.Parse(customFoodInput.text);

        player.maxHealth = customHealth;
        player.hungerLossPerSecond = customFood;

        canvaCustom.SetActive(false);

        player.Launch = true;
        Time.timeScale = 1f; // Réactive le jeu
    }

    public void ShowCustomMode()
    {
        canvaDifficulty.SetActive(false);
        canvaCustom.SetActive(true);
        Time.timeScale = 1f;
    }
}
