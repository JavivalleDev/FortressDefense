using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientMusic : MonoBehaviour
{
    private AudioSource _as;

    [SerializeField] private AudioClip _peaceClip;
    [SerializeField] private AudioClip _warClip;

    private bool switching;
    private bool _peace;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.clip = _peaceClip;
        
        _as.Play();

        _as.spatialBlend = 0;
        _as.loop = true;
    }

    private void Update()
    {
        if (!switching)
        {
            _as.volume = Constants._musicVolume;
        }
    }

    public void SetPeaceMusic(bool peace)
    {
        switching = true;
        _peace = peace;
        InvokeRepeating("TurnDownVolume", 0, .5f);
    }

    private void TurnDownVolume()
    {
        _as.volume -= .1f;
        if (_as.volume < .1f)
        {
            InvokeRepeating("TurnUpVolume", 0, .5f);
        }
    }

    private void TurnUpVolume()
    {
        CancelInvoke("TurnDownVolume");

        _as.clip = _peace ? _peaceClip : _warClip;

        _as.volume += .1f;

        if (_as.volume >= Constants._musicVolume)
        {
            switching = false;
            _as.Play();
            CancelInvoke("TurnUpVolume");
        }
    }
}
