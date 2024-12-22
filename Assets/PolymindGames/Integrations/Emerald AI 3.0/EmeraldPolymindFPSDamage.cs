using EmeraldAI;
using UnityEngine;

namespace PolymindGames
{
#if EMERALD_AI_PRESENT
    [RequireComponent(typeof(EmeraldSystem))]
#endif
    public class EmeraldPolymindFPSDamage : MonoBehaviour, IDamageReceiver
    {
#if EMERALD_AI_PRESENT
        public EmeraldSystem m_EmeraldAI;

        [SerializeField, Range(0f, 1)]
        private float m_CriticalHitChance = 0.1f;

        [SerializeField, Range(0.01f, 10f)]
        private float m_ReceivedDamageMod = 0.5f;

        public ICharacter Character => throw new System.NotImplementedException();

        public DamageResult ReceiveDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public DamageResult ReceiveDamage(float damage, in DamageArgs args)
        {
            m_EmeraldAI.HealthComponent.Damage((int)damage, null, 100, false);
            return DamageResult.Normal;
        }

        private void Awake() => m_EmeraldAI = GetComponent<EmeraldSystem>();

        public void Start(){
            m_EmeraldAI = GetComponent<EmeraldSystem>();
        }
#endif
    }
}