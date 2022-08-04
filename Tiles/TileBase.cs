using UnityEngine;
using System.Collections;

public abstract class TileBase : MonoBehaviour
{
    [SerializeField] protected float TimeToChangeDirection;
    [SerializeField] protected float Xdirection;
    [SerializeField] protected float Ydirection;
    [SerializeField] protected float Speed;
    [SerializeField] protected Rigidbody2D Rb;

    public void Move()
    {
        Rb.velocity = new Vector2(Xdirection * Speed, Ydirection * Speed);
    }
    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToChangeDirection);
            Ydirection *= -1;
            Xdirection *= -1;
            Move();
        }
    }
}