using UnityEngine;

public class BackgroundTransition : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0f, 1f)] private float _parallaxForce;
    private Vector3 targetPosition;

    private void Start()
    {
        if (!_target)
        {
            throw new System.Exception($"Target is missing for {gameObject}");
        }
        targetPosition = _target.position; // delete this?
    }
    private void Update()
    {
        Vector3 deltaSpace = _target.position - targetPosition;
        targetPosition = _target.position;
        transform.position += new Vector3(deltaSpace.x * _parallaxForce, 0, 0);
    }
}
