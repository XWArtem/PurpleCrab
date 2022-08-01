using UnityEngine;
/// <summary>
/// camera following the main hero
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
    [Header ("Target Transform")] 
    [Tooltip ("Establish hero's Transform here")] 
    private Transform target;

    [SerializeField]
    [Header("Camera Offsets")]
    [Tooltip("Establish XYZ Offsets for camera here in order to get its proper location")]
    private Vector3 cameraOffset;

    public bool cameraIsActive = true;

    private void Awake()
    {
        target = GameObject.Find("MainHero").transform;
        GameManager.Instance.SetCamera(this);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        if (cameraIsActive) 
        { 
            transform.position = desiredPosition; 
        }
    }
}
