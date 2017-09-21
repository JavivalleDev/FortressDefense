using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoUI : MonoBehaviour
{
    [SerializeField] private GameObject _resourcePanel;
    [SerializeField] private GameObject _buildingPanel;
    [SerializeField] private GameObject _timePanel;
    [SerializeField] private GameObject _unitPanel;
    [SerializeField] private GameObject _iconsPanel;

    [SerializeField] private GameObject _resourceButton;
    [SerializeField] private GameObject _buildingButton;
    [SerializeField] private GameObject _timeButton;
    [SerializeField] private GameObject _unitButton;
    [SerializeField] private GameObject _iconsButton;

    [SerializeField] private Canvas _menu;

    public void ResourceButton()
    {
        _resourcePanel.SetActive(false);
        _buildingPanel.SetActive(true);
    }

    public void BuildingButton()
    {
        _buildingPanel.SetActive(false);
        _timePanel.SetActive(true);
    }

    public void TimeButton()
    {
        _timePanel.SetActive(false);
        _unitPanel.SetActive(true);
    }

    public void UnitButton()
    {
        _unitPanel.SetActive(false);
        _iconsPanel.SetActive(true);
    }

    public void IconsButton()
    {
        _iconsPanel.SetActive(false);
        _menu.enabled = true;
    }
}
