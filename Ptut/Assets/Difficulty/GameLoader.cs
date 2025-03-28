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

    //public Canvas statsCanvas;
    //public Canvas inventoryCanvas;

    void Start()
    {
        canvaCustom.SetActive(false);
        canvaStats.SetActive(false);

        if (player == null)
        {
            Debug.LogError("PlayerStats non assign� !");
        }

        Time.timeScale = 0.0f;
        Application.targetFrameRate = 60;
    }

    void Awake()
    {
        modeEasy = Resources.Load<DifficultyLevel>("DifficultyEasy");
        modeNormal = Resources.Load<DifficultyLevel>("DifficultyMedium");
        modeHard = Resources.Load<DifficultyLevel>("DifficultyHard");
    }
    public void LoadModeEasy()
    {
        LoadMode(modeEasy);
        //player.maxHealth = modeEasy.getHealth();
        //player.currHealth = modeEasy.getHealth();

        //player.damageMultiplicator = modeEasy.getDamage();

        //player.healthLossPerSecond = modeEasy.getHealthDecrease();
        //player.hungerLossPerSecond = modeEasy.getHungerDecrease();
        //player.thirstLossPerSecond = modeEasy.getThirstDecrease();
        //player.staminaLossPerSecond = modeEasy.getStaminaDecrease();

        //canvaDifficulty.SetActive(false);
        //canvaStats.SetActive(true);
        //Time.timeScale = 1f; //Set le time scale � 1 car on le met a 0 dans le Start
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadModeNormal()
    {
        LoadMode(modeNormal);
        //player.maxHealth = modeNormal.getHealth();
        //player.currHealth = modeNormal.getHealth();

        //player.damageMultiplicator = modeNormal.getDamage();

        //player.healthLossPerSecond = modeNormal.getHealthDecrease();
        //player.hungerLossPerSecond = modeNormal.getHungerDecrease();
        //player.thirstLossPerSecond = modeNormal.getThirstDecrease();
        //player.staminaLossPerSecond = modeNormal.getStaminaDecrease();

        //canvaDifficulty.SetActive(false);
        //canvaStats.SetActive(true);
        //Time.timeScale = 1f;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadModeHard()
    {
        LoadMode(modeHard);
        //player.maxHealth = modeHard.getHealth();
        //player.currHealth = modeHard.getHealth();

        //player.damageMultiplicator = modeHard.getDamage();

        //player.healthLossPerSecond = modeHard.getHealthDecrease();
        //player.hungerLossPerSecond = modeHard.getHungerDecrease();
        //player.thirstLossPerSecond = modeHard.getThirstDecrease();
        //player.staminaLossPerSecond = modeHard.getStaminaDecrease();

        //canvaDifficulty.SetActive(false);
        //canvaStats.SetActive(true);
        //Time.timeScale = 1f;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
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


        player.damageMultiplicator = customInputManager.GetInputValue("DamageIndice");

        canvaCustom.SetActive(false);
        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);

        Time.timeScale = 1f; // R�active le jeu
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCustomMode()
    {
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(false);
        }
        canvaCustom.SetActive(true);
        Time.timeScale = 0f;
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
        GameObject detailsPanel = selectedButton.transform.Find("DetailsPanel").gameObject;

        detailsPanel.SetActive(true);
    }

    public void HideDetails(Button selectedButton)
    {
        GameObject detailsPanel = selectedButton.transform.Find("DetailsPanel").gameObject;
        GameObject button = selectedButton.transform.Find("Button").gameObject;

        detailsPanel.SetActive(false);
        button.SetActive(true);
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

    public void HideDifficulty()
    {
        canvaDifficulty.SetActive(false);
        Time.timeScale = 1f;
    }

    private void LoadMode(DifficultyLevel mode)
    {
        player.maxHealth = mode.getHealth();
        player.currHealth = mode.getHealth();
        player.damageMultiplicator = mode.getDamage();
        player.healthLossPerSecond = mode.getHealthDecrease();
        player.hungerLossPerSecond = mode.getHungerDecrease();
        player.thirstLossPerSecond = mode.getThirstDecrease();
        player.staminaLossPerSecond = mode.getStaminaDecrease();
        canvaDifficulty.SetActive(false);
        canvaStats.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
