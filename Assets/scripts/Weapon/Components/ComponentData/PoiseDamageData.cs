using System.Collections;
using System.Collections.Generic;
using Mtscoptor.Weapons.Components.ComponentData;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    public class PoiseDamageData : ComponentData<AttackPoiseDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(PoiseDamage);
        }
    }
}

