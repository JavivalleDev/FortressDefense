using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameAssets._Scripts.Managers
{
    public class UnitPoolManager : MonoBehaviour
    {
        public static UnitPoolManager Instance { get; set; }

        [SerializeField] private GameObject _goPlayerArcherPrefab;
        [SerializeField] private GameObject _goPlayerSpearmanPrefab;
        [SerializeField] private GameObject _goPlayerSwordsmanPrefab;
        [SerializeField] private GameObject _goEnemyArcherPrefab;
        [SerializeField] private GameObject _goEnemySpearmanPrefab;
        [SerializeField] private GameObject _goEnemySwordsmanPrefab;

        [SerializeField] private GameObject _arrowPrefab;

        private static GameObject ThisGO;

        private List<GameObject> _goPlayerArchers;
        private List<GameObject> _goPlayerSpearmen;
        private List<GameObject> _goPlayerSwordsmen;

        private List<GameObject> _goEnemyArchers;
        private List<GameObject> _goEnemySpearmen;
        private List<GameObject> _goEnemySwordsmen;

        private List<GameObject> _arrows;

        private void Awake()
        {
            _goPlayerArchers = new List<GameObject>();
            _goPlayerSpearmen = new List<GameObject>();
            _goPlayerSwordsmen = new List<GameObject>();
            _goEnemyArchers = new List<GameObject>();
            _goEnemySpearmen = new List<GameObject>();
            _goEnemySwordsmen = new List<GameObject>();

            _arrows = new List<GameObject>();

            ThisGO = this.gameObject;

            SpawnUnits();

            Instance = this;
        }

        private void SpawnUnit(bool enemy, PlayerUnitManager.EUnitType type)
        {
            GameObject newUnit = null;

            if (enemy)
            {
                switch (type)
                {
                    case PlayerUnitManager.EUnitType.Archer:
                        newUnit = Instantiate(_goEnemyArcherPrefab);
                        _goEnemyArchers.Add(newUnit);
                        break;

                    case PlayerUnitManager.EUnitType.Spearman:
                        newUnit = Instantiate(_goEnemySpearmanPrefab);
                        _goEnemySpearmen.Add(newUnit);
                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:
                        newUnit = Instantiate(_goEnemySwordsmanPrefab);
                        _goEnemySwordsmen.Add(newUnit);
                        break;

                    default: return;
                }
            }
            else
            {
                switch (type)
                {
                    case PlayerUnitManager.EUnitType.Archer:
                        newUnit = Instantiate(_goPlayerArcherPrefab);
                        _goPlayerArchers.Add(newUnit);
                        break;

                    case PlayerUnitManager.EUnitType.Spearman:
                        newUnit = Instantiate(_goPlayerSpearmanPrefab);
                        _goPlayerSpearmen.Add(newUnit);
                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:
                        newUnit = Instantiate(_goPlayerSwordsmanPrefab);
                        _goPlayerSwordsmen.Add(newUnit);
                        break;

                    default: return;
                }
            }

            newUnit.transform.position = Vector3.zero;
            newUnit.transform.rotation = Quaternion.identity;

            newUnit.transform.SetParent(ThisGO.transform);

            newUnit.SetActive(false);
        }

        public GameObject TakeUnit(bool enemy, PlayerUnitManager.EUnitType type)
        {

            GameObject returnedObject = null;

            if (enemy)
            {
                switch (type)
                {
                    case PlayerUnitManager.EUnitType.Archer:

                        if (_goEnemyArchers.Count == 0)
                        {
                            SpawnUnit(true, PlayerUnitManager.EUnitType.Archer);
                        }

                        returnedObject = _goEnemyArchers[0];
                        _goEnemyArchers.Remove(returnedObject);

                        break;

                    case PlayerUnitManager.EUnitType.Spearman:

                        if (_goEnemySpearmen.Count == 0)
                        {
                            SpawnUnit(true, PlayerUnitManager.EUnitType.Spearman);
                        }

                        returnedObject = _goEnemySpearmen[0];
                        _goEnemySpearmen.Remove(returnedObject);

                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:

                        if (_goEnemySwordsmen.Count == 0)
                        {
                            SpawnUnit(true, PlayerUnitManager.EUnitType.Swordsman);
                        }

                        returnedObject = _goEnemySwordsmen[0];
                        _goEnemySwordsmen.Remove(returnedObject);

                        break;

                    default: return null;
                }
            }
            else
            {
                switch (type)
                {
                    case PlayerUnitManager.EUnitType.Archer:

                        if (_goPlayerArchers.Count == 0)
                        {
                            SpawnUnit(false, PlayerUnitManager.EUnitType.Archer);
                        }

                        returnedObject = _goPlayerArchers[0];
                        _goPlayerArchers.Remove(returnedObject);

                        break;

                    case PlayerUnitManager.EUnitType.Spearman:

                        if (_goPlayerSpearmen.Count == 0)
                        {
                            SpawnUnit(false, PlayerUnitManager.EUnitType.Spearman);
                        }

                        returnedObject = _goPlayerSpearmen[0];
                        _goPlayerSpearmen.Remove(returnedObject);

                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:

                        if (_goPlayerSwordsmen.Count == 0)
                        {
                            SpawnUnit(false, PlayerUnitManager.EUnitType.Swordsman);
                        }

                        returnedObject = _goPlayerSwordsmen[0];
                        _goPlayerSwordsmen.Remove(returnedObject);

                        break;

                    default: return null;
                }
            }


            return returnedObject;
        }

        public void ReturnUnit(GameObject returned, bool enemy, PlayerUnitManager.EUnitType type)
        {
            returned.SetActive(false);

            if (enemy)
            {
                switch (type)
                {
                    case PlayerUnitManager.EUnitType.Archer:
                        _goEnemyArchers.Add(returned);
                        break;

                    case PlayerUnitManager.EUnitType.Spearman:
                        _goEnemySpearmen.Add(returned);
                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:
                        _goEnemySwordsmen.Add(returned);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case PlayerUnitManager.EUnitType.Archer:
                        _goPlayerArchers.Add(returned);
                        break;

                    case PlayerUnitManager.EUnitType.Spearman:
                        _goPlayerSpearmen.Add(returned);
                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:
                        _goPlayerSwordsmen.Add(returned);
                        break;
                }
            }
        }

        #region Spawn
        private void SpawnPlayerUnits(int qty = 30)
        {
            for (int i = 0; i < qty; i++)
            {
                SpawnUnit(false, PlayerUnitManager.EUnitType.Archer);
                SpawnUnit(false, PlayerUnitManager.EUnitType.Spearman);
                SpawnUnit(false, PlayerUnitManager.EUnitType.Swordsman);
            }
        }

        private void SpawnEnemyUnits(int qty = 30)
        {
            for (int i = 0; i < qty; i++)
            {
                SpawnUnit(true, PlayerUnitManager.EUnitType.Archer);
                SpawnUnit(true, PlayerUnitManager.EUnitType.Spearman);
                SpawnUnit(true, PlayerUnitManager.EUnitType.Swordsman);
            }
        }

        private void SpawnUnits(int qty = 50)
        {
            SpawnPlayerUnits(qty);
            SpawnEnemyUnits(qty);
        }
        #endregion

        private void SpawnArrows(int qty = 40)
        {
            GameObject newUnit = Instantiate(_arrowPrefab);
            _arrows.Add(newUnit);

            newUnit.transform.position = Vector3.zero;
            newUnit.transform.rotation = Quaternion.identity;

            newUnit.transform.SetParent(ThisGO.transform);

            newUnit.SetActive(false);
        }

        public GameObject TakeArrow()
        {
            if(_arrows.Count <= 0) SpawnArrows();

            GameObject returnedObject = _arrows[0];
            _arrows.Remove(returnedObject);

            return returnedObject;
        }

        public void ReturnArrow(GameObject arrow)
        {
            arrow.SetActive(false);
            _arrows.Add(arrow);
        }



        ////private void SpawnPlayerArcher()
        ////{
        ////    GameObject newUnit = Instantiate(_goPlayerArcherPrefab);

        ////    newUnit.transform.position = Vector3.zero;
        ////    newUnit.transform.rotation = Quaternion.identity;

        ////    newUnit.transform.SetParent(ThisGO.transform);

        ////    newUnit.SetActive(false);

        ////    _goPlayerArchers.Add(newUnit);
        ////}

        ////private void SpawnPlayerSpearman()
        ////{
        ////    GameObject newUnit = Instantiate(_goPlayerSpearmanPrefab);

        ////    newUnit.transform.position = Vector3.zero;
        ////    newUnit.transform.rotation = Quaternion.identity;

        ////    newUnit.transform.SetParent(ThisGO.transform);

        ////    newUnit.SetActive(false);

        ////    _goPlayerSpearmen.Add(newUnit);
        ////}

        ////private void SpawnPlayerSwordsman()
        ////{
        ////    GameObject newUnit = Instantiate(_goPlayerSwordsmanPrefab);

        ////    newUnit.transform.position = Vector3.zero;
        ////    newUnit.transform.rotation = Quaternion.identity;

        ////    newUnit.transform.SetParent(ThisGO.transform);

        ////    newUnit.SetActive(false);

        ////    _goPlayerSwordsmen.Add(newUnit);
        ////}


        ////private void SpawnEnemyArcher()
        ////{
        ////    GameObject newUnit = Instantiate(_goEnemyArcherPrefab);

        ////    newUnit.transform.position = Vector3.zero;
        ////    newUnit.transform.rotation = Quaternion.identity;

        ////    newUnit.transform.SetParent(ThisGO.transform);

        ////    newUnit.SetActive(false);

        ////    _goEnemyArchers.Add(newUnit);
        ////}

        ////private void SpawnEnemySpearman()
        ////{
        ////    GameObject newUnit = Instantiate(_goEnemySpearmanPrefab);

        ////    newUnit.transform.position = Vector3.zero;
        ////    newUnit.transform.rotation = Quaternion.identity;

        ////    newUnit.transform.SetParent(ThisGO.transform);

        ////    newUnit.SetActive(false);

        ////    _goEnemySpearmen.Add(newUnit);
        ////}

        ////private void SpawnEnemySwordsman()
        ////{
        ////    GameObject newUnit = Instantiate(_goEnemySwordsmanPrefab);

        ////    newUnit.transform.position = Vector3.zero;
        ////    newUnit.transform.rotation = Quaternion.identity;

        ////    newUnit.transform.SetParent(ThisGO.transform);

        ////    newUnit.SetActive(false);

        ////    _goEnemySwordsmen.Add(newUnit);
        ////}

        //#region Take

        //#region TakePlayer

        //public GameObject TakePlayerArcher()
        //{
        //    if (_goPlayerArchers.Count == 0)
        //    {
        //        SpawnPlayerArcher();
        //    }

        //    var returnedObject = _goPlayerArchers[0];
        //    _goPlayerArchers.Remove(returnedObject);

        //    return returnedObject;
        //}

        //public GameObject TakePlayerSpearman()
        //{
        //    if (_goPlayerSpearmen.Count == 0)
        //    {
        //        SpawnPlayerSpearman();
        //    }

        //    var returnedObject = _goPlayerSpearmen[0];
        //    _goPlayerSpearmen.Remove(returnedObject);

        //    return returnedObject;
        //}

        //public GameObject TakePlayerSwordsman()
        //{
        //    if (_goPlayerSwordsmen.Count == 0)
        //    {
        //        SpawnPlayerSwordsman();
        //    }

        //    var returnedObject = _goPlayerSwordsmen[0];
        //    _goPlayerSwordsmen.Remove(returnedObject);

        //    return returnedObject;
        //}

        //#endregion

        //#region TakeEnemy

        //public GameObject TakeEnemyArcher()
        //{
        //    if (_goEnemyArchers.Count == 0)
        //    {
        //        SpawnEnemyArcher();
        //    }

        //    var returnedObject = _goEnemyArchers[0];
        //    _goEnemyArchers.Remove(returnedObject);

        //    return returnedObject;
        //}

        //public GameObject TakeEnemySpearman()
        //{
        //    if (_goEnemySpearmen.Count == 0)
        //    {
        //        SpawnEnemySpearman();
        //    }

        //    var returnedObject = _goEnemySpearmen[0];
        //    _goEnemySpearmen.Remove(returnedObject);

        //    return returnedObject;
        //}

        //public GameObject TakeEnemySwordsman()
        //{
        //    if (_goEnemySwordsmen.Count == 0)
        //    {
        //        SpawnEnemySwordsman();
        //    }

        //    var returnedObject = _goEnemySwordsmen[0];
        //    _goEnemySwordsmen.Remove(returnedObject);

        //    return returnedObject;
        //}

        //#endregion

        //#endregion
    }
}
