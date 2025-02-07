using UnityEngine;

namespace Mtscoptor.Interfaces
{

    public interface IKnockBackable
    {
        void KnockBack(Vector2 angle, float strength, int direction);
    }
}


