using UnityEngine;
using System.Collections;

public class KinematicTiles : MonoBehaviour
{
    [SerializeField] private float _Xdirection = 1.0f;
    [SerializeField] private float _speed = 0.4f;
    [SerializeField] private float _timeToChangeDirection = 3.0f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        TileBase _tileBase = new TileBase(_timeToChangeDirection, _Xdirection, 0f, _speed,_rb);
        StartCoroutine(nameof(ChangeDirection), _timeToChangeDirection);
    }
    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToChangeDirection);
            _Xdirection *= -1;
        }
    }
}
