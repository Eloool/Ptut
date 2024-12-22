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
    private GameObject canvaDifficulty, canvaCustom, canvaStats;

    public Button[] difficultyButtons; 

    // Champs pour entrer les valeurs custom
    [SerializeField] private InputField customHealthInput;
    [SerializeField] private InputField customFoodInput;
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

        player.hungerLossPerSecond = modeEasy.getHungerDecrease();
        player.thirstLossPerSecond = modeEasy.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;//Set le time scale à 1 car on le met a 0 dans le Start
    }

    public void LoadModeNormal()
    {
        player.maxHealth = modeNormal.getHealth();
        player.currHealth = modeNormal.getHealth();

        player.hungerLossPerSecond = modeNormal.getHungerDecrease();
        player.thirstLossPerSecond = modeNormal.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;
    }

    public void LoadModeHard()
    {
        player.maxHealth = modeHard.getHealth();
        player.currHealth = modeHard.getHealth();

        player.hungerLossPerSecond = modeHard.getHungerDecrease();
        player.thirstLossPerSecond = modeHard.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;
    }

    public void LoadModeCustom()
    {
        // Récupère les valeurs des champs InputField
        float customHealth = float.Parse(customHealthInput.text);
        float customFood = float.Parse(customFoodInput.text);

        player.maxHealth = customHealth;
        player.currHealth = customHealth;

        player.hungerLossPerSecond = customFood;

        canvaCustom.SetActive(false);
        canvaStats.SetActive(true);

        player.Launch = true;
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
}
