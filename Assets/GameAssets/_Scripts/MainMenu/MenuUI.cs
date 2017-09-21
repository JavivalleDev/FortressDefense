using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _guidePanel;
    [SerializeField] private GameObject _controlsPanel;
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _exitPanel;
    [SerializeField] private GameObject _tomenuPanel;

    [SerializeField] private Image _fadeImage;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressText;
    [SerializeField] private Text _loadText;
    [SerializeField] private Image _loadBackg;

    [SerializeField] private Slider _FXvolumeSlider;
    [SerializeField] private Text _FXvolumeText;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Text _musicVolumeText;

    [SerializeField] private Toggle _fullScreenToggle;

    [SerializeField] private Texture2D _cursorImg;

    private AsyncOperation loadGameplay;
    private bool _bIsLoading;

    private AudioSource _as;

    private void Awake()
    {
        //if(_cursorImg) Cursor.SetCursor(_cursorImg, Vector2.zero, CursorMode.Auto);

        _FXvolumeSlider.value = PlayerPrefs.HasKey("FXVolume") ? PlayerPrefs.GetFloat("FXVolume") : 1;
        _FXvolumeText.text = Mathf.Floor(_FXvolumeSlider.value * 100).ToString();

        _musicVolumeSlider.value = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 1;
        _musicVolumeText.text = Mathf.Floor(_musicVolumeSlider.value * 100).ToString();

        Constants._fxVolume = _FXvolumeSlider.value;
        Constants._musicVolume = _musicVolumeSlider.value;

        _fullScreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;
        Screen.fullScreen = _fullScreenToggle.isOn;

        _as = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (loadGameplay != null)
        {
            _progressBar.fillAmount = loadGameplay.progress;
            _progressText.text = (loadGameplay.progress * 100) + "%";

            if (loadGameplay.progress > 0.89f && _fadeImage.color.a >= .99f)
            {
                _progressBar.fillAmount = 1;
                _progressText.text = "100%";
                Invoke("LoadAlready", .5f);
            }
        }

        if(_bIsLoading) _fadeImage.color += new Color(0, 0, 0, Time.deltaTime);

        if(_as) _as.volume = Constants._musicVolume;

    }

    private void LoadAlready()
    {
        loadGameplay.allowSceneActivation = true;
    }

    public void Play()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            loadGameplay = SceneManager.LoadSceneAsync("GamePlay");
        }
        else
        {
            loadGameplay = SceneManager.LoadSceneAsync("Menu");
            Time.timeScale = 1;
        }

        loadGameplay.allowSceneActivation = false;

        _progressBar.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        _progressText.gameObject.SetActive(true);
        _loadBackg.gameObject.SetActive(true);

        _fadeImage.raycastTarget = true;

        _bIsLoading = true;

    }

    public void Tutorial()
    {
        loadGameplay = SceneManager.LoadSceneAsync("Tutorial");

        loadGameplay.allowSceneActivation = false;

        _progressBar.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        _progressText.gameObject.SetActive(true);
        _loadBackg.gameObject.SetActive(true);

        _fadeImage.raycastTarget = true;

        _bIsLoading = true;
    }

    public void OpenOptions()
    {
        _optionsPanel.SetActive(true);
        _guidePanel.SetActive(false);
        _controlsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _exitPanel.SetActive(false);
        if(SceneManager.GetActiveScene().name.Equals("GamePlay")) _tomenuPanel.SetActive(false);
    }

    public void OpenGuide()
    {
        _optionsPanel.SetActive(false);
        _guidePanel.SetActive(true);
        _controlsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _exitPanel.SetActive(false);
        if (SceneManager.GetActiveScene().name.Equals("GamePlay")) _tomenuPanel.SetActive(false);
    }

    public void OpenControls()
    {
        _optionsPanel.SetActive(false);
        _guidePanel.SetActive(false);
        _controlsPanel.SetActive(true);
        _creditsPanel.SetActive(false);
        _exitPanel.SetActive(false);
        if (SceneManager.GetActiveScene().name.Equals("GamePlay")) _tomenuPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        _optionsPanel.SetActive(false);
        _guidePanel.SetActive(false);
        _controlsPanel.SetActive(false);
        _creditsPanel.SetActive(true);
        _exitPanel.SetActive(false);
        if (SceneManager.GetActiveScene().name.Equals("GamePlay")) _tomenuPanel.SetActive(false);
    }

    public void OpenExit()
    {
        _optionsPanel.SetActive(false);
        _guidePanel.SetActive(false);
        _controlsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _exitPanel.SetActive(true);
        if (SceneManager.GetActiveScene().name.Equals("GamePlay")) _tomenuPanel.SetActive(false);
    }

    public void OpenToMenu()
    {
        _optionsPanel.SetActive(false);
        _guidePanel.SetActive(false);
        _controlsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _exitPanel.SetActive(false);
        _tomenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        _optionsPanel.SetActive(false);
    }

    public void CloseGuide()
    {
        _guidePanel.SetActive(false);
    }

    public void CloseControls()
    {
        _controlsPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        _creditsPanel.SetActive(false);
    }

    public void CloseExit()
    {
        _exitPanel.SetActive(false);
    }

    public void CloseToMenu()
    {
        _tomenuPanel.SetActive(false);
    }

    public void SetFxVolume()
    {
        _FXvolumeText.text = Mathf.Floor(_FXvolumeSlider.value * 100).ToString();
        Constants._fxVolume = _FXvolumeSlider.value;

        PlayerPrefs.SetFloat("FXVolume", _FXvolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        _musicVolumeText.text = Mathf.Floor(_musicVolumeSlider.value * 100).ToString();
        Constants._musicVolume = _musicVolumeSlider.value;

        PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
    }

    public void SetFullScreem()
    {
        PlayerPrefs.SetInt("Fullscreen", _fullScreenToggle.isOn ? 1 : 0);
        Screen.fullScreen = _fullScreenToggle.isOn;
    }


    public void Exit()
    {
        Application.Quit();
    }


}
