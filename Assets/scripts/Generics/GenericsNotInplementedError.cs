using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����һ����̬�࣬���ڼ���ض������Ƿ�ʵ��
public static class GenericNotInplementedError<T>
{
    /// <summary>
    /// �÷�������Ƿ�ʵ��T���͵�һ��������������ǣ������¼һ������
    /// </summary>
    /// <param name="value">Ҫ�������</param>
    /// <param name="name">����ı���������</param>
    /// <returns>���������Ϊ�գ�Ĭ�������null</returns>
    public static T TryGet(T value, string name)
    {
        if (value != null)
        {
            return value;
        }

        //���������Ϣ
        Debug.LogError(typeof(T) + " not implemented on " + name);
        return default;
    }

}