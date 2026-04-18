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

    private void Update()
    {
        if (InputControls.Instance.pause.WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (canTogglePause)
        {
            eventSystem.SetSelectedGameObject(null);
            gamePaused = !gamePaused;
            pauseCanvas.SetActive(gamePaused);
        }
    }

    public void ToggleSettings(bool openSettings)
    {
        if (openSettings)
        {
            UpdateSettingButtons();
        }

        canTogglePause = !openSettings;
        if (gamePaused)
        {
            pauseCanvas.SetActive(!openSettings);
        }
        settingsCanvas.SetActive(openSettings);
    }

    public void UpdateSettingButtons()
    {
        fontSetting.font = GameManager.Instance.settings.GetCurrentFont();
        resSetting.text = GameManager.Instance.settings.GetCurrentResolution().ToString();
        volSetting.text = GameManager.Instance.settings.GetCurrentVolume().ToString() + "%";
    }

    public void ChangeFont()
    {
        GameManager.Instance.settings.selectedFont = (GameManager.Instance.settings.selectedFont+1) % GameManager.Instance.settings.gameFonts.Count;
        GameManager.Instance.settings.UpdateFont();
        UpdateSettingButtons();
    }

    public void ChangeResolution()
    {
        GameManager.Instance.settings.selectedResolution = (GameManager.Instance.settings.selectedResolution + 1) % GameManager.Instance.settings.resolutions.Count;
        GameManager.Instance.settings.UpdateResolution();
        UpdateSettingButtons();
    }

    public void ChangeVolume()
    {
        GameManager.Instance.settings.volume = (GameManager.Instance.settings.volume + 10) % 110.0f;
        GameManager.Instance.settings.UpdateVolume();
        UpdateSettingButtons();
    }
}
