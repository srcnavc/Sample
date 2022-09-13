using UnityEngine;

public class CGSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    /// <summary>
    ///     Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance => _instance;

    /// <summary>
    ///     On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
    /// </summary>
    protected virtual void Awake()
    {
        if (!Application.isPlaying)
            return;
        _instance = this as T;
    }

    /// <summary>
    ///     On destroy, we release the instance;
    /// </summary>
    protected virtual void OnDestroy()
    {
    }
}