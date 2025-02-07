using Mtscoptor.Weapons.Components;
using System;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}