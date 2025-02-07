
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    [Serializable]
    public class AttackPoiseDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}
