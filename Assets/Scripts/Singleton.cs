using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : Singleton <T>{

	protected new virtual bool DontDestroyOnLoad => false;

    protected virtual bool CreateIfNull => true;
    private static bool _createIfNull = true;

    private static bool _alreadyTryFind = false;

    private static T _instance;
	public static T Instance {
		get {
			if (_instance == null) {

                if (!_alreadyTryFind)
                {
                    _instance = FindObjectOfType<T>();
                    _alreadyTryFind = true;
                }

                if(_instance == null && _createIfNull)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();

                    if (!_instance.CreateIfNull)
                    {
                        _createIfNull = false;
                        Destroy(_instance);
                    }
                }				
			}

			return _instance;
		}

		protected set {
			_instance = value;
		}
	}

	public static bool HasInstance{        
        get {
            if (_instance == null)
            {
                Debug.Log("Could not find an instace of " + typeof(T).Name);
            }
            return _instance != null;
        }
	}

    public static implicit operator bool (Singleton<T> self)
    {
        if (_instance == null)
        {
            Debug.Log("Could not find an instace of " + typeof(T).Name);
        }
        return _instance != null;
    }


    protected void Awake(){
		SetupInstance ();
	}

	protected virtual void SetupInstance(){
		Instance = this as T;
        if (DontDestroyOnLoad)
            DontDestroyOnLoad(transform);
    }
}
