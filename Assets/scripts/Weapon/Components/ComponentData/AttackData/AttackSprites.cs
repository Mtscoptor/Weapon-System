
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public PhaseSprites[] PhaseSprites { get; private set; }

        [field: SerializeField] public int ManaCost {  get; private set; }
    }

    [Serializable]
    public struct PhaseSprites
    {
        [field: SerializeField] public AttackPhases Phase { get; private set; }

        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}