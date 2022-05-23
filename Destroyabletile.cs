using UnityEngine;
using System.Collections;

public class Destroyabletile : MonoBehaviour
{
    [SerializeField] private float _timeToFall = 0.1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool alreadyFellDown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            if (rb != null && !alreadyFellDown)
                //StartCoroutine("Fall", _timeToFall);
                Invoke("Fall", _timeToFall);
    }

    private void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        alreadyFellDown = true;
    }

    //IEnumerator Fall()
    //{
    //    while (true)
    //    {
    //        rb.bodyType = RigidbodyType2D.Dynamic;        }
    //}
}
