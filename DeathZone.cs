using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    //[SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.GameOver();
    }
}
