using System.Linq;
using Assets.GameAssets._Scripts.Managers;
using UnityEngine;

namespace Assets.GameAssets._Scripts.Units
{
    public class EnemyUnit : Unit
    {
        /*
         * Comportamiento basico de la unidad enemiga
         * Cambia el comportamiento de FindTarget()
         */

        private GameObject _finalTarget;

        private void Start()
        {
            _finalTarget = GameManager.Instance.GetFinalTarget();
        }

        protected override void Update()
        {
            base.Update();

            _bHasTarget = true;
            MoveToTarget();
        }

        protected override void FindTarget()
        {
            base.FindTarget();

            //LayerMask targetLayerMask = (int)Mathf.Pow(2, LayerMask.NameToLayer("PlayerTarget"));
            LayerMask targetLayerMask = LayerMask.GetMask("PlayerTarget") | LayerMask.GetMask("PlayerWall");
            Collider[] _targets = Physics.OverlapSphere(transform.position, _fFindSphereRadius, targetLayerMask);
            
            if (_targets.Length == 0 || !_currentTarget)
            {
                _currentTarget = _finalTarget;
            }
            else
            {
                _targets = _targets.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
                foreach(var t in _targets)
                {
                    if (t.GetComponent<Target>().GetLevel() > 0)
                    {
                        _currentTarget = t.gameObject;
                        break;
                    }
                }
            }

            if (_currentTarget.GetComponent<Target>().GetLevel() < 1)
            {
                _currentTarget = null;
            }
        }
    }
}
