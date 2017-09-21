using UnityEngine;

namespace Assets.GameAssets._Scripts
{
    public class Target : MonoBehaviour
    {
        /* Script básico para todos los edificios y unidades
         * Controla el nivel y en base a este el daño y la vida de los objetos
         * Heal()
         * Damage() - para dañar al objeto y matarlo si llega a 0
         * Die() - no se implementa, cada objeto lo hereda y lo trata de forma distinta
         * SetLevel() - se pone el objeto al nivel indicado, aumenta su vida y daño en base a ese nivel
         * AddLevel() - Incrementa un nivel
         */
        //Como subir el nivel de las tropas enemigas, para subir su vida y su daño, a lo mejor subir el nivel de cada unidad spawneada segun el nivel de ronda?
        protected int _iLevel;

        [SerializeField] protected int iStartingLife;
        protected int iMaxLife;
        protected int iCurrentLife;

        [SerializeField] protected int iStartingDmg;
        protected int iDmg;

        protected PlaySounds _playsound;

        //Espadachin 25 daño 40 vida
        //Arquero 15 daño 25 vida
        //Lancero 12 daño 60 vida

        private ParticleSystem _ps;

        protected virtual void Awake()
        {            
            iCurrentLife = iMaxLife = iStartingLife;
            iDmg = iStartingDmg;

            _ps = GetComponentInChildren<ParticleSystem>();

            _playsound = GetComponent<PlaySounds>();

        }

        public void Heal()
        {
            iCurrentLife = iMaxLife;
        }

        public void Heal(int life)
        {
            iCurrentLife += life;
            iCurrentLife = Mathf.Min(iMaxLife, iCurrentLife);
        }

        public virtual void Damage(int dmg)
        {
            if(_ps) _ps.Play();
            iCurrentLife -= dmg;
            if (iCurrentLife <= 0)
            {
                iCurrentLife = 0;
                Die();
            }
        }

        protected virtual void Die()
        {
        }

        public void SetLevel(int level)
        {
            _iLevel = level;

            if (level == 0) return;

            iMaxLife = iStartingLife + (int)(iStartingLife * (level-1) * 0.33);
            iCurrentLife = iMaxLife;
            iDmg = iStartingDmg * level;
        }

        public void AddLevel()
        {
            _iLevel++;
            SetLevel(_iLevel);
        }

        public int GetLevel()
        {
            return _iLevel;
        }
    }
}
