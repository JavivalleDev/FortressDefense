using System.Linq;
using Assets.GameAssets._Scripts.Managers;
using UnityEngine;


namespace Assets.GameAssets._Scripts.Units
{
    public class Unit : Target
    {
        /*
         * Comportamiento basico de las unidades. 
         * Attack() - Metodo de ataque de la unidad, en funcion del rango de esta y de la distancia con el objetivo
         * MoveToTarget() - La unidad se dirige al objetivo
         * ReturnToPosition() - La unidad vuelve a su posicion y rotacion original
         * FindTarget() - Cada tipo (aliada o enemiga) implementa su propio modo de buscar un target, pues
         * el comportamiento enemigo requiere tener siempre un target al que dirigirse mientras que el 
         * aliado permite no tener target y por tanto un comportamiento estacionario
         */


        private UnitAnimatorController _animator;
        public PlayerUnitManager.EUnitType unitType;

        [SerializeField] private GameObject _eyesPosition;
        [SerializeField] private GameObject _shootPosition;

        //Finding variables
        [SerializeField] protected float _fFindRate;
        protected float _fFindTime;
        [SerializeField] protected float _fFindSphereRadius = 5;

        //Attack variables
        protected GameObject _currentTarget;
        protected bool _bHasTarget;

        [SerializeField] private float speed;
        [SerializeField] protected float _iRange = 1.5f;

        [SerializeField] protected float _fAttackRate;
        protected float _fAttackTime;
        protected bool _bAttacking;

        //Initial position variables
        protected Vector3 _v3InitialPosition;
        protected Quaternion _qInitialRotation;

        //Component variables
        protected Rigidbody _rbRigidbody;
        protected Collider _collider;

        [SerializeField] private bool _bIsEnemy;

        private System.Action _onDeath;
        private bool _bIsDeath;

        public bool _bUnitWall;


        public void Initialize(System.Action die)
        {
            _onDeath = die;
        }
        
        protected override void Awake()
        {
            base.Awake();

            if(unitType != PlayerUnitManager.EUnitType.Tower) SetLevel(1);

            _animator = GetComponent<UnitAnimatorController>();

            _v3InitialPosition = transform.position;
            _qInitialRotation = transform.rotation;

            _rbRigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            //_rbRigidbody.isKinematic = true;
        }
        
        protected virtual void Update()
        {
            if ((_v3InitialPosition.y >= -0.1f && _v3InitialPosition.y <= 0.1f) && !_bIsDeath)
            {
                transform.position = new Vector3(transform.position.x, _v3InitialPosition.y, transform.position.z);
            }

            if(_currentTarget) if(Vector3.Distance(_currentTarget.transform.position, transform.position) >= _fFindSphereRadius && !_bIsEnemy) _currentTarget = null;

            if (GetLevel() < 1) return;

            if (!_currentTarget || !_currentTarget.gameObject.activeSelf) _bHasTarget = false;

            if ((Vector3.Distance(transform.position, _v3InitialPosition) < .3f || !_currentTarget) && _animator) _animator.Idle();

            if (Time.time >= _fFindRate + _fFindTime) FindTarget();
            if (Time.time >= _fAttackRate + _fAttackTime) Attack();

        }

        public void SetInitialPosition(Vector3 position, Quaternion rotation)
        {
            _v3InitialPosition = position;
            _qInitialRotation = rotation;

            transform.position = position;
            transform.rotation = rotation;
        }

