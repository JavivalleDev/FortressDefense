using System;
using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts.Buildings;
using Assets.GameAssets._Scripts.Units;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.GameAssets._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; set; }

        [SerializeField] private GameObject FinalTarget;

        //Round Control
        private int _iRound;
        private string _timeLeft;

        [SerializeField] private float _fStartingRoundTime;
        private float _fTimeToNextRound;

        private bool _bCountdown;

        //SpawnControl
        [SerializeField] private List<GameObject> _spawnPoints;

        [SerializeField] private int _iUnitsPerSpawnPoint;
        private int _iUnitsSpawned;
        private bool _bRoundStarted;

        /*[Range(0, 1)] [SerializeField] */
        private float _archersProportion;
        /*[Range(0, 1)] [SerializeField] */private float _swordsmenProportion;
        /*[Range(0, 1)] [SerializeField] */private float _spearmenProportion;

        private AmbientMusic _ambient;

        private int _iUnitsDeath;

        void Awake()
        {
            _bCountdown = true;
            SetRoundTime();

            _ambient = GetComponentInChildren<AmbientMusic>();

            Instance = this;
        }

        void Update()
        {
            Constants.TimeStand = Time.time;
            CountDown();
            SetTimerText();
            EndRound();
            //CastBuildingOnInterface();

            InterfaceManager.Instance.SetRoundData(_iRound, _timeLeft);
        }

        //private void CastBuildingOnInterface()
        //{
        //    if (!Input.GetMouseButtonDown(0) || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)) return;

        //    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("PlayerTarget")))
        //    {
        //        Building building = hit.transform.GetComponent<Building>();
        //        if(building) building.SetInterfaceDetails();
        //    }
        //}

        public GameObject GetFinalTarget()
        {
            return FinalTarget;
        }

        #region TimeandRoundControl
        private void StartRound()
        {
            _bCountdown = false;
            _iRound++;

            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.RoundBegin);
            _ambient.SetPeaceMusic(false);


            SpawnStart();
        }

        private void SetRoundTime()
        {
            _fTimeToNextRound = _fStartingRoundTime + (1 * _iRound);
            _fTimeToNextRound = Mathf.Min(_fTimeToNextRound, 120);
            _iUnitsPerSpawnPoint += 1;
        }

        private void EndRound()
        {
            if (!(_iUnitsDeath >= _iUnitsSpawned) || !_bRoundStarted) return;

            PlaySounds2D.Instance.PlaySound(SoundPool.ESounds2D.RoundEnd);
            _ambient.SetPeaceMusic(true);

            _iUnitsSpawned = 0;
            _iUnitsDeath = 0;

            SetRoundTime();
            _bCountdown = true;
            _bRoundStarted = false;
        }

        private void CountDown()
        {
            if (!_bCountdown) return;

            _fTimeToNextRound -= Time.deltaTime;
            if (_fTimeToNextRound <= 0.1f)
            {
                StartRound();
            }
        }

        private void SetTimerText()
        {
            if (_bCountdown)
            {
                var minutes = Mathf.Floor(_fTimeToNextRound / 60);
                var seconds = Mathf.Floor((_fTimeToNextRound - (minutes * 60)) % 60);

                string minutesString = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
                string secondsString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

                _timeLeft = minutesString + ":" + secondsString;
            }
            else
            {
                _timeLeft = (_iUnitsSpawned - _iUnitsDeath).ToString();
            }


        }
        #endregion

        #region EnemyUnitsSpawn
        private void SpawnStart()
        {
            _bRoundStarted = true;

            while (_archersProportion + _swordsmenProportion + _spearmenProportion < 0.9 ||
                   _archersProportion + _swordsmenProportion + _spearmenProportion > 1.1)
            {
                _archersProportion = Random.Range(0, 1f);
                _swordsmenProportion = Random.Range(0, 1f);
                _spearmenProportion = Random.Range(0, 1f);
            }

            //SpawnUnits();
            StartCoroutine(SpawnUnitsCoroutine());
        }

        private IEnumerator SpawnUnitsCoroutine()
        {
            WaitForSeconds wfs = new WaitForSeconds(0.01f);
            foreach (var s in _spawnPoints)
            {
                for (int i = 0; i < _iUnitsPerSpawnPoint * _spearmenProportion; i++)
                {
                    EnemyUnit newUnit = UnitPoolManager.Instance.TakeUnit(true, PlayerUnitManager.EUnitType.Spearman).GetComponent<EnemyUnit>();
                    _iUnitsSpawned++;
                    SetActiveUnit(s, newUnit);
                    yield return wfs;
                }

                for (int i = 0; i < _iUnitsPerSpawnPoint * _swordsmenProportion; i++)
                {
                    EnemyUnit newUnit = UnitPoolManager.Instance.TakeUnit(true, PlayerUnitManager.EUnitType.Swordsman).GetComponent<EnemyUnit>();
                    _iUnitsSpawned++;
                    SetActiveUnit(s, newUnit);
                    yield return wfs;
                }

                for (int i = 0; i < _iUnitsPerSpawnPoint * _archersProportion; i++)
                {
                    EnemyUnit newUnit = UnitPoolManager.Instance.TakeUnit(true, PlayerUnitManager.EUnitType.Archer).GetComponent<EnemyUnit>();
                    _iUnitsSpawned++;
                    SetActiveUnit(s, newUnit);
                    yield return wfs;
                }
            }
        }

        //private void SpawnUnits()
        //{
        //    //_bRoundStarted = true;

        //    //while (_archersProportion + _swordsmenProportion + _spearmenProportion < 0.9 ||
        //    //       _archersProportion + _swordsmenProportion + _spearmenProportion > 1.1)
        //    //{
        //    //    _archersProportion = Random.Range(0, 1f);
        //    //    _swordsmenProportion = Random.Range(0, 1f);
        //    //    _spearmenProportion = Random.Range(0, 1f);
        //    //}


        //    foreach (var s in _spawnPoints)
        //    {
        //        for (int i = 0; i < _iUnitsPerSpawnPoint * _spearmenProportion; i++)
        //        {
        //            EnemyUnit newUnit = UnitPoolManager.Instance.TakeUnit(true, PlayerUnitManager.EUnitType.Spearman).GetComponent<EnemyUnit>();
        //            _iUnitsSpawned++;
        //            SetActiveUnit(s, newUnit);
        //        }

        //        for (int i = 0; i < _iUnitsPerSpawnPoint * _swordsmenProportion; i++)
        //        {
        //            EnemyUnit newUnit = UnitPoolManager.Instance.TakeUnit(true, PlayerUnitManager.EUnitType .Swordsman).GetComponent<EnemyUnit>();
        //            _iUnitsSpawned++;
        //            SetActiveUnit(s, newUnit);
        //        }

        //        for (int i = 0; i < _iUnitsPerSpawnPoint * _archersProportion; i++)
        //        {
        //            EnemyUnit newUnit = UnitPoolManager.Instance.TakeUnit(true, PlayerUnitManager.EUnitType.Archer).GetComponent<EnemyUnit>();
        //            _iUnitsSpawned++;
        //            SetActiveUnit(s, newUnit);
        //        }
        //    }
        //}

        private void SetActiveUnit(GameObject s, EnemyUnit newUnit)
        {
            newUnit.transform.position = s.transform.position + new Vector3(Random.insideUnitCircle.x * 5, 0, Random.insideUnitCircle.y * 5);
            newUnit.transform.rotation = s.transform.rotation;

            newUnit.Initialize(OnUnitDeath);
            newUnit.SetLevel(_iRound);

            newUnit.gameObject.SetActive(true);
        }

        private void OnUnitDeath()
        {
            _iUnitsDeath++;
        }
        #endregion
    }
}
