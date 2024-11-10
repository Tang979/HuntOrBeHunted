using PolymindGames.ProceduralMotion;
using UnityEngine;
using System;

namespace PolymindGames
{
    public sealed class CharacterShakeHandler : CharacterBehaviour, IShakeHandler
    {
        [SerializeField, NotNull, BeginGroup("Motions")]
        private AdditiveShakeMotion _headShakeMotion;
        
        [SerializeField, NotNull, EndGroup]
        private AdditiveShakeMotion _handsShakeMotion;

        [SerializeField, Range(0f, 1000f), BeginGroup("Damage")]
        private float _minDamageThreshold = 10f;

        [SerializeField, Range(0f, 1000f)]
        private float _maxShakeDamage = 50f;

        [SerializeField, SpaceArea(3f), EndGroup]
        [ReorderableList(ListStyle.Lined), LabelFromChild(nameof(DamageShake.Type))]
        private DamageShake[] _damageShakes;


        public void AddShake(ShakeMotionData shake, float multiplier = 1f, BodyPoint point = BodyPoint.Head)
        {
            if (point == BodyPoint.Head)
            {
                _headShakeMotion.AddPositionShake(shake.PositionShake, multiplier);
                _headShakeMotion.AddRotationShake(shake.RotationShake, multiplier);
            }
            else if (point == BodyPoint.Hands)
            {
                _handsShakeMotion.AddPositionShake(shake.PositionShake, multiplier);
                _handsShakeMotion.AddRotationShake(shake.RotationShake, multiplier);
            }
        }

        public void AddShake(ShakeData shake, BodyPoint point = BodyPoint.Head)
        {
            if (point == BodyPoint.Head)
                _headShakeMotion.AddShake(shake);
            else if (point == BodyPoint.Hands)
                _handsShakeMotion.AddShake(shake);
        }

        protected override void OnBehaviourEnable(ICharacter character)
        {
            ShakeEvents.AddReceiver(this);
            character.HealthManager.DamageReceived += DamageReceived;
        }

        protected override void OnBehaviourDisable(ICharacter character)
        {
            ShakeEvents.RemoveReceiver(this);
            character.HealthManager.DamageReceived -= DamageReceived;
        }

        private void DamageReceived(float damage, in DamageArgs args)
        {
            if (damage > _minDamageThreshold)
            {
                float multiplier = (Mathf.Min(damage, _maxShakeDamage) - _minDamageThreshold) / (_maxShakeDamage - _minDamageThreshold);
                PlayDamageShake(args.DamageType, multiplier);
            }
        }

        private void PlayDamageShake(DamageType damageType, float multiplier)
        {
            for (int i = 0; i < _damageShakes.Length; i++)
            {
                if (_damageShakes[i].Type == damageType)
                {
                    AddShake(_damageShakes[i].Data.Shake, _damageShakes[i].Data.Multiplier * multiplier);
                    return;
                }
            }
        }

        #region Internal
        [Serializable]
        private struct DamageShake
        {
            public DamageType Type;
            public ShakeData Data;
        }
        #endregion
    }
}