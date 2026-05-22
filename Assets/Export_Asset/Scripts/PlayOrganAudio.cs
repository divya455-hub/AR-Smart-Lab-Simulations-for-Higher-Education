using UnityEngine;

public class PlayOrganAudio : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip brainClip;     // Audio clip for brain details
    public AudioClip eyeClip;       // Audio clip for eye details
    public AudioClip heartClip;     // Audio clip for heart details
    public AudioClip lungsClip;     // Audio clip for lungs details
    public AudioClip kidneyClip;    // Audio clip for kidney details
    public AudioClip liverClip;     // Audio clip for liver details
    public AudioClip uterusClip;    // Audio clip for uterus details
    public AudioClip intestineClip; // Audio clip for intestine details
    public AudioClip skullClip;
    public AudioClip gaugeClip; // Audio clip for skull details

    private AudioClip currentClip;  // To track the currently playing clip
    private bool isPaused = false;  // Tracks if the audio is paused
    private float lastTapTime = 0f; // To track the time between taps
    private float doubleTapThreshold = 0.3f; // Max time between two taps to be considered a double-tap

    // Method to handle single or double tap for brain audio
    public void HandleBrainAudio()
    {
        HandleAudio(brainClip);
    }

    // Method to handle single or double tap for eye audio
    public void HandleEyeAudio()
    {
        HandleAudio(eyeClip);
    }

    // Method to handle single or double tap for heart audio
    public void HandleHeartAudio()
    {
        HandleAudio(heartClip);
    }

    // Method to handle single or double tap for lungs audio
    public void HandleLungsAudio()
    {
        HandleAudio(lungsClip);
    }

    // Method to handle single or double tap for kidney audio
    public void HandleKidneyAudio()
    {
        HandleAudio(kidneyClip);
    }

    // Method to handle single or double tap for liver audio
    public void HandleLiverAudio()
    {
        HandleAudio(liverClip);
    }

    // Method to handle single or double tap for uterus audio
    public void HandleUterusAudio()
    {
        HandleAudio(uterusClip);
    }

    // Method to handle single or double tap for intestine audio
    public void HandleIntestineAudio()
    {
        HandleAudio(intestineClip);
    }

    // Method to handle single or double tap for skull audio
    public void HandleSkullAudio()
    {
        HandleAudio(skullClip);
    }
    public void HandlegaugeAudio()
    {
        HandleAudio(gaugeClip);
    }

    // Generalized method to handle single or double tap for any organ
    private void HandleAudio(AudioClip organClip)
    {
        if (IsDoubleTap())
        {
            StopAudio(); // Stop audio on double-tap
            return;
        }

        // Play or toggle organ audio on single tap
        if (currentClip != organClip)
        {
            PlayNewAudio(organClip);
        }
        else
        {
            ToggleAudio(); // Play, pause, or resume
        }
    }

    // Helper method to play a new audio clip
    private void PlayNewAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop(); // Stop any currently playing audio
            audioSource.clip = clip; // Set the new audio clip
            currentClip = clip; // Update the current clip
            audioSource.Play(); // Start playing the new audio
            isPaused = false; // Reset pause state
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }

    // Helper method to toggle play, pause, or resume
    private void ToggleAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause(); // Pause the audio
            isPaused = true;
        }
        else if (isPaused)
        {
            audioSource.UnPause(); // Resume the audio
            isPaused = false;
        }
    }

    // Helper method to stop the audio completely
    private void StopAudio()
    {
        if (audioSource.isPlaying || isPaused)
        {
            audioSource.Stop(); // Stop the audio
            currentClip = null; // Reset the current clip
            isPaused = false; // Reset pause state
            Debug.Log("Audio stopped completely.");
        }
    }

    // Helper method to check if the user performed a double-tap
    private bool IsDoubleTap()
    {
        float timeSinceLastTap = Time.time - lastTapTime;
        lastTapTime = Time.time;

        return timeSinceLastTap <= doubleTapThreshold;
    }
}
