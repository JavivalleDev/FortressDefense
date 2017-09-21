using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts.Buildings;
using UnityEngine;

public class BuildingImageController : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private Building _building;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_building.GetLevel() == 0)
        {
            _sprite.color = _building.BUpdatingInterface ? new Color(0, 1, 0, .5f) : new Color(.24f, .75f, 1, .5f);
        }
        else _sprite.color = _building.BUpdatingInterface ? new Color(1, .92f, .16f, .5f) : new Color(1, 1, 1, .15f);
    }
}
