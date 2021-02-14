using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviorHelper : Singleton<MonoBehaviorHelper>
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public static Coroutine StartCoroutine(IEnumerator routine) => ((MonoBehaviour) Instance).StartCoroutine(routine);

    public static void StopCoroutine(Coroutine coroutine) => ((MonoBehaviour) Instance).StopCoroutine(coroutine);

}
