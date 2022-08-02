using UnityEngine;
using System.Collections;

public class LiftTile : MonoBehaviour
{
    [SerializeField] private float _Ydirection = 1.0f;
    [SerializeField] private float _speed = 0.4f;
    [SerializeField] private float _timeToChangeDirection = 3.0f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        TileBase _tileBase = new TileBase(_timeToChangeDirection, 0, _Ydirection, _speed, _rb);
        StartCoroutine(nameof(ChangeDirection), _timeToChangeDirection);
    }
    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToChangeDirection);
            _Ydirection *= -1;
        }
    }
}
