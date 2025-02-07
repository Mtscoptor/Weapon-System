using System.Collections;
using System.Collections.Generic;
using Mtscoptor.Weapons.Components.ComponentData;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    public class KnockBackData : ComponentData<AttackKnockBack>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(KnockBack);
        }
    }
}
