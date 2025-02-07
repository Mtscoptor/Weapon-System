using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mtscoptor.Weapons.Components
{
    public class InputHoldData : ComponentData.ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(InputHold);
        }
    }
}
