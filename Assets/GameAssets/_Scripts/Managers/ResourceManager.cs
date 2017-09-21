using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

namespace Assets.GameAssets._Scripts.Managers
{
    /*Gestor de recursos
     Controla la cantidad de recursos disponibles, tanto naturales como humanos
     Actualiza los textos de la interfaz para representar la cantidad actual de los recursos existentes
     Multiples metodos para añadir recursos individualmente o conjuntamente. Coger de los existentes o directamente quitar recursos sin comprobar si hay
     */
    public class ResourceManager : MonoBehaviour
    {

        public static ResourceManager Instance { get; set; }

        public enum EResourceType
        {
            Gold,
            Food,
            Wood,
            Stone,
            Bow,
            Sword,
            Spear,
            Population
        }

        //natural resources
        private int _iGold;
        private int _iFood;
        private int _iWood;
        private int _iStone;

        //human resources
        private int _iBow;
        private int _iSword;
        private int _iSpear;
        private int _iPopulation;

        private void Awake()
        {
            AddHumanResources(50);
            AddNaturalResources(1500);
            Instance = this;
        }

        private void Update()
        {
            UpdateResources();
        }

        private void UpdateResources()
        {
            InterfaceManager.Instance.SetResourcesData(_iGold, _iFood, _iWood, _iStone, _iBow, _iSword, _iSpear, _iPopulation);
        }

        public int GetResource(EResourceType type)
        {
            switch (type)
            {
                case EResourceType.Gold:
                    return _iGold;

                case EResourceType.Food:
                    return _iFood;

                case EResourceType.Wood:
                    return _iWood;
                
                case EResourceType.Stone:
                    return _iStone;

                case EResourceType.Bow:
                    return _iBow;

                case EResourceType.Sword:
                    return _iSword;

                case EResourceType.Spear:
                    return _iSpear;
                    
                case EResourceType.Population:
                    return _iPopulation;

                default:
                    return 0;
            }
        }

        private void SetResource(EResourceType type, int qty)
        {
            switch (type)
            {
                case EResourceType.Gold:
                    _iGold = qty;
                    break;

                case EResourceType.Food:
                    _iFood = qty;
                    break;

                case EResourceType.Wood:
                    _iWood = qty;
                    break;

                case EResourceType.Stone:
                    _iStone = qty;
                    break;

                case EResourceType.Bow:
                    _iBow = qty;
                    break;

                case EResourceType.Sword:
                    _iSword = qty;
                    break;

                case EResourceType.Spear:
                    _iSpear = qty;
                    break;

                case EResourceType.Population:
                    _iPopulation = qty;
                    break;

                default:
                    return;
            }
        }

        public bool CheckResource(EResourceType type, int qty)
        {
            int resource = GetResource(type);

            return qty <= resource;
        }

        public void TakeResource(EResourceType type, int qty)
        {
            if (!CheckResource(type, qty)) return;

            int resource = GetResource(type);
            resource -= qty;

            SetResource(type, resource);
        }

        public void AddResource(EResourceType type, int qty)
        {
            int resource = GetResource(type);

            resource += qty;
            resource = Mathf.Min(Constants.STORAGE_MAX, resource);

            SetResource(type, resource);
        }

