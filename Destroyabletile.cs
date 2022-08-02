using UnityEngine;

public class Destroyabletile : MonoBehaviour
{
    [SerializeField] private float _timeToFall = 0.1f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _alreadyFellDown;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == CONSTSTRINGS.Player)
            if (_rb != null && !_alreadyFellDown)
                Invoke(nameof(Fall), _timeToFall);
    }
    private void Fall()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _alreadyFellDown = true;
    }
}
