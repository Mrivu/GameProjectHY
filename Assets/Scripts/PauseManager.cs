using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject pauseCanvas;

    public EventSystem eventSystem;

    public bool gamePaused = false;
    public bool canTogglePause = true;

    [Header("Settings buttons")]
    public TextMeshProUGUI fontSetting;
    public TextMeshProUGUI resSetting;
    public TextMeshProUGUI volSetting;

    private void Start()
    {
        gamePaused = false;

        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        if (settingsCanvas != null)
            settingsCanvas.SetActive(false);
    }

    public void TogglePause()
    {
        if (canTogglePause)
        {
            if (eventSystem != null)
            {
                eventSystem.SetSelectedGameObject(null);
            }

            gamePaused = !gamePaused;

            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(gamePaused);
            }

        }
    }

    public void ToggleSettings(bool openSettings)
    {
        if (openSettings)
        {
            UpdateSettingButtons();
        }

        canTogglePause = !openSettings;

        if (gamePaused && pauseCanvas != null)
        {
            pauseCanvas.SetActive(!openSettings);
        }

        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(openSettings);
        }
    }

    public void UpdateSettingButtons()
    {
        if (fontSetting != null)
            fontSetting.font = GameManager.Instance.settings.GetCurrentFont();

        if (resSetting != null)
            resSetting.text = GameManager.Instance.settings.GetCurrentResolution().ToString();

        if (volSetting != null)
            volSetting.text = GameManager.Instance.settings.GetCurrentVolume().ToString() + "%";
    }

    public void ChangeFont()
    {
        GameManager.Instance.settings.selectedFont =
            (GameManager.Instance.settings.selectedFont + 1) %
            GameManager.Instance.settings.gameFonts.Count;

        GameManager.Instance.settings.UpdateFont();
        UpdateSettingButtons();
    }

    public void ChangeResolution()
    {
        GameManager.Instance.settings.selectedResolution =
            (GameManager.Instance.settings.selectedResolution + 1) %
            GameManager.Instance.settings.resolutions.Count;

        GameManager.Instance.settings.UpdateResolution();
        UpdateSettingButtons();
    }

    public void ChangeVolume()
    {
        GameManager.Instance.settings.volume =
            (GameManager.Instance.settings.volume + 10) % 110.0f;

        GameManager.Instance.settings.UpdateVolume();
        UpdateSettingButtons();
    }
}