        public bool CheckNaturalCosts(int gold, int food, int wood, int stone, out string errorMessage)
        {
            errorMessage = "";
            bool ok = true;

            if (gold > 0 && !CheckResource(EResourceType.Gold, gold))
            {
                errorMessage += "Lack " + (gold - GetResource(EResourceType.Gold)) + " gold/n";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            if (food > 0 && !CheckResource(EResourceType.Food, food))
            {
                errorMessage += "Lack " + (food - GetResource(EResourceType.Food)) + " food/n";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            if (wood > 0 && !CheckResource(EResourceType.Wood, wood))
            {
                errorMessage += "Lack " + (wood - GetResource(EResourceType.Wood)) + " wood/n";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            if (stone > 0 && !CheckResource(EResourceType.Stone, stone))
            {
                errorMessage += "Lack " + (stone - GetResource(EResourceType.Stone)) + " stone";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            return ok;
        }

        public bool CheckHumanCosts(int bow, int sword, int spear, int population, out string errorMessage)
        {
            errorMessage = "";
            bool ok = true;

            if (bow > 0 && !CheckResource(EResourceType.Gold, bow))
            {
                errorMessage += "Lack " + (bow - GetResource(EResourceType.Bow)) + " bow/n";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            if (sword > 0 && !CheckResource(EResourceType.Food, sword))
            {
                errorMessage += "Lack " + (sword - GetResource(EResourceType.Sword)) + " sword/n";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            if (spear > 0 && !CheckResource(EResourceType.Wood, spear))
            {
                errorMessage += "Lack " + (spear - GetResource(EResourceType.Spear)) + " spear/n";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            if (population > 0 && !CheckResource(EResourceType.Stone, population))
            {
                errorMessage += "Lack " + (population - GetResource(EResourceType.Population)) + " population";
                ok = false;
                //TODO: mensaje de falta recurso
            }

            return ok;
        }

        #region GeneralAdd
        public void AddNaturalResources(int qty)
        {
            _iGold += qty;
            _iFood += qty;
            _iWood += qty;
            _iStone += qty;

            _iGold = Mathf.Min(Constants.STORAGE_MAX, _iGold);
            _iFood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iWood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iStone = Mathf.Min(Constants.STORAGE_MAX, _iStone);
        }

        public void AddNaturalResources(int gold, int food, int wood, int stone)
        {
            _iGold += gold;
            _iFood += food;
            _iWood += wood;
            _iStone += stone;

            _iGold = Mathf.Min(Constants.STORAGE_MAX, _iGold);
            _iFood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iWood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iStone = Mathf.Min(Constants.STORAGE_MAX, _iStone);
        }

        public void AddHumanResources(int qty)
        {
            _iBow += qty;
            _iSword += qty;
            _iSpear += qty;
            _iPopulation += qty;

            _iBow = Mathf.Min(Constants.STORAGE_MAX, _iBow);
            _iSword = Mathf.Min(Constants.STORAGE_MAX, _iSword);
            _iSpear = Mathf.Min(Constants.STORAGE_MAX, _iSpear);
            _iPopulation = Mathf.Min(Constants.STORAGE_MAX, _iPopulation);
        }

        public void AddHumanResources(int bow, int sword, int spear, int population)
        {
            _iBow += bow;
            _iSword += sword;
            _iSpear += spear;
            _iPopulation += population;

            _iBow = Mathf.Min(Constants.STORAGE_MAX, _iBow);
            _iSword = Mathf.Min(Constants.STORAGE_MAX, _iSword);
            _iSpear = Mathf.Min(Constants.STORAGE_MAX, _iSpear);
            _iPopulation = Mathf.Min(Constants.STORAGE_MAX, _iPopulation);
        }
        #endregion

        #region GeneralTake
        public void TakeNaturalResources(int qty)
        {
            _iGold -= qty;
            _iFood -= qty;
            _iWood -= qty;
            _iStone -= qty;

            _iGold = Mathf.Max(0, _iGold);
            _iFood = Mathf.Max(0, _iFood);
            _iWood = Mathf.Max(0, _iFood);
            _iStone = Mathf.Max(0, _iStone);
        }

        public void TakeNaturalResources(int gold, int food, int wood, int stone)
        {
            _iGold -= gold;
            _iFood -= food;
            _iWood -= wood;
            _iStone -= stone;

            _iGold = Mathf.Max(0, _iGold);
            _iFood = Mathf.Max(0, _iFood);
            _iWood = Mathf.Max(0, _iFood);
            _iStone = Mathf.Max(0, _iStone);
        }

        public void TakeHumanResources(int qty)
        {
            _iBow -= qty;
            _iSword -= qty;
            _iSpear -= qty;
            _iPopulation -= qty;

            _iBow = Mathf.Max(0, _iBow);
            _iSword = Mathf.Max(0, _iSword);
            _iSpear = Mathf.Max(0, _iSpear);
            _iPopulation = Mathf.Max(0, _iPopulation);
        }

        public void TakeHumanResources(int bow, int sword, int spear, int population)
        {
            _iBow -= bow;
            _iSword -= sword;
            _iSpear -= spear;
            _iPopulation -= population;

            _iBow = Mathf.Max(0, _iBow);
            _iSword = Mathf.Max(0, _iSword);
            _iSpear = Mathf.Max(0, _iSpear);
            _iPopulation = Mathf.Max(0, _iPopulation);
        }
        #endregion

        /*
        #region GetResoureces

        public static int GetCurrentGold()
        {
            return _iGold;
        }

        public static int GetCurrentFood()
        {
            return _iFood;
        }

        public static int GetCurrentWood()
        {
            return _iWood;
        }

        public static int GetCurrentStone()
        {
            return _iStone;
        }

        public static int GetCurrentBows()
        {
            return _iBow;
        }

        public static int GetCurrentSwords()
        {
            return _iSword;
        }

        public static int GetCurrentSpears()
        {
            return _iSpear;
        }

        public static int GetCurrentPopulation()
        {
            return _iPopulation;
        }
        #endregion

        #region AddNaturalResources

        public static void AddNaturalResources(int qty)
        {
            _iGold += qty;
            _iFood += qty;
            _iWood += qty;
            _iStone += qty;

            _iGold = Mathf.Min(Constants.STORAGE_MAX, _iGold);
            _iFood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iWood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iStone = Mathf.Min(Constants.STORAGE_MAX, _iStone);
        }

        public static void AddNaturalResources(int gold, int food, int wood, int stone)
        {
            _iGold += gold;
            _iFood += food;
            _iWood += wood;
            _iStone += stone;

            _iGold = Mathf.Min(Constants.STORAGE_MAX, _iGold);
            _iFood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iWood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
            _iStone = Mathf.Min(Constants.STORAGE_MAX, _iStone);
        }

        public static void AddGold(int qty)
        {
            _iGold += qty;
            _iGold = Mathf.Min(Constants.STORAGE_MAX, _iGold);
        }

        public static void AddFood(int qty)
        {
            _iFood += qty;
            _iFood = Mathf.Min(Constants.STORAGE_MAX, _iFood);
        }

        public static void AddWood(int qty)
        {
            _iWood += qty;
            _iWood = Mathf.Min(Constants.STORAGE_MAX, _iWood);
        }

        public static void AddStone(int qty)
        {
            _iStone += qty;
            _iStone = Mathf.Min(Constants.STORAGE_MAX, _iStone);
        }

        #endregion

        #region TakeNaturalResources
        public static bool TakeGold(int qty)
        {
            if (qty > _iGold) return false;

            _iGold -= qty;
            return true;
        }

        public static bool TakeFood(int qty)
        {
            if (qty > _iFood) return false;

            _iFood -= qty;
            return true;
        }

        public static bool TakeWood(int qty)
        {
            if (qty > _iWood) return false;

            _iWood -= qty;
            return true;
        }

        public static bool TakeStone(int qty)
        {
            if (qty > _iStone) return false;

            _iStone -= qty;
            return true;
        }

        #endregion

        #region AddHumanResources

        public static void AddHumanResources(int qty)
        {
            _iBow += qty;
            _iSword += qty;
            _iSpear += qty;
            _iPopulation += qty;

            _iBow = Mathf.Min(Constants.STORAGE_MAX, _iBow);
            _iSword = Mathf.Min(Constants.STORAGE_MAX, _iSword);
            _iSpear = Mathf.Min(Constants.STORAGE_MAX, _iSpear);
            _iPopulation = Mathf.Min(Constants.STORAGE_MAX, _iPopulation);
        }

        public static void AddHumanResources(int bow, int sword, int spear, int population)
        {
            _iBow += bow;
            _iSword += sword;
            _iSpear += spear;
            _iPopulation += population;

            _iBow = Mathf.Min(Constants.STORAGE_MAX, _iBow);
            _iSword = Mathf.Min(Constants.STORAGE_MAX, _iSword);
            _iSpear = Mathf.Min(Constants.STORAGE_MAX, _iSpear);
            _iPopulation = Mathf.Min(Constants.STORAGE_MAX, _iPopulation);
        }

        public static void AddBow(int qty)
        {
            _iBow += qty;
            _iBow = Mathf.Min(Constants.STORAGE_MAX, _iBow);
        }

        public static void AddSword(int qty)
        {
            _iSword += qty;
            _iSword = Mathf.Min(Constants.STORAGE_MAX, _iSword);
        }

        public static void AddSpear(int qty)
        {
            _iSpear += qty;
            _iSpear = Mathf.Min(Constants.STORAGE_MAX, _iSpear);
        }

        public static void AddPopulation(int qty)
        {
            _iPopulation += qty;
            _iPopulation = Mathf.Min(Constants.STORAGE_MAX, _iPopulation);
        }

        #endregion

        #region TakeHumanResources

        public static bool TakeBow(int qty)
        {
            if (qty > _iBow) return false;

            _iBow -= qty;
            return true;
        }

        public static bool TakeSword(int qty)
        {
            if (qty > _iSword) return false;

            _iSword -= qty;
            return true;
        }

        public static bool TakeSpear(int qty)
        {
            if (qty > _iSpear) return false;

            _iSpear -= qty;
            return true;
        }

        public static bool TakePopulation(int qty)
        {
            if (qty > _iPopulation) return false;

            _iPopulation -= qty;
            return true;
        }

        #endregion

        #region RemoveResources

        public static void RemoveNaturalResources(int qty)
        {
            _iGold -= qty;
            _iFood -= qty;
            _iWood -= qty;
            _iStone -= qty;

            _iGold = Mathf.Max(Constants.STORAGE_MIN, _iGold);
            _iFood = Mathf.Max(Constants.STORAGE_MIN, _iFood);
            _iWood = Mathf.Max(Constants.STORAGE_MIN, _iFood);
            _iStone = Mathf.Max(Constants.STORAGE_MIN, _iStone);
        }

        public static void RemoveNaturalResources(int gold, int food, int wood, int stone)
        {
            _iGold -= gold;
            _iFood -= food;
            _iWood -= wood;
            _iStone -= stone;

            _iGold = Mathf.Max(Constants.STORAGE_MIN, _iGold);
            _iFood = Mathf.Max(Constants.STORAGE_MIN, _iFood);
            _iWood = Mathf.Max(Constants.STORAGE_MIN, _iFood);
            _iStone = Mathf.Max(Constants.STORAGE_MIN, _iStone);
        }

        public static void RemoveHumanResources(int qty)
        {
            _iBow -= qty;
            _iSword -= qty;
            _iSpear -= qty;
            _iPopulation -= qty;

            _iBow = Mathf.Max(Constants.STORAGE_MIN, _iBow);
            _iSword = Mathf.Max(Constants.STORAGE_MIN, _iSword);
            _iSpear = Mathf.Max(Constants.STORAGE_MIN, _iSpear);
            _iPopulation = Mathf.Max(Constants.STORAGE_MIN, _iPopulation);
        }

        public static void RemoveHumanResources(int bow, int sword, int spear, int population)
        {
            _iBow -= bow;
            _iSword -= sword;
            _iSpear -= spear;
            _iPopulation -= population;

            _iBow = Mathf.Max(Constants.STORAGE_MIN, _iBow);
            _iSword = Mathf.Max(Constants.STORAGE_MIN, _iSword);
            _iSpear = Mathf.Max(Constants.STORAGE_MIN, _iSpear);
            _iPopulation = Mathf.Max(Constants.STORAGE_MIN, _iPopulation);
        }

        #endregion
        */
    }
}
