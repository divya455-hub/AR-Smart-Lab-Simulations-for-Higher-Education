using UnityEngine;
using TMPro; // Required for TextMeshPro UI components
using UnityEngine.UI; // Required for Button component

public class GameManager : MonoBehaviour
{
    // --- Connections (Drag and drop these in the Unity Inspector) ---
    public PendulumController pendulumScript;    // The script attached to the Bob
    public TextMeshProUGUI timerDisplay;        // UI Text to show the running time
    public TextMeshProUGUI dataTable;           // UI Text to show the logged data
    public Button startStopButton;             // The Button UI object
    public TextMeshProUGUI buttonText;          // The Text component inside the Button

    // --- Internal State Variables ---
    private bool isRunning = false;
    private float startTime;
    private int runCount = 0; // Counts the number of runs logged

    void Start()
    {
        // Initial setup for UI and button
        UpdateTimerDisplay(0f);
        dataTable.text = "Run | Total Time (s)\n"; // Set table header
        buttonText.text = "START";

        // Setup the button to call ToggleSimulation when clicked
        if (startStopButton != null)
        {
            startStopButton.onClick.AddListener(ToggleSimulation);
        }

        // Ensure the pendulum starts paused
        if (pendulumScript != null)
        {
            pendulumScript.enabled = false;
        }
    }

    void Update()
    {
        if (isRunning && pendulumScript != null)
        {
            // Continuously track and display the running time while simulation is active
            float timeElapsed = Time.time - startTime;
            UpdateTimerDisplay(timeElapsed);
        }
    }

    // Function that runs when the button is clicked
    public void ToggleSimulation()
    {
        if (pendulumScript == null) return;

        isRunning = !isRunning;
        pendulumScript.enabled = isRunning;

        if (isRunning)
        {
            // --- START SEQUENCE ---
            startTime = Time.time; // Record the moment we start

            buttonText.text = "STOP";
            pendulumScript.ResetPendulum(); // Reset the Bob to its initial position
            UpdateTimerDisplay(0f); // Reset timer display immediately
        }
        else
        {
            // --- MANUAL STOP SEQUENCE (Log the time!) ---

            // Calculate the total time elapsed for this run
            float finalTime = Time.time - startTime;

            // Log the value to the table
            RecordData(finalTime);

            buttonText.text = "START";
        }
    }

    // Updates the visual timer on the screen
    void UpdateTimerDisplay(float time)
    {
        if (timerDisplay != null)
        {
            timerDisplay.text = $"Time: {time:F2}s";
        }
    }

    // Logs the data to the table text component
    void RecordData(float finalTime)
    {
        if (dataTable != null)
        {
            runCount++;
            // Append the new total time to the table
            dataTable.text += $"Run {runCount} | {finalTime:F2}s\n";
        }
    }
}