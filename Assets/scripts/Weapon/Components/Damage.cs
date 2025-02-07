using System.Collections;
using System.Collections.Generic;
using Mtscoptor.Interfaces;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{

    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {

        private ActionHitBox hitBox;

        private PlayerStats playerStats { get; set; }

        private float decreaseDamage;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {

            foreach (var item in colliders)
            {

                if (item.TryGetComponent(out IDamageable damageable))
                {

                    damageable.Damage(currentAttackData.Amount+playerStats.attackPower* currentAttackData.AttackPowerCoefficient-decreaseDamage);

                }
            }
        }

        protected override void Start()
        {
            base.Start();
            playerStats = new PlayerStats();

            hitBox = GetComponent<ActionHitBox>();

            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}
