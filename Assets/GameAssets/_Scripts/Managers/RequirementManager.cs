using UnityEngine;
using Assets.GameAssets._Scripts.Buildings;

namespace Assets.GameAssets._Scripts.Managers
{
    public class RequirementManager : MonoBehaviour
    {

        public static RequirementManager Instance { get; set; }
        [HideInInspector] public int iMainLevel;
        [HideInInspector] public int iGoldMineLevel;
        [HideInInspector] public int iFarmLevel;
        [HideInInspector] public int iSawmillLevel;
        [HideInInspector] public int iQuarryLevel;
        [HideInInspector] public int iArcheryLevel;
        [HideInInspector] public int iBarracksLevel;
        [HideInInspector] public int iGuardLevel;
        [HideInInspector] public int iStorageLevel;
        [HideInInspector] public int iBlacksmithLevelLevel;

        private void Awake()
        {
            Instance = this;
        }

        public void SetLevel(Building.BuildingType type, int level)
        {
            switch (type)
            {
                case Building.BuildingType.Main:
                    iMainLevel = level;
                    break;

                case Building.BuildingType.GoldMine:
                    iGoldMineLevel = level;
                    break;

                case Building.BuildingType.Farm:
                    iFarmLevel = level;
                    break;

                case Building.BuildingType.Sawmill:
                    iSawmillLevel = level;
                    break;

                case Building.BuildingType.Quarry:
                    iQuarryLevel = level;
                    break;

                case Building.BuildingType.Archery:
                    iArcheryLevel = level;
                    break;

                case Building.BuildingType.Barracks:
                    iBarracksLevel = level;
                    break;

                case Building.BuildingType.Guard:
                    iGuardLevel = level;
                    break;

                case Building.BuildingType.Storage:
                    iStorageLevel = level;
                    break;

                case Building.BuildingType.Blacksmith:
                    iBlacksmithLevelLevel = level;
                    break;
            }
        }

        public int GetLevel(Building.BuildingType type)
        {
            int level = 0;
            switch (type)
            {
                case Building.BuildingType.Main:
                    level = iMainLevel;
                    break;

                case Building.BuildingType.GoldMine:
                    level = iGoldMineLevel;
                    break;

                case Building.BuildingType.Farm:
                    level = iFarmLevel;
                    break;

                case Building.BuildingType.Sawmill:
                    level = iSawmillLevel;
                    break;

                case Building.BuildingType.Quarry:
                    level = iQuarryLevel;
                    break;

                case Building.BuildingType.Archery:
                    level = iArcheryLevel;
                    break;

                case Building.BuildingType.Barracks:
                    level = iBarracksLevel;
                    break;

                case Building.BuildingType.Guard:
                    level = iGuardLevel;
                    break;

                case Building.BuildingType.Storage:
                    level = iStorageLevel;
                    break;

                case Building.BuildingType.Blacksmith:
                    level = iBlacksmithLevelLevel;
                    break;
            }
            return level;
        }

    }
}