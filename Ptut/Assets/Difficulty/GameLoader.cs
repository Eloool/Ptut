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
    [SerializeField] private InputField customPlayerMaxHealth;
    [SerializeField] private InputField customPlayerMaxFood;
    [SerializeField] private InputField customPlayerMaxThirst;

    //[SerializeField] private InputField customPlayerRegenHealth;
    [SerializeField] private InputField customPlayerDecreaseFood;
    [SerializeField] private InputField customPlayerDecreaseThirst;

    [SerializeField] private InputField customPlayerDamageReceive;
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

        player.damageIndice = modeNormal.getDamage();

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

        player.damageIndice = modeHard.getDamage();

        player.hungerLossPerSecond = modeHard.getHungerDecrease();
        player.thirstLossPerSecond = modeHard.getThirstDecrease();

        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;
    }

    public void LoadModeCustom()
    {
        // Récupère les valeurs des champs InputField
        float customMaxHealth = float.Parse(customPlayerMaxHealth.text);
        float customMaxFood = float.Parse(customPlayerMaxFood.text);
        float customMaxThirst = float.Parse(customPlayerMaxThirst.text);

        //float customRegenHealth = float.Parse(customPlayerRegenHealth.text);
        float customDecreaseFood = float.Parse(customPlayerDecreaseFood.text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
        float customDecreaseThirst = float.Parse(customPlayerDecreaseThirst.text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);

        float customDamageReceive = float.Parse(customPlayerDamageReceive.text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
        

        player.maxHealth = customMaxHealth;
        player.currHealth = customMaxHealth;

        player.maxHunger = customMaxFood;
        player.currHunger = customMaxFood;

        player.maxThirst = customMaxThirst;
        player.currThirst = customMaxThirst;

        //player.healthLossPerSecond = customRegenHealth;
        player.hungerLossPerSecond = customDecreaseFood;
        player.thirstLossPerSecond = customDecreaseThirst;

        player.damageIndice = customDamageReceive;

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

    public void HideCustomMode()
    {
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(true);
        }
        canvaCustom.SetActive(false);
        canvaDifficulty.SetActive(false);
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
