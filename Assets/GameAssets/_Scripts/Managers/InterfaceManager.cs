using System;
using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts.Buildings;
using Assets.GameAssets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    public static InterfaceManager Instance { get; set; }

    [Header("Round")]
    [SerializeField] private Text _roundText;
    [SerializeField] private Text _timeText;

    [Header("Resources")]
    [SerializeField] private Text _goldText;
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _woodText;
    [SerializeField] private Text _ironText;
    [SerializeField] private Text _bowText;
    [SerializeField] private Text _swordText;
    [SerializeField] private Text _spearText;
    [SerializeField] private Text _populationText;

    [Header("Building")]
    private Building _previousBuilding;
    [SerializeField] private Text _buildingName;
    [SerializeField] private Text _buildingLevel;
    [SerializeField] private Image _buildingImage;
    [SerializeField] private Text _goldUpgradeText;
    [SerializeField] private Text _foodUpgradeText;
    [SerializeField] private Text _woodUpgradeText;
    [SerializeField] private Text _ironUpgradeText;
    [SerializeField] private Text _goldRepairText;
    [SerializeField] private Text _foodRepairText;
    [SerializeField] private Text _woodRepairText;
    [SerializeField] private Text _ironRepairText;
    [SerializeField] private Text _HPText;
    [SerializeField] private Image _buildingHPBar;
    [SerializeField] private Button _buildingUpgradeButton;
    [SerializeField] private Button _buildingRepairButton;
    [SerializeField] private Image _upgradeBar;
    [SerializeField] private Image _repairBar;

    [Header("Recruit Units")]
    [SerializeField] private Button _recruitArcherButton;
    [SerializeField] private Text _recruitArcherCostText;
    [SerializeField] private Text _archersAvailableText;
    [SerializeField] private Button _recruitSwordsmanButton;
    [SerializeField] private Text _recruitSwordsmanCostText;
    [SerializeField] private Text _swordsmenAvailableText;
    [SerializeField] private Button _recruitSpearmanButton;
    [SerializeField] private Text _recruitSpearmanCostText;
    [SerializeField] private Text _spearmenAvailableText;
    [SerializeField] private Button _placeArcherButton;
    [SerializeField] private Button _placeSwordsmanButton;
    [SerializeField] private Button _placeSpearmanButton;

    [Header("Upgrade Units")]
    [SerializeField] private Button _upgradeArcherButton;
    [SerializeField] private Button _upgradeSwordsmanButton;
    [SerializeField] private Button _upgradeSpearmanButton;
    [SerializeField] private Text _upgradeBowCostText;
    [SerializeField] private Text _upgradeSwordCostText;
    [SerializeField] private Text _upgradeSpearCostText;

    [Header("Units Stats")]
    [SerializeField] private Text _archerHPText;
    [SerializeField] private Text _archerDMGText;
    //[SerializeField] private Text _archerRNGText;
    [SerializeField] private Text _archerLVLText;
    [SerializeField] private Text _swordsmanHPText;
    [SerializeField] private Text _swordsmanDMGText;
    //[SerializeField] private Text _swordsmanRNGText;
    [SerializeField] private Text _swordsmanLVLText;
    [SerializeField] private Text _spearmanHPText;
    [SerializeField] private Text _spearmanDMGText;
    //[SerializeField] private Text _spearmanRNGText;
    [SerializeField] private Text _spearmanLVLText;

    [Header("Units Stats")]
    [SerializeField] private Text _archersPlacedText;
    [SerializeField] private Text _swordsmenPlacedText;
    [SerializeField] private Text _spearmenPlacedText;

    [SerializeField] private Text _damageText;
    [SerializeField] private Text _descrText;

    [Header("Time Control")]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _slowMoButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _x2Button;
    [SerializeField] private Button _x4Button;

    [Header("Panels")]
    [SerializeField] private GameObject _buildingPanel;
    [SerializeField] private GameObject _recruitPanel;
    [SerializeField] private GameObject _upgradetPanel;
    [SerializeField] private GameObject _placePanel;
    [SerializeField] private GameObject _statsPanel;
    [SerializeField] private GameObject _timePanel;
    [SerializeField] private Image _buildingPanelImage;
    [SerializeField] private Image _unitsPanelImage;
    [SerializeField] private Image _statsPanelImage;
    [SerializeField] private Image _timePanelImage;
    [SerializeField] private Image[] _buildingPanelImages;
    [SerializeField] private Image[] _unitsPanelImages;
    [SerializeField] private Image[] _statsPanelImages;
    [SerializeField] private Image[] _timePanelImages;

    private bool _bPaused;

    void Awake()
    {
        SetTimeControlButtons();
        Instance = this;
    }

    private void Update()
    {
        PauseButtons();
    }

    private void PauseButtons()
    {
        if (!_bPaused) return;

        _buildingRepairButton.interactable = false;
        _buildingUpgradeButton.interactable = false;
        _placeArcherButton.interactable = false;
        _placeSpearmanButton.interactable = false;
        _placeSwordsmanButton.interactable = false;
        _recruitArcherButton.interactable = false;
        _recruitSpearmanButton.interactable = false;
        _recruitSwordsmanButton.interactable = false;
        _upgradeArcherButton.interactable = false;
        _upgradeSpearmanButton.interactable = false;
        _upgradeSwordsmanButton.interactable = false;
    }

    #region time control
    private void SetTimeControlButtons()
    {
        _pauseButton.onClick.AddListener(PauseTime);
        _slowMoButton.onClick.AddListener(SlowMoTime);
        _playButton.onClick.AddListener(PlayTime);
        _x2Button.onClick.AddListener(Play2Time);
        _x4Button.onClick.AddListener(Play4Time);
    }

    public void PauseTime()
    {
        PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.TimeButton);
        Time.timeScale = 0;
        _bPaused = true;
    }

    public void SlowMoTime()
    {
        PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.TimeButton);
        Time.timeScale = 0.5f;
        _bPaused = false;
    }

    public void PlayTime()
    {
        PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.TimeButton);
        Time.timeScale = 1;
        _bPaused = false;
    }

    public void Play2Time()
    {
        PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.TimeButton);
        Time.timeScale = 2;
        _bPaused = false;
    }

    public void Play4Time()
    {
        PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.TimeButton);
        Time.timeScale = 4;
        _bPaused = false;
    }
    #endregion

    public void SetRoundData(int round, string time)
    {
        _roundText.text = round.ToString();
        _timeText.text = time;
    }

    public void SetResourcesData(int gold, int food, int wood, int stone, int bows, int swords, int spears, int pop)
    {
        _goldText.text = gold.ToString();
        _foodText.text = food.ToString();
        _woodText.text = wood.ToString();
        _ironText.text = stone.ToString();
        _bowText.text = bows.ToString();
        _swordText.text = swords.ToString();
        _spearText.text = spears.ToString();
        _populationText.text = pop.ToString();
    }

    public void SetBuildingData(Building building, string name, int level, Sprite image,
        int goldUpgrade, int foodUpgrade, int woodUpgrade, int stoneUpgrade,
        int goldRepair, int foodRepair, int woodRepair, int stoneRepair,
        int currentLife, int maxLife, bool upgrade, bool repair, System.Action upgradeAction, System.Action repairAction)
    {
        if (_previousBuilding && _previousBuilding != building) _previousBuilding.BUpdatingInterface = false;

        building.BUpdatingInterface = true;
        _previousBuilding = building;

        _buildingName.text = name;
        _buildingLevel.text = level.ToString();
        _buildingImage.sprite = image;

        _goldUpgradeText.text = goldUpgrade.ToString();
        _foodUpgradeText.text = foodUpgrade.ToString();
        _woodUpgradeText.text = woodUpgrade.ToString();
        _ironUpgradeText.text = stoneUpgrade.ToString();

        _goldUpgradeText.color = goldUpgrade > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Gold) ? Color.red : Color.white;
        _foodUpgradeText.color = foodUpgrade > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Food) ? Color.red : Color.white;
        _woodUpgradeText.color = woodUpgrade > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Wood) ? Color.red : Color.white;
        _ironUpgradeText.color = stoneUpgrade > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Stone) ? Color.red : Color.white;

        _goldRepairText.text = goldRepair.ToString();
        _foodRepairText.text = foodRepair.ToString();
        _woodRepairText.text = woodRepair.ToString();
        _ironRepairText.text = stoneRepair.ToString();

        _goldRepairText.color = goldRepair > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Gold) ? Color.red : Color.white;
        _foodRepairText.color = foodRepair > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Food) ? Color.red : Color.white;
        _woodRepairText.color = woodRepair > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Wood) ? Color.red : Color.white;
        _ironRepairText.color = stoneRepair > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Stone) ? Color.red : Color.white;

        _HPText.text = currentLife + "/" + maxLife;
        _buildingHPBar.fillAmount = (float) currentLife / (float) maxLife;
        _buildingUpgradeButton.interactable = upgrade;
        _buildingRepairButton.interactable = repair;
        
        _buildingUpgradeButton.onClick.RemoveAllListeners();
        _buildingRepairButton.onClick.RemoveAllListeners();

        _buildingUpgradeButton.onClick.AddListener(() => upgradeAction());
        _buildingRepairButton.onClick.AddListener(() => repairAction());
    }

    public void SetRecruitData(bool archer, bool swordsman, bool spearman, 
        bool archerAvailable, bool swordsmanAvailable, bool spearmanAvailable,
        int bowcost, int swordcost, int spearcost, 
        int archeravailable, int swordsmanavailable, int spearmanavailable)
    {
        _recruitArcherButton.interactable = archer;
        _recruitSwordsmanButton.interactable = swordsman;
        _recruitSpearmanButton.interactable = spearman;

        _placeArcherButton.interactable = archerAvailable;
        _placeSwordsmanButton.interactable = swordsmanAvailable;
        _placeSpearmanButton.interactable = spearmanAvailable;

        _recruitArcherCostText.text = bowcost.ToString();
        _recruitSwordsmanCostText.text = swordcost.ToString();
        _recruitSpearmanCostText.text = spearcost.ToString();

        _recruitArcherCostText.color = bowcost > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Bow) ? Color.red : Color.white;
        _recruitSwordsmanCostText.color = swordcost > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Sword) ? Color.red : Color.white;
        _recruitSpearmanCostText.color = spearcost > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Spear) ? Color.red : Color.white;

        _archersAvailableText.text = archeravailable.ToString();
        _swordsmenAvailableText.text = swordsmanavailable.ToString();
        _spearmenAvailableText.text = spearmanavailable.ToString();
    }

    public void SetUpgradeData(bool archer, bool swordsman, bool spearman, int bowcost, int swordcost, int spearcost)
    {
        _upgradeArcherButton.interactable = archer;
        _upgradeSwordsmanButton.interactable = swordsman;
        _upgradeSpearmanButton.interactable = spearman;
        _upgradeBowCostText.text = bowcost.ToString();
        _upgradeSwordCostText.text = swordcost.ToString();
        _upgradeSpearCostText.text = spearcost.ToString();

        _upgradeBowCostText.color = bowcost > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Bow) ? Color.red : Color.white;
        _upgradeSwordCostText.color = swordcost > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Sword) ? Color.red : Color.white;
        _upgradeSpearCostText.color = spearcost > ResourceManager.Instance.GetResource(ResourceManager.EResourceType.Spear) ? Color.red : Color.white;
    }

    public void SetRecruitUpgradePlaceActions(System.Action recruitArcher, System.Action recruitSwordsman,System.Action recruitSpearman,
        System.Action upgradeArcher, System.Action upgradeSwordsman, System.Action upgradeSpearman,
        System.Action placeArcher, System.Action placeSwordsman, System.Action placeSpearman)
    {
        _recruitArcherButton.onClick.AddListener(() => recruitArcher());
        _recruitSwordsmanButton.onClick.AddListener(() => recruitSwordsman());
        _recruitSpearmanButton.onClick.AddListener(() => recruitSpearman());

        _upgradeArcherButton.onClick.AddListener(() => upgradeArcher());
        _upgradeSwordsmanButton.onClick.AddListener(() => upgradeSwordsman());
        _upgradeSpearmanButton.onClick.AddListener(() => upgradeSpearman());

        _placeArcherButton.onClick.AddListener(() => placeArcher());
        _placeSwordsmanButton.onClick.AddListener(() => placeSwordsman());
        _placeSpearmanButton.onClick.AddListener(() => placeSpearman());
    }

    public void SetUnitStatsData(int archerHP, int archerDMG, int archerRNG, int archerLVL,
        int swordsmanHP, int swordsmanDMG, int swordsmanRNG, int swordsmanLVL,
        int spearmanHP, int spearmanDMG, int spearsmanRNG, int spearsmanLVL)
    {
        _archerHPText     .text = archerHP.ToString();
        _archerDMGText    .text = archerDMG.ToString();
        //_archerRNGText    .text = archerRNG.ToString();
        _archerLVLText    .text = archerLVL.ToString();
        _swordsmanHPText  .text = swordsmanHP.ToString();
        _swordsmanDMGText .text = swordsmanDMG.ToString();
        //_swordsmanRNGText .text = swordsmanRNG.ToString();
        _swordsmanLVLText .text = swordsmanLVL.ToString();
        _spearmanHPText   .text = spearmanHP.ToString();
        _spearmanDMGText  .text = spearmanDMG.ToString();
        //_spearmanRNGText  .text = spearsmanRNG.ToString();
        _spearmanLVLText.text = spearsmanLVL.ToString();
    }

    public void SetArchersAvailable(int count)
    {
        _archersPlacedText.text = count.ToString();
    }

    public void SetSwordmenAvailable(int count)
    {
        _swordsmenPlacedText.text = count.ToString();
    }

    public void SetSpearmenAvailable(int count)
    {
        _spearmenPlacedText.text = count.ToString();
    }

    public void SetStatsText(Building.BuildingType type, int current, int next, string descr)
    {
        string damage = type == Building.BuildingType.Tower ? "damage" : "production";

        string text = "Curent " + damage + ": " + current
                      + "\nNext level " + damage + ": " + next;


        _damageText.text = next > 0 ? _damageText.text = text : _damageText.text = String.Empty;
        _descrText.text = descr;
    }

    public void SetUpgradeBarFillAmout(float amount)
    {
        _upgradeBar.fillAmount = amount;
    }

    public void SetRepairBarFillAmout(float amount)
    {
        _repairBar.fillAmount = amount;
    }

    public void CloseBuildingPanel()
    {
        _buildingPanel.SetActive(!_buildingPanel.activeSelf);

        foreach (var i in _buildingPanelImages) i.enabled = !i.enabled;

        _buildingPanelImage.color = _buildingPanel.activeSelf ? Color.red : Color.green;
    }

    public void CloseUnitsPanel()
    {
        _recruitPanel.SetActive(!_recruitPanel.activeSelf);
        _upgradetPanel.SetActive(!_upgradetPanel.activeSelf);
        _placePanel.SetActive(!_placePanel.activeSelf);

        foreach (var i in _unitsPanelImages) i.enabled = !i.enabled;

        _unitsPanelImage.color = _recruitPanel.activeSelf ? Color.red : Color.green;
    }

    public void CloseStatsPanel()
    {
        _statsPanel.SetActive(!_statsPanel.activeSelf);

        foreach (var i in _statsPanelImages) i.enabled = !i.enabled;

        _statsPanelImage.color = _statsPanel.activeSelf ? Color.red : Color.green;
    }

    public void CloseTimePanel()
    {
        _timePanel.SetActive(!_timePanel.activeSelf);

        foreach (var i in _timePanelImages) i.enabled = !i.enabled;

        _timePanelImage.color = _timePanel.activeSelf ? Color.red : Color.green;
    }
}

