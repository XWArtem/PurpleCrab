using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Header ("Target Transform")] [Tooltip ("Establish hero's Transform here")] 
    private Transform _target;

    [SerializeField] [Header("Camera Offsets")] [Tooltip("Establish XYZ Offsets for camera here in order to get its proper location")]
    private Vector3 _cameraOffset = new Vector3(0f, 0f, -10f);

    private bool _cameraIsActive = true;
    [SerializeField] private float _smoothSpeed = 0.2f;

    private void Start()
    {
        GameManager.Instance.SetCamera(this);
    }
    private void OnEnable()
    {
        MainHeroSpawner.FindTargetAction += FindTarget;
        StaticActions.SetCameraActiveAction += SetCameraActive;
    }
    private void OnDisable()
    {
        MainHeroSpawner.FindTargetAction -= FindTarget;
        StaticActions.SetCameraActiveAction -= SetCameraActive;
    }
    private void SetCameraActive(bool isActive)
    {
        _cameraIsActive = isActive;
    }
    void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            if (_cameraIsActive)
            {
                transform.position = smoothedPosition;
            }
        }
    }
    private void FindTarget()
    {
        _target = GameObject.Find(CONSTSTRINGS.MainHero).transform;
        transform.position = _target.position;
    }
}
