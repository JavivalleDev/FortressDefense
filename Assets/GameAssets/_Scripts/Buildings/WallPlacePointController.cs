using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts;
using Assets.GameAssets._Scripts.Units;
using UnityEngine;

public class WallPlacePointController : MonoBehaviour
{
    [SerializeField] private Target _wall;
    
    void Update()
    {
        if (_wall.GetLevel() == 0)
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if (_wall.GetLevel() == 1)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
