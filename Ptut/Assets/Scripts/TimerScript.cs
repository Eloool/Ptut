using UnityEngine;
using UnityEngine.UI; // Nécessaire si tu affiches du texte dans l'UI

public class TimerScript : MonoBehaviour
{
    public float timerDuration = 30f * 60f; // 30 minutes en secondes
    private float timer;
    public Text timerText; // UI Text pour afficher le temps (optionnel)
    public GameObject endMessage; // Un objet ou panneau qui s'affichera à la fin

    public Camera Camera;

    // Références aux scripts ou composants contrôlant la caméra et le mouvement du joueur
    public MonoBehaviour cameraController;
    public MonoBehaviour PlayerController;

    void Start()
    {
        //timerDuration = 5f; // 10 secondes pour tester
        timer = timerDuration;

        if (endMessage != null)
        {
            endMessage.SetActive(false); // Cache le message au début
        }
    }


    void Update()
    {
        // Décompte
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            DisplayTimer(timer);
        }
        else
        {
            timer = 0; // S'assurer que le timer ne soit pas négatif
            TimeUp();
        }
    }

    void DisplayTimer(float timeToDisplay)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeToDisplay / 60);
            int seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void TimeUp()
    {
        //Debug.Log("Le temps est écoulé !");
        if (endMessage != null)
        {
            endMessage.SetActive(true); // Affiche le message ou le panneau
        }
        // Désactiver le mouvement de la caméra et du joueur
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
        Time.timeScale = 0f;
        if (PlayerController != null)
        {
            PlayerController.enabled = false;
        }
    }
}
