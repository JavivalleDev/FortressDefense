using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySounds : MonoBehaviour
{

    private AudioSource _as;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.playOnAwake = false;
        _as.spatialBlend = 1;
        _as.rolloffMode = AudioRolloffMode.Linear;
        _as.maxDistance = 100;
        _as.dopplerLevel = 0;
    }

    private void Update()
    {
        _as.volume = Constants._fxVolume;
    }

    public void PlaySound(SoundPool.ESounds sound)
    {
        _as.PlayOneShot(SoundPool.Instance.GetAudioClip(sound));
    }

    public void PlayArrow()
    {
        _as.PlayOneShot(SoundPool.Instance.GetArrowClip());
    }

    public void PlaySword()
    {
        _as.PlayOneShot(SoundPool.Instance.GetSwordClip());
    }

    public void PlaySpear()
    {
        _as.PlayOneShot(SoundPool.Instance.GetSpearClip());
    }

    public void PlayShout()
    {
        _as.PlayOneShot(SoundPool.Instance.GetShoutClip());
    }

    public void PlayDead()
    {
        _as.PlayOneShot(SoundPool.Instance.GetDeadClip());
    }
}
