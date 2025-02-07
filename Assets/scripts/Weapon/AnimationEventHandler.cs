using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.Weapons
{

    public class AnimationEventHandler : MonoBehaviour
    {

        public event Action OnFinish;

        public event Action OnStartMovement;

        public event Action OnStopMovement;

        public event Action OnAttackAction;
        public event Action OnMinHoldPassed;

        public event Action<AttackPhases> OnEnterAttackPhases;

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();

        private void StartMovementTrigger() => OnStartMovement?.Invoke();

        private void StopMovementTrigger() => OnStopMovement?.Invoke();

        private void AttackActionTrigger() => OnAttackAction?.Invoke();

        private void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();

        private void EnterAttackPhases(AttackPhases phase) => OnEnterAttackPhases?.Invoke(phase);
    }
}