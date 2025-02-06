using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    public Slider FPSSlider;
    public TMP_Text FPSText;

    void Start()
    {
        QualitySettings.vSyncCount = 0;  // Disable V-Sync to allow unlimited FPS

        FPSSlider.onValueChanged.AddListener(SetFPSCap);
        SetFPSCap(FPSSlider.value);
    }

    void SetFPSCap(float newFPSCap)
    {
        int targetFPS = Mathf.RoundToInt(newFPSCap);
        Application.targetFrameRate = targetFPS;

        if (FPSText != null) // Update UI text if assigned
        {
            FPSText.text = $"{targetFPS}";
        }
    }

    void OnDestroy()
    {
        FPSSlider.onValueChanged.RemoveListener(SetFPSCap);
    }
}
