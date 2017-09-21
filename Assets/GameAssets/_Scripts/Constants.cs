using UnityEngine;

namespace Assets.GameAssets._Scripts
{
    public static class Constants
    {
        public static float _fxVolume;
        public static float _musicVolume;

        public static int BUILD_MAX_LEVEL = 10;
        public static int BUILD_MIN_LEVEL = 1;
        public static int BUILD_DESTROYED_LEVEL = 0;
        public static int BUILD_INITIAL_HEALTH = 100;

        public static int UNIT_MAX_LEVEL = 10;
        public static int UNIT_MIN_LEVEL = 1;

        public static int STORAGE_MAX = 99999;
        public static int STORAGE_MIN = 0;
        public static float STORAGE_COST_DOWNGRADE_MULTIPLIER = 0.8f;
        public static float STORAGE_COST_UPGRADE_MULTIPLIER = 0.5f;
        public static float STORAGE_COST_DEMOLISH_MULTIPLIER = 0.33f;

        public static string RESOURCE_ERROR = "You don't have enough resources";
        public static string MAINBUILD_ERROR = "You need to upgrade your Main Building first";

        public static float TimeStand;

    }
}