        protected void Attack()
        {
            if (!_bHasTarget || !_currentTarget) return;

            _bAttacking = false;

            float range = _bUnitWall ? _iRange * 2 : _iRange;
            MeshRenderer _renderer = _currentTarget.GetComponentInChildren<MeshRenderer>();

            if(_renderer) range = (_bUnitWall ? _iRange * 2 : _iRange) + _renderer.bounds.extents.magnitude;

            if (Vector3.Distance(_currentTarget.transform.position, transform.position) > range) return;

            _bAttacking = true;

            if (_animator)
            {
                _animator.Idle();
                _animator.Attack();
            }

            if (_playsound)
            {
                switch (unitType)
                {
                        case PlayerUnitManager.EUnitType.Archer: _playsound.PlayArrow();
                            break;

                        case PlayerUnitManager.EUnitType.Tower: _playsound.PlayArrow();
                            break;

                        case PlayerUnitManager.EUnitType.Swordsman: _playsound.PlaySword();
                            break;

                        case PlayerUnitManager.EUnitType.Spearman: _playsound.PlaySpear();
                            break;
                }
            }

            if (unitType == PlayerUnitManager.EUnitType.Archer || unitType == PlayerUnitManager.EUnitType.Tower)
            {
                GameObject arrow = UnitPoolManager.Instance.TakeArrow();
                arrow.SetActive(true);
                arrow.transform.position = _shootPosition.transform.position;
                arrow.transform.LookAt(_currentTarget.transform.position);
                arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * 50, ForceMode.Impulse);
                arrow.GetComponent<Arrow>().Spawned();
            }
            
            _fAttackTime = Time.time;
            _currentTarget.gameObject.GetComponent<Target>().Damage(iDmg);
        }

        protected void MoveToTarget()
        {
            if (!_bHasTarget || unitType == PlayerUnitManager.EUnitType.Tower || !_currentTarget || _bIsDeath || _bUnitWall) return;
            
            Vector3 direction = _currentTarget.transform.position - transform.position;
            direction = direction.normalized;

            direction.y = _eyesPosition.transform.localPosition.y;

            transform.forward = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime, 0.0F);


            if (/*(unitType == PlayerUnitManager.EUnitType.Archer && transform.CompareTag("PlayerTarget")) ||*/ _bAttacking) return;
            _rbRigidbody.MovePosition(transform.position + direction * Time.deltaTime * speed);
            if (_animator) _animator.Charge();
        }

        protected void ReturnToPosition()
        {
            if (_bHasTarget || unitType == PlayerUnitManager.EUnitType.Tower) return;

            if (Vector3.Distance(_v3InitialPosition, transform.position) < 0.2f)
            {
                if (transform.rotation == _qInitialRotation) return;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _qInitialRotation, 1);
                return;
            }

            Vector3 direction = _v3InitialPosition - transform.position;
            direction = direction.normalized;

            transform.forward = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime, 0.0F);

            _rbRigidbody.MovePosition(transform.position + direction * Time.deltaTime * speed/1.5f);
            if (_animator) _animator.Walk();
        }

        protected virtual void FindTarget()
        {
            _fFindTime = Time.time;
        }

        public override void Damage(int dmg)
        {
            base.Damage(dmg);
            if (_playsound && iCurrentLife > 0)
            {
                _playsound.PlayShout();
                _playsound.PlaySound(SoundPool.ESounds.Gore1);
            }
        }

        protected override void Die()
        {
            if (_bIsDeath) return;

            _bUnitWall = false;
            _bIsDeath = true;
            _currentTarget = null;

            if(_bIsEnemy) ResourceManager.Instance.AddNaturalResources(25);

            SetLevel(0);

            _rbRigidbody.isKinematic = true;
            _collider.enabled = false;

            if (_playsound)
            {
                _playsound.PlayDead();
                _playsound.PlaySound(SoundPool.ESounds.Gore1);
            }

            if(_animator) _animator.Die();
            Invoke("ContinueDeath", 4.5f);
        }

        protected void ContinueDeath()
        {
            //transform.position = Vector3.one * 500;
            if(_animator) _animator.IdleTrigger();

            _rbRigidbody.isKinematic = false;
            _collider.enabled = true;

            UnitPoolManager.Instance.ReturnUnit(this.gameObject, _bIsEnemy, unitType);

            _bIsDeath = false;

            _rbRigidbody.constraints |= RigidbodyConstraints.FreezePositionY;

            if (_onDeath != null) _onDeath();
        }
    }
}
