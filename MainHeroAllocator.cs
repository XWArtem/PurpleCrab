using UnityEngine;

public class MainHeroAllocator : MonoBehaviour
{
    private MainHeroSpawner _mainHeroSpawner;

    private void Awake()
    {
        _mainHeroSpawner = FindObjectOfType<MainHeroSpawner>();
        _mainHeroSpawner.StartPositionOnScene = gameObject.transform;
        _mainHeroSpawner.SpawnHero();
    }
}