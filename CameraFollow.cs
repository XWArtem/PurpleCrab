using UnityEngine;
/// <summary>
/// camera that follows the main hero
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Header ("Target Transform")] [Tooltip ("Establish hero's Transform here")] 
    private Transform _target;

    [SerializeField] [Header("Camera Offsets")] [Tooltip("Establish XYZ Offsets for camera here in order to get its proper location")]
    private Vector3 _cameraOffset;

    private bool _cameraIsActive = true;
    public bool CameraIsActive 
    {
        private get
        {
            return _cameraIsActive;
        }
        set
        {
            _cameraIsActive = value;
        }
    }

    private void Awake()
    {
        _cameraOffset = new Vector3(0f, 0f, -10f);
        _target = GameObject.Find("MainHero").transform;
        GameManager.Instance.SetCamera(this);
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + _cameraOffset;
        if (_cameraIsActive) 
        { 
            transform.position = desiredPosition; 
        }
    }
}
