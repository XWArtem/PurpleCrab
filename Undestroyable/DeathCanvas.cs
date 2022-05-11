using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    public static DeathCanvas instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
