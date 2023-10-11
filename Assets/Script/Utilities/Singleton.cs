using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null) Debug.LogError("Singleton<" + typeof(T) + "> is NULL.");
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null) {
            Debug.LogError("Singleton<" + typeof(T) + "> is already exist.");
            Destroy(gameObject);
        }else{
            instance = (T) this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
