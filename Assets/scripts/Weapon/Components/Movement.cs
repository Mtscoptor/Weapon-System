
using Mtscoptor.Weapons.Components.ComponentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{

    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {

        private CoreSystem.Movement coreMovement;

        private Player player {  get; set; }

        private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);

        private void HandleStartMovement()
        {

            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];

            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, player.facingDir);
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void Start()
        {
            base.Start();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
