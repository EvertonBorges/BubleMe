﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log(typeof(T).ToString() + " is Null");
            }
            return _instance;
        }
    }

    protected void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    protected void Start() {
        StartInit();
    }

    protected virtual void Init() {}

    protected virtual void StartInit() {}
    
}
