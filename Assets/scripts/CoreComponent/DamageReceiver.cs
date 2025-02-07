using System.Collections;
using System.Collections.Generic;
using Mtscoptor.Interfaces;
using UnityEngine;

namespace Mtscoptor.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;

        private PlayerStats playerStats;

        private Stats stats;
        private ParticleManager particleManager;

        public void Damage(float amount)
        {
            Debug.Log(core.transform.parent.name + " Damaged!");
            stats.Health.Decrease(amount);
            particleManager.StartParticlesWithRandomRotation(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();
        }
    }
}