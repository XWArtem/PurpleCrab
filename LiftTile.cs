using UnityEngine;
using System.Collections;

public class LiftTile : MonoBehaviour
{
    [SerializeField] private float Ydirection = 1.0f;
    [SerializeField] private float YdirectionSpeed = 0.4f;
    [SerializeField] private float timeToChangeDirection = 3.0f;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine("ChangeDirection", timeToChangeDirection);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = new Vector2(0f,Ydirection * YdirectionSpeed);
        //rb.AddForce(new Vector2(XdirectionSpeed* Xdirection, 0f), ForceMode2D.Force);
        //transform.position += transform.right * XdirectionSpeed * Time.deltaTime * Xdirection;
    }
    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToChangeDirection);
            Ydirection *= -1;
        }
    }
}
