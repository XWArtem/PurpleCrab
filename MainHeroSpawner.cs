using UnityEngine;
using System;

public class MainHeroSpawner : MonoBehaviour
{
    public static Action FindTargetAction;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Player _player;
    private GameObject heroInstantiated;
    private Transform _startPositionOnScene;
    public Transform StartPositionOnScene
    {
        get
        {
            return _startPositionOnScene;
        }
        set
        {
            _startPositionOnScene = value;
        }
    }
    public void SpawnHero()
    {
        heroInstantiated = Instantiate(_playerPrefab, _startPositionOnScene.position, Quaternion.identity);
        heroInstantiated.name = CONSTSTRINGS.MainHero;
        FindTargetAction?.Invoke();
    }
    private void OnDisable()
    {
        if (heroInstantiated != null)
        {
            Destroy(heroInstantiated);
        }
    }
}
