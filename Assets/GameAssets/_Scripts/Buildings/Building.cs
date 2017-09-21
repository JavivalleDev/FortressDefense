using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Assets.GameAssets._Scripts.Managers;
using Assets.GameAssets._Scripts.Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.GameAssets._Scripts.Buildings
{
    /*Comportamiento basico de los edificios
     costes de recursos del edificio y 
     Mejorar()
     Reparar()
     Degradar()
     Demoler()
     Comprobar costes de accion()
     Calcular costes de accion()*/

    public class Building : Target
    {
        public enum BuildingType
        {
            Main,
            GoldMine,
            Farm,
            Sawmill,
            Quarry,
            Archery,
            Barracks,
            Guard,
            Storage,
            Blacksmith,
            Wall,
            Tower
        }

        [SerializeField] private int _startingLevel;

        [SerializeField] protected BuildingType type;
        [SerializeField] protected string _buildingName;
        [SerializeField] protected Sprite _buildingImage;

        [Header("Costs")]
        [SerializeField]
        protected int _iGoldCost;
        [SerializeField] protected int _iFoodCost;
        [SerializeField] protected int _iWoodCost;
        [SerializeField] protected int _iStoneCost;

        [Header("Availability Check")]
        protected float checkTime;
        [SerializeField] protected float checkRate;

        protected bool _bUpgradeable;
        protected bool _bDowngradeable;
        protected bool _bRepairable;
        protected bool _bDemolishable;
        public bool BUpdatingInterface;

        [Header("Building")]
        [SerializeField]
        private GameObject _constructionSite;

        private MeshRenderer[] _renderers;
        private SkinnedMeshRenderer[] _srenderers;
        private Collider[] _colliders;

        private int _repairTicks = 10;
        private int _repairTicksCurrent;
        private bool _bRepairing;

        private int _upgradeTicks = 10;
        private int _upgradeTicksCurrent;
        private bool _bUpgrading;

        private bool _bDamaged;
        private float _damagedTime;

        [SerializeField] private GameObject particlesObject;

        protected string description;

        protected override void Awake()
        {
            base.Awake();

            _renderers = GetComponentsInChildren<MeshRenderer>();
            _srenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            _colliders = GetComponentsInChildren<Collider>();

            switch (type)
            {
                case BuildingType.Main:
                    description =
                        "This is your main building. It's level is the max level for the rest of buildings";
                    break;

                case BuildingType.Archery:
                    description = "Produces Bows so that you can recuit archers";
                    break;

                case BuildingType.Barracks:
                    description = "Produces Swords so that you can recuit swordsmen";
                    break;

                case BuildingType.Guard:
                    description = "Produces Spears so that you can recuit spearmen";
                    break;

                case BuildingType.Blacksmith:
                    description = "Allows you to upgrade your units. It's level is the max level of your units";
                    break;

                case BuildingType.GoldMine:
                    description = "Produces Gold";
                    break;

                case BuildingType.Quarry:
                    description = "Produces Stone";
                    break;

                case BuildingType.Farm:
                    description = "Produces Wheat";
                    break;

                case BuildingType.Sawmill:
                    description = "Produces Wood";
                    break;

                case BuildingType.Wall:
                    description = "Strong defensive building. Allows you to place archers on it, to increase their range and keep them safe";
                    break;

                case BuildingType.Tower:
                    description = "Fires at enemy units";
                    break;
            }

            _bDemolishable = false;
        }

        protected void Start()
        {

            SetLevel(_startingLevel);

            if (type == BuildingType.Main)
            {
                RequirementManager.Instance.SetLevel(type, _iLevel);
                BUpdatingInterface = true;
            }
        }

        protected virtual void Update()
        {
            particlesObject.SetActive(_iLevel > 0 && iCurrentLife <= iMaxLife * (2 / 3f));

            if (GetLevel() < 1)
            {
                foreach (var c in _colliders)
                {
                    c.isTrigger = true;
                    if (!c.Equals(GetComponent<Collider>())) c.enabled = false;
                }
                foreach (var r in _renderers)
                {
                    r.enabled = false;
                    if (r.name.Equals("Image")) r.enabled = true;
                }
                foreach (var r in _srenderers) r.enabled = false;
                if (_constructionSite) foreach (var r in _constructionSite.GetComponentsInChildren<MeshRenderer>()) r.enabled = true;
                //GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            else if (GetLevel() == 1)
            {
                foreach (var c in _colliders)
                {
                    c.isTrigger = false;
                    c.enabled = true;
                }
                foreach (var r in _renderers)
                {
                    r.enabled = true;
                    if (r.name.Equals("Image")) r.enabled = false;
                }
                foreach (var r in _srenderers) r.enabled = true;
                if (_constructionSite) foreach (var r in _constructionSite.GetComponentsInChildren<MeshRenderer>()) r.enabled = false;
                //GetComponentInChildren<MeshRenderer>().enabled = true;
            }

            if (Time.time > checkTime + checkRate)
            {
                checkTime = Time.time;

                CheckUpgrade();
                CheckDowngrade();
                CheckRepair();
            }

            if (Time.time > _damagedTime + 1.1f) _bDamaged = false;

            if (!BUpdatingInterface) return;

            SetInterfaceDetails();
        }

        protected void OnMouseDown()
        {
            if (BUpdatingInterface) return;

            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)) return;

            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.BuildingSelected);

            SetInterfaceDetails();
        }

        protected virtual void SetInterfaceDetails()
        {
            InterfaceManager.Instance.SetBuildingData(this, _buildingName, _iLevel, _buildingImage,
                GetUpgradeGoldCost(), GetUpgradeFoodCost(), GetUpgradeWoodCost(), GetUpgradeStoneCost(),
                GetRepairGoldCost(), GetRepairFoodCost(), GetRepairWoodCost(), GetRepairStoneCost(),
                iCurrentLife, iMaxLife, _bUpgradeable, _bRepairable, Upgrade, Repair);

            InterfaceManager.Instance.SetUpgradeBarFillAmout((float)_upgradeTicksCurrent / _upgradeTicks);
            InterfaceManager.Instance.SetRepairBarFillAmout((float)_repairTicksCurrent / _repairTicks);

            InterfaceManager.Instance.SetStatsText(type, iDmg, iStartingDmg * (_iLevel + 1), description);
        }

        public override void Damage(int dmg)
        {
            base.Damage(dmg);

            _bDamaged = true;
            _damagedTime = Time.time;

            StopCoroutine(UpgradeCoroutine());

            if (_bUpgrading) ResourceManager.Instance.AddNaturalResources((int)(GetUpgradeGoldCost() * .5f), (int)(GetUpgradeFoodCost() * .5f), (int)(GetUpgradeWoodCost() * .5f), (int)(GetUpgradeStoneCost() * .5f));

            _upgradeTicksCurrent = 0;
            _bUpgrading = false;

        }

        protected override void Die()
        {
            ResetBuilding();

            if (_playsound)
            {
                if (type == BuildingType.Wall) _playsound.PlaySound(SoundPool.ESounds.WallDestroyed);
                else _playsound.PlaySound(SoundPool.ESounds.BuildingDestroyed);
            }

            if (type == BuildingType.Main)
            {
                SceneManager.LoadScene("GameOver");
                Time.timeScale = 1;
            }
        }

        protected virtual void ResetBuilding()
        {
            this.SetLevel(0);
            StopAllCoroutines();

            _upgradeTicksCurrent = 0;
            _bUpgrading = false;
            _repairTicksCurrent = 0;
            _bRepairing = false;

            RequirementManager.Instance.SetLevel(type, 0);
            if (type == BuildingType.Tower) GetComponent<Unit>().SetLevel(0);
        }

        #region Upgrade
        protected void CheckUpgrade()
        {
            string error;

            //Primer check, compara el nivel del edificio principal
            if (BuildingType.Main != type) _bUpgradeable = _iLevel < RequirementManager.Instance.GetLevel(BuildingType.Main);

            if (!_bUpgradeable && type != BuildingType.Main)
            {
                error = Constants.MAINBUILD_ERROR;

                return;
            }

            //Segundo check, si el nivel del edificio es menor que el del edificio principal, comprobamos los recursos
            _bUpgradeable = ResourceManager.Instance.CheckNaturalCosts(GetUpgradeGoldCost(), GetUpgradeFoodCost(), GetUpgradeWoodCost(), GetUpgradeStoneCost(), out error) && !_bUpgrading && !_bRepairing && !_bDamaged;
            if (!_bUpgradeable) error = Constants.RESOURCE_ERROR;

        }

        protected void Upgrade()
        {
            if (!_bUpgradeable) return;

            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.Upgrade);

            ResourceManager.Instance.TakeNaturalResources(GetUpgradeGoldCost(), GetUpgradeFoodCost(), GetUpgradeWoodCost(), GetUpgradeStoneCost());

            StartCoroutine(UpgradeCoroutine());
            _bUpgradeable = false;
        }

        private IEnumerator UpgradeCoroutine()
        {
            if (_bDamaged) yield break;

            _upgradeTicksCurrent++;
            _bUpgrading = true;

            if (_upgradeTicksCurrent > _upgradeTicks)
            {
                ContinueUpgrade();
            }
            else
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(UpgradeCoroutine());
            }
        }

        protected virtual void ContinueUpgrade()
        {
            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.UpgradeFinished);

            _upgradeTicksCurrent = 0;
            _bUpgrading = false;
            AddLevel();
            if (type == BuildingType.Tower) GetComponent<Unit>().AddLevel();

            _upgradeTicks++;

            RequirementManager.Instance.SetLevel(type, _iLevel);
        }
        #endregion

        #region Repair
        protected void CheckRepair()
        {
            string error;
            _bRepairable = iCurrentLife < iMaxLife && ResourceManager.Instance.CheckNaturalCosts(GetRepairGoldCost(), GetRepairFoodCost(), GetRepairWoodCost(), GetRepairStoneCost(), out error) && !_bRepairing && !_bUpgrading && _iLevel > 0;
            if (!_bRepairable) error = Constants.RESOURCE_ERROR;
        }

        protected void Repair()
        {
            if (!_bRepairable) return;

            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.Repair);

            ResourceManager.Instance.TakeNaturalResources(GetRepairGoldCost(), GetRepairFoodCost(), GetRepairWoodCost(), GetRepairStoneCost());

            int lifeToRepair = iMaxLife - iCurrentLife;
            //int ticksNeeded = (iCurrentLife / iMaxLife) * 10; podria usarse como exploit.....

            StartCoroutine(RepairCoroutine(lifeToRepair));
            _bRepairable = false;
        }

        protected IEnumerator RepairCoroutine(int life)
        {
            _repairTicksCurrent++;
            _bRepairing = true;

            if (_repairTicksCurrent > _repairTicks)
            {
                PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.RepairFinished);

                _repairTicksCurrent = 0;
                _bRepairing = false;
                Heal(life);
            }
            else
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(RepairCoroutine(life));
            }
        }
        #endregion

        #region Downgrade
        protected void CheckDowngrade()
        {
            _bDowngradeable = _iLevel > 1;
        }

        protected void Downgrade()
        {
            if (!_bDowngradeable) return;

            int goldReturned = GetDowngradeGoldReturned();
            int foodReturned = GetDowngradeFoodReturned();
            int woodReturned = GetDowngradeWoodReturned();
            int stoneReturned = GetDowngradeStoneReturned();

            _iLevel--;

            //TODO: mostrar recursos devueltos

            ResourceManager.Instance.AddNaturalResources(goldReturned, foodReturned, woodReturned, stoneReturned);
        }
        #endregion

        #region Demolish

        protected void Demolish()
        {
            if (!_bDemolishable) return;

            int goldReturned = GetDemolishGoldReturned();
            int foodReturned = GetDemolishFoodReturned();
            int woodReturned = GetDemolishWoodReturned();
            int stoneReturned = GetDemolishStoneReturned();

            //TODO: mostrar recursos devueltos

            ResourceManager.Instance.AddNaturalResources(goldReturned, foodReturned, woodReturned, stoneReturned);
            Destroy(gameObject);
        }
        #endregion

        #region UpgradeCostCalculation

        protected int GetUpgradeCost(int resource)
        {
            return Mathf.RoundToInt(resource + (resource * _iLevel * Constants.STORAGE_COST_UPGRADE_MULTIPLIER));
        }

        protected int GetUpgradeGoldCost()
        {
            return GetUpgradeCost(_iGoldCost);
        }

        protected int GetUpgradeFoodCost()
        {
            return GetUpgradeCost(_iFoodCost);
        }

        protected int GetUpgradeWoodCost()
        {
            return GetUpgradeCost(_iWoodCost);
        }

        protected int GetUpgradeStoneCost()
        {
            return GetUpgradeCost(_iStoneCost);
        }

        #endregion

        #region DowngradeReturnCalculation

        protected int GetDowngradeReturn(int resource)
        {
            return Mathf.RoundToInt(resource * _iLevel * Constants.STORAGE_COST_DOWNGRADE_MULTIPLIER);
        }

        protected int GetDowngradeGoldReturned()
        {
            return GetDowngradeReturn(_iGoldCost);
        }

        protected int GetDowngradeFoodReturned()
        {
            return GetDowngradeReturn(_iFoodCost);

        }

        protected int GetDowngradeWoodReturned()
        {
            return GetDowngradeReturn(_iWoodCost);
        }

        protected int GetDowngradeStoneReturned()
        {
            return GetDowngradeReturn(_iStoneCost);
        }

        #endregion

        #region DemolishReturnCalculation

        protected int GetDemolishReturn(int resource)
        {
            int qty = 0;

            for (int i = _iLevel; i < Constants.BUILD_MIN_LEVEL; i--)
            {

                qty += Mathf.RoundToInt(resource * i * Constants.STORAGE_COST_DEMOLISH_MULTIPLIER);
            }

            return qty;
        }

        protected int GetDemolishGoldReturned()
        {
            return GetDemolishReturn(_iGoldCost);
        }

        protected int GetDemolishFoodReturned()
        {
            return GetDemolishReturn(_iFoodCost);
        }

        protected int GetDemolishWoodReturned()
        {
            return GetDemolishReturn(_iWoodCost);
        }

        protected int GetDemolishStoneReturned()
        {
            return GetDemolishReturn(_iStoneCost);
        }

        #endregion

        #region RepairCostCalculation

        protected float GetLifePercentage()
        {
            return (float)iCurrentLife / (float)iMaxLife;
        }

        protected int GetRepairCost(int resource, float percentage)
        {
            return Mathf.RoundToInt(resource * (_iLevel) * (1 - percentage) * 0.66f);
        }

        protected int GetRepairGoldCost()
        {
            return GetRepairCost(_iGoldCost, GetLifePercentage());
        }

        protected int GetRepairFoodCost()
        {
            return GetRepairCost(_iFoodCost, GetLifePercentage());
        }

        protected int GetRepairWoodCost()
        {
            return GetRepairCost(_iWoodCost, GetLifePercentage());
        }

        protected int GetRepairStoneCost()
        {
            return GetRepairCost(_iStoneCost, GetLifePercentage());
        }

        #endregion
    }
}
