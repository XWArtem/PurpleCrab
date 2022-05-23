using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 cameraOffset;
    public bool cameraIsActive = true;

    private void Awake()
    {
        target = GameObject.Find("MainHero").transform;
        GameManager.instance.SetCamera(this);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        if (cameraIsActive) transform.position = desiredPosition;
    }
}
