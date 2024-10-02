using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public abstract class Persistent_Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get; protected set;
        }

        protected virtual void Awake()
        {
            if ( Instance is { } && Instance != (this as T) )
            {
                Destroy(gameObject);
                return;
            }
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this as T;
        }
        public static void Release(T data)
        {
            if ( Instance == data )
            {
                Instance = null;
            }
        }
    }
}
