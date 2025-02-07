using System.Collections;
using System.Collections.Generic;
using Mtscoptor.CoreSystem;
using Mtscoptor.Weapons.Components.ComponentData;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{

    public abstract class WeaponComponent : MonoBehaviour
    {
        protected AnimationEventHandler eventHandler;

        protected bool isAttackActive;
        
        protected Weapon weapon;

        protected Core Core => weapon.Core;

        public virtual void Init()
        { }

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();

            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
            Debug.Log("Attack true");
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
            Debug.Log("Attack false");
        }

        protected virtual void OnDestroy()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }

        protected virtual void Start()
        {
            Debug.Log("Start");
            weapon.OnEnter += HandleEnter;

            weapon.OnExit += HandleExit;
        }
    }

    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T2 currentAttackData;

        protected T1 data;

        public override void Init()
        {
            base.Init();

            data = weapon.Data.GetData<T1>();
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentAttackData = data.AttackData[0];
        }
    }
}
