using System.Text.RegularExpressions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;

    [Header("Audio Clips")]
    public AudioClip mainmenuMusic;

    public AudioClip interior;
    public AudioClip townAmbience;
    
    public AudioClip textSfx;

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
        music.volume = volume/400.0f;
        sfx.volume = volume/100.0f;
    }

    public void PlayTextSFX()
    {
        sfx.clip = textSfx;
        sfx.Play();
    }

    public void PlayMusic(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                music.clip = mainmenuMusic;
                music.Play(); break;
            case "HouseInterior":
                music.clip = interior;
                music.Play(); break;
            case "HouseOutside":
                music.clip = townAmbience;
                music.Play(); break;
            case "AbandonedHouse":
                music.clip = townAmbience;
                music.Play(); break;
            case "Shrine":
                music.clip = mainmenuMusic;
                music.Play(); break;
            case "Ending":
                music.clip = interior;
                music.Play(); break;
        }
    }
}
