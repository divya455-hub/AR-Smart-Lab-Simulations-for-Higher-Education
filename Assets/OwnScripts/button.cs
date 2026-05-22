using UnityEngine;

public class PlayAudioOnClick : MonoBehaviour
{
    public AudioSource audioSource; 

    public void PlayAudio()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
}


