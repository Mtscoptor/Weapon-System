
using System;
using System.Collections;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }

        [field:SerializeField] public float AttackPowerCoefficient { get; private set; }

    }
}