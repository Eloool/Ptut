using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    public float savedVolume; // To store the volume before muting
    public bool isMuted = false; // Flag to track mute state

    void Start()
    {
        // Set the slider's initial value to the current audio volume
        savedVolume = AudioListener.volume;
        volumeSlider.value = AudioListener.volume;

        // Add listener to call OnVolumeChange when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void Update()
    {
        HandleTimeScaleVolume();
    }

    void OnVolumeChange(float value)
    {
        // Update the global volume and store it
        AudioListener.volume = value;
        savedVolume = value;
    }

    void OnDestroy()
    {
        // Remove the listener to avoid memory leaks
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
    }

    private void HandleTimeScaleVolume()
    {
        if (Time.timeScale == 0 && !isMuted)
        {
            // Mute volume when timeScale is 0
            AudioListener.volume = 0f;
            isMuted = true;
        }
        else if (Time.timeScale == 1 && isMuted)
        {
            // Restore volume when timeScale is 1
            AudioListener.volume = savedVolume;
            isMuted = false;
        }
    }
}
