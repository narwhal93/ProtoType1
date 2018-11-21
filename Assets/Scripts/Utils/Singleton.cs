using UnityEngine;
using System.Collections;

public class Singleton<T> where T : class, new()
{
    static private T _instance = null;
    static public T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    protected Singleton()
    {
    }
}
