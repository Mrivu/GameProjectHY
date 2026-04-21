using System.Text.RegularExpressions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;

    [Header("Audio Clips")]
    public AudioClip mainmenuMusic;
    public AudioClip mainmenuMusic2;

    public AudioClip textSfx;
    public AudioClip townAmbience;

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
        music.volume = volume/100.0f;
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
                music.clip = mainmenuMusic2;
                music.Play(); break;
            case "HouseInterior":
                music.clip = null;
                music.Stop(); break;
            case "HouseOutside":
                music.clip = townAmbience;
                music.Play(); break;
        }
    }
}
