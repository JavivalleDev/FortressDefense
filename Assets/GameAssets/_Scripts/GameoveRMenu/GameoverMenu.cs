using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverMenu : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressText;
    [SerializeField] private Text _loadText;
    [SerializeField] private Image _loadBackg;

    private AsyncOperation loadGameplay;
    private bool _bIsLoading;

    private AudioSource _as;

    private void Awake()
    {
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

        if (_bIsLoading) _fadeImage.color += new Color(0, 0, 0, Time.deltaTime);

        if (_as) _as.volume = Constants._musicVolume;

    }

    private void LoadAlready()
    {
        loadGameplay.allowSceneActivation = true;
    }

    public void LoadScene()
    {
        loadGameplay.allowSceneActivation = false;

        _progressBar.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        _progressText.gameObject.SetActive(true);
        _loadBackg.gameObject.SetActive(true);

        _fadeImage.raycastTarget = true;

        _bIsLoading = true;

    }

    public void TryAgain()
    {
        loadGameplay = SceneManager.LoadSceneAsync("GamePlay");
        loadGameplay.allowSceneActivation = false;
        LoadScene();
    }

    public void ToMenu()
    {
        loadGameplay = SceneManager.LoadSceneAsync("Menu");
        loadGameplay.allowSceneActivation = false;
        LoadScene();
    }

    public void Exit()
    {
        Application.Quit();
    }


}

