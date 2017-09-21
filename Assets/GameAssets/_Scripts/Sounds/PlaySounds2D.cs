using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySounds2D : MonoBehaviour {

    private AudioSource _as;

    public static PlaySounds2D Instance { get; set; }

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.playOnAwake = false;
        _as.spatialBlend = 0;

        Instance = this;
    }

    private void Update()
    {
        _as.volume = Constants._musicVolume;
    }

    public void PlaySound(SoundPool.ESounds2D sound)
    {
        _as.PlayOneShot(SoundPool.Instance.GetAudioClip2D(sound));
    }

    public void PlayPlaceSound()
    {
        _as.PlayOneShot(SoundPool.Instance.GetPlaceClip2D());
    }
}
