using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    public static PauseCanvas instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
