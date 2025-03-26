using UnityEngine;
using UnityEngine.UI; // N�cessaire si tu affiches du texte dans l'UI

public class TimerScript : MonoBehaviour
{
    public float timerDuration = 30f * 60f; // 30 minutes en secondes
    private float timer;
    public Text timerText; // UI Text pour afficher le temps (optionnel)
    public GameObject endMessage; // Un objet ou panneau qui s'affichera � la fin

    public Camera Camera;

    // R�f�rences aux scripts ou composants contr�lant la cam�ra et le mouvement du joueur
    public MonoBehaviour cameraController;
    public MonoBehaviour PlayerController;

    void Start()
    {
        //timerDuration = 5f; // 10 secondes pour tester
        timer = timerDuration;

        if (endMessage != null)
        {
            endMessage.SetActive(false); // Cache le message au d�but
        }
    }


    void Update()
    {
        // D�compte
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            DisplayTimer(timer);
        }
        else
        {
            timer = 0; // S'assurer que le timer ne soit pas n�gatif
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
        //Debug.Log("Le temps est �coul� !");
        if (endMessage != null)
        {
            endMessage.SetActive(true); // Affiche le message ou le panneau
        }
        // D�sactiver le mouvement de la cam�ra et du joueur
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
