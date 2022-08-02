using UnityEngine;

public class TileBase
{
    public float _timeToChangeDirection;
    private float _Xdirection;
    private float _Ydirection;
    private float _speed;
    private Rigidbody2D _rb;
    public TileBase(float timeToChangeDirection, float XDirection, float YDirection, float speed, Rigidbody2D rb)
    {
        _timeToChangeDirection = timeToChangeDirection;
        _Xdirection = XDirection;
        _Ydirection = YDirection;
        _speed = speed;
        _rb = rb;
        Move();
    }
    public void Move()
    {
        _rb.velocity = new Vector2(_Xdirection * _speed, _Ydirection * _speed);
    }
}
