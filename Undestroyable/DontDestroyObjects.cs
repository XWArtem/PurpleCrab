using UnityEngine;
public class DontDestroyObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] _undestroyableObjects;

    private void Awake()
    {
        foreach (GameObject gameObject in _undestroyableObjects)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}