using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] public GameObject _player = null;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.25f;
    [SerializeField] private Vector3 cameraOffset;

    private void Awake()
    {
        target = GameObject.Find("MainHero").transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        //Vector2 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;

        //transform.LookAt(target); 
    }
}
