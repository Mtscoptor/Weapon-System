using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.Weapons.Components.ComponentData
{
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentNmae();
            SetComponentDependency();
        }

        public void SetComponentNmae() => name = GetType().Name;

        protected abstract void SetComponentDependency();

        public virtual void SetAttackDataNames() { }

        public virtual void InitializeAttackData(int numberOfAttacks) { }
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {

        [SerializeField] private T[] attackData;

        public T[] AttackData { get => attackData; private set => attackData = value; }

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for (int i = 0; i < AttackData.Length; i++)
            {
                AttackData[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            int oldLen = attackData != null ? attackData.Length : 0;

            if (oldLen == numberOfAttacks) return;

            Array.Resize(ref attackData, numberOfAttacks);

            if (oldLen < numberOfAttacks)
            {
                for (int i = oldLen; i < numberOfAttacks; i++)
                {
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObj;
                }
            }

            SetAttackDataNames();
        }
    }
}

