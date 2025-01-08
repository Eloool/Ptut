using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public DifficultyLevel modeEasy, modeNormal, modeHard;

    public PlayerStats player;

    [SerializeField]
    private GameObject canvaDifficulty, canvaCustom, canvaStats, canvasInfoCustom;

    public Button[] difficultyButtons;

    [SerializeField] private CustomInputManager customInputManager;

    void Start()
    {
        Time.timeScale = 0f;
        canvaDifficulty.SetActive(true);
        canvaCustom.SetActive(false);
        canvaStats.SetActive(false);
    }
    public void LoadModeEasy()
    {

        player.maxHealth = modeEasy.getHealth();
        player.currHealth = modeEasy.getHealth();

        player.damageIndice = modeEasy.getDamage();

        player.healthLossPerSecond = modeEasy.getHealthDecrease();
        player.hungerLossPerSecond = modeEasy.getHungerDecrease();
        player.thirstLossPerSecond = modeEasy.getThirstDecrease();
        player.staminaLossPerSecond = modeEasy.getStaminaDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;//Set le time scale à 1 car on le met a 0 dans le Start
    }

    public void LoadModeNormal()
    {
        player.maxHealth = modeNormal.getHealth();
        player.currHealth = modeNormal.getHealth();

        player.damageIndice = modeNormal.getDamage();

        player.healthLossPerSecond = modeNormal.getHealthDecrease();
        player.hungerLossPerSecond = modeNormal.getHungerDecrease();
        player.thirstLossPerSecond = modeNormal.getThirstDecrease();
        player.staminaLossPerSecond = modeNormal.getStaminaDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;
    }

    public void LoadModeHard()
    {
        player.maxHealth = modeHard.getHealth();
        player.currHealth = modeHard.getHealth();

        player.damageIndice = modeHard.getDamage();

        player.healthLossPerSecond = modeHard.getHealthDecrease();
        player.hungerLossPerSecond = modeHard.getHungerDecrease();
        player.thirstLossPerSecond = modeHard.getThirstDecrease();
        player.staminaLossPerSecond = modeHard.getStaminaDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;
    }

    public void LoadModeCustom()
    {
        customInputManager.ValidateInputs();

        player.maxHealth = customInputManager.GetInputValue("MaxHealth");
        player.currHealth = player.maxHealth;

        player.maxHunger = customInputManager.GetInputValue("MaxHunger");
        player.currHunger = player.maxHunger;

        player.maxThirst = customInputManager.GetInputValue("MaxThirst");
        player.currThirst = player.maxThirst;

        player.maxStamina = customInputManager.GetInputValue("MaxStamina");
        player.currStamina = player.maxStamina;

        player.healthLossPerSecond = customInputManager.GetInputValue("HealthLoss");
        player.hungerLossPerSecond = customInputManager.GetInputValue("HungerLoss");
        player.thirstLossPerSecond = customInputManager.GetInputValue("ThirstLoss");
        player.staminaLossPerSecond = customInputManager.GetInputValue("StaminaLoss");


        player.damageIndice = customInputManager.GetInputValue("DamageIndice");

        canvaCustom.SetActive(false);
        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);

        Time.timeScale = 1f; // Réactive le jeu
    }

    public void ShowCustomMode()
    {
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(false);
        }
        canvaCustom.SetActive(true);
        Time.timeScale = 1f;
    }

    public void HideCustomMode()
    {
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(true);
        }
        canvaCustom.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowDetails(Button selectedButton)
    {
        HideOtherButtons(selectedButton);

        Transform detailsPanelTransform = selectedButton.transform.Find("DetailsPanel");

        GameObject detailsPanel = detailsPanelTransform.gameObject;

        detailsPanel.SetActive(true);

        canvaDifficulty.GetComponent<HorizontalLayoutGroup>().padding.top = -450;
    }

    public void HideDetails(Button selectedButton)
    {
        // Réaffiche tous les boutons
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(true);
        }

        Transform detailsPanelTransform = selectedButton.transform.Find("DetailsPanel");

        GameObject detailsPanel = detailsPanelTransform.gameObject;

        detailsPanel.SetActive(false);

        canvaDifficulty.GetComponent<HorizontalLayoutGroup>().padding.top = 0;
    }

    // Méthode pour masquer tous les boutons sauf celui sélectionné
    private void HideOtherButtons(Button selectedButton)
    {
        foreach (Button button in difficultyButtons)
        {
            if (button != selectedButton) // Masquer les autres boutons
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void ShowInfoCustom()
    {
        canvasInfoCustom.SetActive(true);
        canvaCustom.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HideInfoCustom()
    {
        canvasInfoCustom.SetActive(false);
        canvaCustom.SetActive(true);
        Time.timeScale = 1f;
    }

}
