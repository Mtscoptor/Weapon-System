using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是一个静态类，用于检查特定类型是否实现
public static class GenericNotInplementedError<T>
{
    /// <summary>
    /// 该方法检查是否实现T类型的一个变量。如果不是，它会记录一个错误。
    /// </summary>
    /// <param name="value">要检查的组件</param>
    /// <param name="name">对象的变量的名称</param>
    /// <returns>变量如果不为空，默认如果是null</returns>
    public static T TryGet(T value, string name)
    {
        if (value != null)
        {
            return value;
        }

        //输出错误信息
        Debug.LogError(typeof(T) + " not implemented on " + name);
        return default;
    }

}