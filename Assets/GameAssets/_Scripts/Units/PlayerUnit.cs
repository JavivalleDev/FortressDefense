using System.Linq;
using Assets.GameAssets._Scripts.Buildings;
using Assets.GameAssets._Scripts.Managers;
using UnityEngine;

namespace Assets.GameAssets._Scripts.Units
{
    public class PlayerUnit : Unit
    {
        /*
         * Comportamiento basico de la unidad aliada
         * Cambia el comportamiento de FindTarget()
         */
        private float _fReturnTime = 2;
        private float _fTimeWithoutTarget;

        private bool _WallSet;
        private Building _wall;

        protected override void Update()
        {
            base.Update();

            if (_bUnitWall)
            {
                if (!_WallSet)
                {
                    _wall = GetComponentInParent<Building>();
                    if(_wall) _WallSet = true;
                }
                else
                {
                    if (_wall.GetLevel() == 0) 
                    {
                        Die();
                        _rbRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                        _rbRigidbody.isKinematic = false;
                    }
                }
            }

            if (_currentTarget && _currentTarget.GetComponent<EnemyUnit>().GetLevel() < 1)
            {
                _currentTarget = null;
                _bHasTarget = false;
            }

            if (!_bHasTarget)
            {
                _fTimeWithoutTarget += Time.deltaTime;
                if (_fTimeWithoutTarget >= _fReturnTime)
                {
                    ReturnToPosition();
                }
            }
            else
            {
                MoveToTarget();
                _fTimeWithoutTarget = 0;
            }

        }

        protected override void FindTarget()
        {
            
            if (_bHasTarget) return;

            base.FindTarget();

            LayerMask targetLayerMask = (int)Mathf.Pow(2, LayerMask.NameToLayer("EnemyTarget"));
            Collider[] _targets = Physics.OverlapSphere(transform.position, _fFindSphereRadius, targetLayerMask);

            
            if (_targets.Length == 0)
            {
                _currentTarget = null;
                return;
            }
            
            _targets = _targets.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();

            foreach (var t in _targets)
            {
                if (t.GetComponent<Target>().GetLevel() > 0)
                {
                    _currentTarget = t.gameObject;
                    _bHasTarget = true;
                    break;
                }
            }
            
        }

        protected override void Die()
        {
            base.Die();
            switch (unitType)
            {
                    case PlayerUnitManager.EUnitType.Archer:
                        PlayerUnitManager.Instance._activeArchers.Remove(this);
                        break;

                    case PlayerUnitManager.EUnitType.Swordsman:
                        PlayerUnitManager.Instance._activeSwordsmen.Remove(this);
                        break;

                    case PlayerUnitManager.EUnitType.Spearman:
                        PlayerUnitManager.Instance._activeSpearmen.Remove(this);
                        break;
            }

            _wall = null;
            _WallSet = false;
        }
    }
}
