using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.GameAssets._Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameAssets._Scripts.Managers
{
    /*
     * Gestor de unidades disponibles para el jugador y de los niveles actuales de estas
     * Add
     * Remove
     * Checks
     */
    public class PlayerUnitManager : MonoBehaviour
    {

        public static PlayerUnitManager Instance { get; set; }

        public enum EUnitType
        {
            Archer,
            Swordsman,
            Spearman,
            Tower
        }

        private int _iArchersAvalaible;
        private int _iSwordsAvalaible;
        private int _iSpearsAvalaible;

        #region unit stats
        private int _archerHP;
        private int _archerDMG;
        private int _archerRNG;
        public int _archerLVL { get; private set; }
        private int _swordsmanHP;
        private int _swordsmanDMG;
        private int _swordsmanRNG;
        public int _swordsmanLVL { get; private set; }
        private int _spearmanHP;
        private int _spearmanDMG;
        private int _spearmanRNG;
        public int _spearmanLVL { get; private set; }

        private int _archerBaseHP;
        private int _archerBaseDMG;
        private int _archerBaseRNG;
        private int _swordsmanBaseHP;
        private int _swordsmanBaseDMG;
        private int _swordsmanBaseRNG;
        private int _spearmanBaseHP;
        private int _spearmanBaseDMG;
        private int _spearmanBaseRNG;
        #endregion

        #region unit costs
        private int _recruitBaseBowCost;
        private int _recruitBaseSwordCost;
        private int _recruitBaseSpearCost;

        private int _recruitBowCost;
        public int _recruitSwordCost { get; private set; }
        public int _recruitSpearCost { get; private set; }

        private int _upgradeBaseBowCost;
        private int _upgradeBaseSwordCost;
        private int _upgradeBaseSpearCost;

        public int _upgradeBowCost { get; private set; }
        public int _upgradeSwordCost { get; private set; }
        public int _upgradeSpearCost { get; private set; }
        #endregion

        private float CheckRate;
        private float CheckTime;

        private bool _bPlacingUnit;
        private EUnitType _placingUnitType;

        public List<PlayerUnit> _activeArchers;
        public List<PlayerUnit> _activeSwordsmen;
        public List<PlayerUnit> _activeSpearmen;

        [SerializeField] private GameObject _placePoints;
        private float _placeTime, _placeRate = .1f;

        private bool isUp;
        private bool isPlaced;

        private void Awake()
        {
            #region init
            _archerLVL = 1;
            _swordsmanLVL = 1;
            _spearmanLVL = 1;

            _recruitBaseBowCost = 10;
            _recruitBaseSwordCost = 10;
            _recruitBaseSpearCost = 10;

            _recruitBowCost = _recruitBaseBowCost;
            _recruitSwordCost = _recruitBaseSwordCost;
            _recruitSpearCost = _recruitBaseSpearCost;

            _upgradeBaseBowCost = 5;
            _upgradeBaseSwordCost = 5;
            _upgradeBaseSpearCost = 5;

            _upgradeBowCost = _upgradeBaseBowCost;
            _upgradeSwordCost = _upgradeBaseSwordCost;
            _upgradeSpearCost = _upgradeBaseSpearCost;

            _archerBaseHP = 25;
            _archerBaseDMG = 15;
            _archerBaseRNG = 6;
            _swordsmanBaseHP = 40;
            _swordsmanBaseDMG = 25;
            _swordsmanBaseRNG = 1;
            _spearmanBaseHP = 60;
            _spearmanBaseDMG = 12;
            _spearmanBaseRNG = 2;

            _archerHP = _archerBaseHP;
            _archerDMG = _archerBaseDMG;
            _archerRNG = _archerBaseRNG;
            _swordsmanHP = _swordsmanBaseHP;
            _swordsmanDMG = _swordsmanBaseDMG;
            _swordsmanRNG = _swordsmanBaseRNG;
            _spearmanHP = _spearmanBaseHP;
            _spearmanDMG = _spearmanBaseDMG;
            _spearmanRNG = _spearmanBaseRNG;

            _activeArchers = new List<PlayerUnit>();
            _activeSwordsmen = new List<PlayerUnit>();
            _activeSpearmen = new List<PlayerUnit>();
            #endregion

            _placePoints.SetActive(false);

            Instance = this;
        }

        private void Start()
        {
            InterfaceManager.Instance.SetRecruitUpgradePlaceActions(RecuitArcher, RecruitSwordsman, RecruitSpearman,
                UpgradeArcher, UpgradeSwordsman, UpgradeSpearman,
                PlaceArcher, PlaceSwordsman, PlaceSpearman);
        }

        private void Update()
        {
            if (Time.time > CheckRate + CheckTime) SetInterfaceDetails();

            isUp = Input.GetMouseButtonUp(0);

            _upgradeBowCost = (_upgradeBaseBowCost * _archerLVL) + (_upgradeBaseBowCost * _activeArchers.Count * _archerLVL); 
            _upgradeSwordCost = (_upgradeBaseSwordCost * _swordsmanLVL) + (_upgradeBaseSwordCost * _activeSwordsmen.Count * _swordsmanLVL);
            _upgradeSpearCost = (_upgradeBaseSpearCost * _spearmanLVL) + (_upgradeBaseSpearCost * _activeSpearmen.Count * _spearmanLVL);
            
            if (!_bPlacingUnit) return;
            PlaceUnit();
        }

        private void SetInterfaceDetails()
        {
            CheckTime = Time.time;

            bool bRecruitArcher = ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Bow, _recruitBowCost);
            bool bRecruitSwordsman = ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Sword, _recruitSwordCost);
            bool bRecruitSpearman = ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Spear, _recruitSpearCost);
            bool bPlaceArcher = CheckUnitAvailable(EUnitType.Archer);
            bool bPlaceSwordsman = CheckUnitAvailable(EUnitType.Swordsman);
            bool bPlaceSpearman = CheckUnitAvailable(EUnitType.Spearman);
            bool bUpgradeArcher = CheckLevel(EUnitType.Archer) && ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Bow, _upgradeBowCost);
            bool bUpgradeSwordsman = CheckLevel(EUnitType.Swordsman) && ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Sword, _upgradeSwordCost);
            bool bUpgradeSpearman = CheckLevel(EUnitType.Spearman) && ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Spear, _upgradeSpearCost);

            InterfaceManager.Instance.SetUnitStatsData(_archerHP, _archerDMG, _archerRNG, _archerLVL,
                _swordsmanHP, _swordsmanDMG, _swordsmanRNG, _swordsmanLVL,
                _spearmanHP, _spearmanDMG, _spearmanRNG, _spearmanLVL);

            InterfaceManager.Instance.SetRecruitData(bRecruitArcher, bRecruitSwordsman, bRecruitSpearman,
                bPlaceArcher, bPlaceSwordsman, bPlaceSpearman,
                _recruitBowCost, _recruitSwordCost, _recruitSpearCost,
                _iArchersAvalaible, _iSwordsAvalaible, _iSpearsAvalaible);

            InterfaceManager.Instance.SetUpgradeData(bUpgradeArcher, bUpgradeSwordsman, bUpgradeSpearman,
                _upgradeBowCost, _upgradeSwordCost, _upgradeSpearCost);

            InterfaceManager.Instance.SetArchersAvailable(_activeArchers.Count);
            InterfaceManager.Instance.SetSwordmenAvailable(_activeSwordsmen.Count);
            InterfaceManager.Instance.SetSpearmenAvailable(_activeSpearmen.Count);
        }

        #region unit management
        private int GetUnitAvailable(EUnitType type)
        {
            switch (type)
            {
                case EUnitType.Archer:
                    return _iArchersAvalaible;

                case EUnitType.Swordsman:
                    return _iSwordsAvalaible;

                case EUnitType.Spearman:
                    return _iSpearsAvalaible;

                default:
                    return 0;
            }
        }

        private void SetUnitAvailable(EUnitType type, int qty = 1)
        {
            switch (type)
            {
                case EUnitType.Archer:
                    _iArchersAvalaible = qty;
                    break;

                case EUnitType.Swordsman:
                    _iSwordsAvalaible = qty;
                    break;

                case EUnitType.Spearman:
                    _iSpearsAvalaible = qty;
                    break;

                default:
                    return;
            }
        }

        private void AddUnitAvailable(EUnitType type, int qty = 1)
        {
            int unit = GetUnitAvailable(type);
            unit += qty;
            SetUnitAvailable(type, unit);
        }

        private bool CheckUnitAvailable(EUnitType type, int qty = 1)
        {
            int unit = GetUnitAvailable(type);

            return qty <= unit;
        }

        private void SetLevel(EUnitType type, int lvl)
        {
            switch (type)
            {
                case EUnitType.Archer:
                    _archerLVL = lvl;
                    _archerHP = _archerBaseHP * _archerLVL;
                    _archerDMG = _archerBaseDMG * _archerLVL;
                    _recruitBowCost = _recruitBaseBowCost * _archerLVL;
                    _upgradeBowCost = _upgradeBaseBowCost * _archerLVL;
                    break;

                case EUnitType.Swordsman:
                    _swordsmanLVL = lvl;
                    _swordsmanHP = _swordsmanBaseHP * _swordsmanLVL;
                    _swordsmanDMG = _swordsmanBaseDMG * _swordsmanLVL;
                    _recruitSwordCost = _recruitBaseSwordCost * _swordsmanLVL;
                    _upgradeSwordCost = _upgradeBaseSwordCost * _swordsmanLVL;
                    break;

                case EUnitType.Spearman:
                    _spearmanLVL = lvl;
                    _spearmanHP = _spearmanBaseHP * _spearmanLVL;
                    _spearmanDMG = _spearmanBaseDMG * _spearmanLVL;
                    _recruitSpearCost = _recruitBaseSpearCost * _spearmanLVL;
                    _upgradeSpearCost = _upgradeBaseSpearCost * _spearmanLVL;
                    break;
            }
        }

        private void AddLevel(EUnitType type)
        {
            switch (type)
            {
                case EUnitType.Archer:
                    _archerLVL++;
                    SetLevel(EUnitType.Archer, _archerLVL);
                    break;

                case EUnitType.Swordsman:
                    _swordsmanLVL++;
                    SetLevel(EUnitType.Swordsman, _swordsmanLVL);
                    break;

                case EUnitType.Spearman:
                    _spearmanLVL++;
                    SetLevel(EUnitType.Spearman, _spearmanLVL);
                    break;
            }
        }

        #endregion

        #region unit recruitment
        private void RecuitArcher()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.RecuitArcher);

            if (Input.GetKey(KeyCode.LeftShift) &&
                ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Bow, _recruitBowCost * 5))
            {
                ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Bow, _recruitBowCost * 5);
                AddUnitAvailable(EUnitType.Archer);
                AddUnitAvailable(EUnitType.Archer);
                AddUnitAvailable(EUnitType.Archer);
                AddUnitAvailable(EUnitType.Archer);
                AddUnitAvailable(EUnitType.Archer);
            }
            else
            {
                ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Bow, _recruitBowCost);
                AddUnitAvailable(EUnitType.Archer);
            }
        }

        private void RecruitSwordsman()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.RecuitSwordsman);

            if (Input.GetKey(KeyCode.LeftShift) &&
                ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Sword, _recruitSwordCost * 5))
            {
                ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Sword, _recruitSwordCost * 5);
                AddUnitAvailable(EUnitType.Swordsman);
                AddUnitAvailable(EUnitType.Swordsman);
                AddUnitAvailable(EUnitType.Swordsman);
                AddUnitAvailable(EUnitType.Swordsman);
                AddUnitAvailable(EUnitType.Swordsman);
            }
            else
            {
                ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Sword, _recruitSwordCost);
                AddUnitAvailable(EUnitType.Swordsman);
            }
        }

        private void RecruitSpearman()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.RecuitSpearman);

            if (Input.GetKey(KeyCode.LeftShift) &&
                ResourceManager.Instance.CheckResource(ResourceManager.EResourceType.Spear, _recruitSpearCost * 5))
            {
                ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Spear, _recruitSpearCost * 5);
                AddUnitAvailable(EUnitType.Spearman);
                AddUnitAvailable(EUnitType.Spearman);
                AddUnitAvailable(EUnitType.Spearman);
                AddUnitAvailable(EUnitType.Spearman);
                AddUnitAvailable(EUnitType.Spearman);
            }
            else
            {
                ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Spear, _recruitSpearCost);
                AddUnitAvailable(EUnitType.Spearman);
            }
        }

        #endregion

        #region unit upgrade
        private void UpgradeArcher()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.UpgradeUnit);

            ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Bow, _upgradeBowCost);
            AddLevel(EUnitType.Archer);
            foreach (var unit in _activeArchers)
            {
                unit.SetLevel(_archerLVL);
            }
        }

        private void UpgradeSwordsman()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.UpgradeUnit);

            ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Sword, _upgradeSwordCost);
            AddLevel(EUnitType.Swordsman);
            foreach (var unit in _activeSwordsmen)
            {
                unit.SetLevel(_swordsmanLVL);
            }
        }

        private void UpgradeSpearman()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.UpgradeUnit);

            ResourceManager.Instance.TakeResource(ResourceManager.EResourceType.Spear, _upgradeSpearCost);
            AddLevel(EUnitType.Spearman);
            foreach (var unit in _activeSpearmen)
            {
                unit.SetLevel(_spearmanLVL);
            }
        }

        private bool CheckLevel(EUnitType type)
        {
            int currentLevel = 0;

            switch (type)
            {
                case EUnitType.Archer:
                    currentLevel = _archerLVL;
                    break;

                case EUnitType.Swordsman:
                    currentLevel = _swordsmanLVL;
                    break;

                case EUnitType.Spearman:
                    currentLevel = _spearmanLVL;
                    break;
            }

            return currentLevel < RequirementManager.Instance.iBlacksmithLevelLevel;
        }
        #endregion

        #region unit placement

        private void PlaceArcher()
        {
            _bPlacingUnit = true;
            _placingUnitType = EUnitType.Archer;
        }

        private void PlaceSwordsman()
        {
            _bPlacingUnit = true;
            _placingUnitType = EUnitType.Swordsman;
        }

        private void PlaceSpearman()
        {
            _bPlacingUnit = true;
            _placingUnitType = EUnitType.Spearman;
        }

        private void PlaceUnit()
        {
            _placePoints.SetActive(true);

            int count = 0;

            Vector3 frontCorner = new Vector3(250, 0, 500);
            Vector3 backCorner = new Vector3(250, 0, 0);
            Vector3 leftCorner = new Vector3(0, 0, 250);
            Vector3 rightCorner = new Vector3(500, 0, 250);

            if (isUp && isPlaced)
            {
                _placePoints.SetActive(false);

                _bPlacingUnit = false;

                isPlaced = false;
            }


            if (!Input.GetMouseButton(0) || Time.time < _placeTime + _placeRate) return;

            isPlaced = true;

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                if (hit.transform.CompareTag("PlacePoint") || (hit.transform.CompareTag("WallPlacePoint") && _placingUnitType == EUnitType.Archer))
                {
                    Vector3 hitPos = hit.point;

                    PlayerUnit newUnit = UnitPoolManager.Instance.TakeUnit(false, _placingUnitType)
                        .GetComponent<PlayerUnit>();

                    switch (_placingUnitType)
                    {
                        case EUnitType.Archer:
                            _iArchersAvalaible--;
                            _activeArchers.Add(newUnit);
                            newUnit.SetLevel(_archerLVL);
                            break;

                        case EUnitType.Swordsman:
                            _iSwordsAvalaible--;
                            _activeSwordsmen.Add(newUnit);
                            newUnit.SetLevel(_swordsmanLVL);
                            break;

                        case EUnitType.Spearman:
                            _iSpearsAvalaible--;
                            _activeSpearmen.Add(newUnit);
                            newUnit.SetLevel(_spearmanLVL);
                            break;
                    }

                    float distanceFront = Vector3.Distance(hit.point, frontCorner);
                    float distanceBack = Vector3.Distance(hit.point, backCorner);
                    float distanceLeft = Vector3.Distance(hit.point, leftCorner);
                    float distanceRight = Vector3.Distance(hit.point, rightCorner);
                    Quaternion unitRotation = Quaternion.identity;

                    if (distanceFront >= distanceBack && distanceFront >= distanceLeft &&
                        distanceFront >= distanceRight) unitRotation = Quaternion.Euler(0, 180, 0);
                    else if (distanceBack >= distanceFront && distanceBack >= distanceLeft &&
                             distanceBack >= distanceRight) unitRotation = Quaternion.Euler(0, 0, 0);
                    else if (distanceLeft >= distanceBack && distanceLeft >= distanceFront &&
                             distanceLeft >= distanceRight) unitRotation = Quaternion.Euler(0, 90, 0);
                    else if (distanceRight >= distanceBack && distanceRight >= distanceLeft &&
                             distanceRight >= distanceFront) unitRotation = Quaternion.Euler(0, 270, 0);

                    newUnit.SetInitialPosition(hitPos, unitRotation);

                    newUnit.transform.position = hitPos;
                    if (hit.transform.CompareTag("WallPlacePoint"))
                    {
                        newUnit._bUnitWall = true;
                    }
                    newUnit.gameObject.SetActive(true);

                    PlaySounds2D.Instance.PlayPlaceSound();

                    _placeTime = Time.time;
                }
            }

            switch (_placingUnitType)
            {
                case EUnitType.Archer:
                    count = _iArchersAvalaible;
                    break;

                case EUnitType.Swordsman:
                    count = _iSwordsAvalaible;
                    break;

                case EUnitType.Spearman:
                    count = _iSpearsAvalaible;
                    break;
            }

            if (count > 0) return;

            _placePoints.SetActive(false);

            _bPlacingUnit = false;

            isPlaced = false;

        }

        #endregion



        //#region General
        //private void AddUnits(int qty = 1)
        //{
        //    _iArchersAvalaible += qty;
        //    _iSwordsAvalaible += qty;
        //    _iSpearsAvalaible += qty;
        //}

        //private void RemoveUnits(int qty = 1)
        //{
        //    if (!CheckRemoveUnits(qty)) return;
        //    _iArchersAvalaible -= qty;
        //    _iSwordsAvalaible -= qty;
        //    _iSpearsAvalaible -= qty;
        //}

        //private bool CheckRemoveUnits(int qty = 1)
        //{
        //    return
        //        (
        //            qty <= _iArchersAvalaible &&
        //            qty <= _iSwordsAvalaible &&
        //            qty <= _iSpearsAvalaible
        //        );
        //}
        //#endregion



    }
}
