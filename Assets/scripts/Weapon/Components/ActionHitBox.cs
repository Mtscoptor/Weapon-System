using Mtscoptor.CoreSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{

    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {

        public event Action<Collider2D[]> OnDetectedCollider2D;
        
        private CoreSystem.Movement movement;
        private Player player { get; set; }
        
        private Vector2 offset;

        private Collider2D[] detected;

        private void HandleAttackAction()
        {
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.center.x * player.facingDir),
                transform.position.y + currentAttackData.HitBox.center.y
                );
            Debug.Log("Set facingDir = " + player.facingDir);

            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0) return;

            OnDetectedCollider2D?.Invoke(detected);
        }

        protected override void Start()
        {
            base.Start();
            player = GetComponentInParent<Player>();
            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            eventHandler.OnAttackAction += HandleAttackAction;
        }
        private void Update()
        {
            
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null) return;

            foreach (var item in data.AttackData)
            {
                if (!item.Debug) continue;

                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
    }
}
