using UnityEngine;

public class SlowingTime : MonoBehaviour
{
    public GameObject pausePanel;
    public float timeToSlowDown = 1f; // Temps nécessaire pour ralentir complètement le jeu (en secondes)

    private bool isTimeSlowingDown = false;
    private float slowDownTimer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTimeSlowingDown)
        {
            isTimeSlowingDown = true;
            // Activer le panneau de pause
            if (pausePanel != null)
                pausePanel.SetActive(true);

            // Commencer à ralentir progressivement le jeu
            Time.timeScale = 1f; // Réinitialiser le timeScale au cas où il serait déjà différent de 1
            slowDownTimer = 0f;
        }
    }

    private void Update()
    {
        if (isTimeSlowingDown)
        {
            slowDownTimer += Time.unscaledDeltaTime;
            float newTimeScale = Mathf.Lerp(1f, 0f, slowDownTimer / timeToSlowDown);
            Time.timeScale = newTimeScale;

            if (newTimeScale <= 0f)
            {
                Time.timeScale = 0f;
                isTimeSlowingDown = false;
            }
        }
    }
}
