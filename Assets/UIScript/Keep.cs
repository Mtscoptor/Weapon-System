using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    // 用于确保此对象只会存在一个实例


    void Awake()
    {

        DontDestroyOnLoad(gameObject); // 防止此对象被销毁

    }
}
