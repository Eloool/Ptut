using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        // Set the slider's initial value to the current audio volume
        volumeSlider.value = AudioListener.volume;

        // Add listener to call OnVolumeChange when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        // Set the global volume
        AudioListener.volume = value;
    }

    void OnDestroy()
    {
        // Remove the listener to avoid memory leaks
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
    }
}
