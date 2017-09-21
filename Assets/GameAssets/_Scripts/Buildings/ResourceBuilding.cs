using Assets.GameAssets._Scripts.Managers;
using UnityEngine;

namespace Assets.GameAssets._Scripts.Buildings
{
    /*Comportamiento de los edificios que generen recursos
     Tipo de recurso generado, tanto natural como humano,
     ratio de produccion
     cantidad producida
     Producir()
     override Mejorar() - añade mejoras de produccion
     */
    public class ResourceBuilding : Building
    {

        public ResourceManager.EResourceType ResourceType;

        [Header("Production")]
        [SerializeField] private float _productionRate;
        private float _producedTime;
        [SerializeField] private int _initialQuantity;
        private int _producedQuantity;

        protected override void Awake()
        {
            base.Awake();

            _producedQuantity = _initialQuantity;
        }

        protected override void Update()
        {
            base.Update();

            if (Time.time >= _producedTime + _productionRate) Produce();
        }

        protected override void SetInterfaceDetails()
        {
            base.SetInterfaceDetails();

            InterfaceManager.Instance.SetStatsText(type, _producedQuantity, _initialQuantity * (_iLevel + 1), description);
        }

        private void Produce()
        {
            if (GetLevel() < 1) return;

            _producedTime = Time.time;

            ResourceManager.Instance.AddResource(ResourceType, _producedQuantity);
        }

        protected override void ContinueUpgrade()
        {
            base.ContinueUpgrade();

            _producedQuantity = _initialQuantity * _iLevel;
        }

        protected override void Die()
        {
            base.Die();
            _producedQuantity = 0;
        }

    }
}
