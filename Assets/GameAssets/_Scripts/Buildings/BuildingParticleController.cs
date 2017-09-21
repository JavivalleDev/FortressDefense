using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts.Buildings;
using UnityEngine;

public class BuildingParticleController : MonoBehaviour
{
    private ParticleSystem _ps;
    [SerializeField] private Building _building;

    private bool bupdating;
    private bool firstCheckBuilt;
    private bool firstCheckDestroyed;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }
    
    private void Update()
    {
        if (_building.GetLevel() == 0)
        {
            if (bupdating && !firstCheckDestroyed)
            {
                _ps.Clear();
                _ps.Stop();
                _ps.startColor = _building.BUpdatingInterface ? Color.green : Color.red;
                _ps.Play();
                firstCheckDestroyed = true;
            }

            if (bupdating != _building.BUpdatingInterface)
            {
                _ps.Clear();
                _ps.Stop();
                _ps.startColor = _building.BUpdatingInterface ? Color.green : Color.red;
                _ps.Play();
            }

            firstCheckBuilt = false;

        }
        
        else if (_building.GetLevel() == 1)
        {
            if (bupdating && !firstCheckBuilt)
            {
                _ps.Clear();
                _ps.Stop();
                _ps.startColor = _building.BUpdatingInterface ? Color.yellow : Color.clear;
                _ps.Play();
                firstCheckBuilt = true;
            }

            if (bupdating != _building.BUpdatingInterface)
            {
                _ps.Clear();
                _ps.Stop();
                _ps.startColor = _building.BUpdatingInterface ? Color.yellow : Color.clear;
                _ps.Play();
            }

            firstCheckDestroyed = false;
        }

        bupdating = _building.BUpdatingInterface;
    }
}
