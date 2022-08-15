using UnityEngine;
public class KinematicTile : TileBase
{
    private void Start()
    {
        base.Rb = gameObject.GetComponent<Rigidbody2D>();
        base.Move();
        base.StartCoroutine(nameof(ChangeDirection), TimeToChangeDirection);
    }
}