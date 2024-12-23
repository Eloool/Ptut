using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomInputManager : MonoBehaviour
{
    [SerializeField] private InputField[] inputFields;

    private Dictionary<string, float> inputValues = new Dictionary<string, float>();
    private Dictionary<string, float> defaultValues = new Dictionary<string, float>();

    void Start()
    {
        //valeur par défaut
        defaultValues["MaxHealth"] = 100f;
        defaultValues["MaxHunger"] = 100f;
        defaultValues["MaxThirst"] = 100f;
        defaultValues["MaxStamina"] = 10f;
        defaultValues["HealthLoss"] = 0.2f;
        defaultValues["HungerLoss"] = 0.2f;
        defaultValues["ThirstLoss"] = 0.2f;
        defaultValues["StaminaLoss"] = 0.2f;
        defaultValues["DamageIndice"] = 100f;

        // Initialiser les valeurs par défaut dans inputValues
        foreach (var input in inputFields)
        {
            if (defaultValues.ContainsKey(input.name))
            {
                inputValues[input.name] = defaultValues[input.name];
            }
            else
            {
                Debug.LogWarning($"Aucune valeur par défaut définie pour {input.name}");
                inputValues[input.name] = 0f; 
            }
        }
    }

    public float GetInputValue(string inputName)
    {
        if (inputValues.ContainsKey(inputName))
        {
            return inputValues[inputName];
        }

        Debug.LogError($"Aucune entrée trouvée pour {inputName}");
        return 0f;
    }

    public void ValidateInputs()
    {
        foreach (var input in inputFields)
        {
            // Si le champ est vide, utiliser la valeur par défaut
            if (string.IsNullOrWhiteSpace(input.text))
            {
                Debug.Log($"Aucune entrée pour {input.name}. Utilisation de la valeur par défaut : {defaultValues[input.name]}");
                inputValues[input.name] = defaultValues[input.name];
            }
            else if (float.TryParse(input.text, out float value))
            {
                inputValues[input.name] = value;
            }
            else
            {
                Debug.LogError($"Entrée invalide pour {input.name}, valeur par défaut appliquée: {defaultValues[input.name]}");
                inputValues[input.name] = defaultValues[input.name];
            }
        }
    }
}
