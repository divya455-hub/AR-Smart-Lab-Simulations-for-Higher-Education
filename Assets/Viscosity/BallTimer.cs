using UnityEngine;
using TMPro;

public class BallTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // UI text for this ball
    private float startTime;            // time when ball enters liquid
    private bool isTiming = false;      // is the timer running?

    void OnTriggerEnter(Collider other)
    {
        // Start timer when entering liquid
        if (other.CompareTag("Liquid") && !isTiming)
        {
            startTime = Time.time;
            isTiming = true;
            Debug.Log(gameObject.name + " entered liquid → Timer started");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);

        // Stop timer when touching ground
        if (collision.gameObject.CompareTag("Ground") && isTiming)
        {
            isTiming = false;
            float finalTime = Time.time - startTime;
            timerText.text = "Time: " + finalTime.ToString("F2") + "s";
            Debug.Log(gameObject.name + " hit the ground → Timer stopped");
        }
    }

    void Update()
    {
        // Update UI while timer is running
        if (isTiming)
        {
            float currentTime = Time.time - startTime;
            timerText.text = "Time: " + currentTime.ToString("F2") + "s";
        }
    }

    // Reset the timer (useful for reset button)
    public void ResetTimer()
    {
        isTiming = false;
        timerText.text = "Time: 0.00s";
    }
}
