using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip mainmenuMusic;
    public AudioClip mainmenuMusic2;

    // Singleton
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume/100.0f;
    }

}
