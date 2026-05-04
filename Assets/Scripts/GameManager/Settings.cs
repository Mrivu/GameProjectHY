using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Settings : MonoBehaviour
{
    public int selectedFont = 0;
    public List<TMP_FontAsset> gameFonts;

    public int selectedResolution = 1;
    public List<Tuple<int, int>> resolutions = new List<Tuple<int, int>>() { 
        new (1280, 720), new(1920, 1080), new(2560, 1440)};

    public float volume = 100.0f;

    public void DefaultValues()
    {
        selectedFont = 0;
        selectedResolution = 1;
        volume = 100.0f;
    }

    public TMP_FontAsset GetCurrentFont()
    {
        return gameFonts[selectedFont];
    }

    public void UpdateFont()
    {
        if (GameManager.Instance.dialogueSystem != null) 
        { 
            GameManager.Instance.dialogueSystem.font = GetCurrentFont();
            GameManager.Instance.dialogueSystem.ApplyFont();
        }
    }

    public Tuple<int, int> GetCurrentResolution()
    {
        return new(resolutions[selectedResolution].Item1, resolutions[selectedResolution].Item2);
    }

    public void UpdateResolution()
    {
        Tuple<int,int> res = GetCurrentResolution();
        Screen.SetResolution(res.Item1, res.Item2, FullScreenMode.FullScreenWindow);
    }

    public float GetCurrentVolume()
    {
        return volume;
    }

    public void UpdateVolume()
    {
        GameManager.Instance.audioManager.SetVolume(GetCurrentVolume());
    }


    public void UpdateAll()
    {
        UpdateFont();
        UpdateResolution();
        UpdateVolume();
    }
}

