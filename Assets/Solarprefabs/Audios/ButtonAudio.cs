using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{
    public Button button;       // assign your button
    public AudioClip clip;      // drag the clip here

    private AudioSource audioSource;

    void Awake()
    {
        // force-disable all auto-play
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Start()
    {
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
