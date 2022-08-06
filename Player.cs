using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _boxCollider;
    private InputController _inputController;

    // ===== MOVEMENT =====
    private float _horizontalMove;
    private float _verticalMove;
    [SerializeField] private float _characterMoveSpeed = 8f;
    [SerializeField] private float _characterJumpForce = 8f;

    private readonly float _extraHeightOffset = 0.05f;
    private readonly float _extraWidthOffset = 0.05f;

    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _wallOnRightSide = false;
    [SerializeField] private bool _wallOnLeftSide = false;

    // ===== CHARACTER STATES =====
    [SerializeField] private bool _characterIsDead = false;
    public bool CharacterIsDead
    {
        get { return _characterIsDead; }
        set { _characterIsDead = value; }
    }

    // ===== ANIMATOR =====
    public Animator animator;

    private void Awake()
    {
        _platformLayerMask = LayerMask.GetMask(CONSTSTRINGS.LayerMaskPlatforms);
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _inputController = FindObjectOfType<InputController>();
    }
    private void Start()
    {
        _characterMoveSpeed = GameManager.Instance.CharacterMoveSpeed;
        _characterJumpForce = GameManager.Instance.CharacterJumpForce;
    }
    private void Update()
    {
        HandleMovement();
        animator.SetFloat(CONSTSTRINGS.HorizontalMove, Mathf.Abs(_horizontalMove));
    }
    private void FixedUpdate()
    {
        CheckGround();
        CheckWallsRightSide();
        CheckWallsLeftSide();
    }
    private void CheckGround()
    {
        RaycastHit2D raycastHitBottom = Physics2D.BoxCast
            (_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, _extraHeightOffset, _platformLayerMask);
        Color rayColor;
        if (raycastHitBottom.collider != null) 
        { 
            rayColor = Color.green; 
        }
        else rayColor = Color.red;

        Debug.DrawRay(_boxCollider.bounds.center + 
            new Vector3(_boxCollider.bounds.extents.x, 0),
            Vector2.down * (_boxCollider.bounds.extents.y + _extraHeightOffset), rayColor);
        
        Debug.DrawRay(_boxCollider.bounds.center -
            new Vector3(_boxCollider.bounds.extents.x, 0),
            Vector2.down * (_boxCollider.bounds.extents.y + _extraHeightOffset), rayColor);
        
        Debug.DrawRay(_boxCollider.bounds.center -
            new Vector3(_boxCollider.bounds.extents.x, _boxCollider.bounds.extents.y + _extraHeightOffset),
            Vector2.right * (_boxCollider.bounds.extents.x*2), rayColor);

        _isGrounded = (raycastHitBottom.collider != null);
    }
    private void CheckWallsRightSide()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast
            (_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.right, _extraWidthOffset, _platformLayerMask);
        _wallOnRightSide = (raycastHitRight.collider != null);
    }
    private void CheckWallsLeftSide()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast
            (_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.left, _extraWidthOffset, _platformLayerMask);
        _wallOnLeftSide = (raycastHitLeft.collider != null);
    }
    private void HandleMovement()
    {
        if (_isGrounded)
        {
            _horizontalMove = _inputController.MoveInput() * _characterMoveSpeed;
            _verticalMove = _inputController.JumpInput() * _characterJumpForce;
        }
        else
        {
            _verticalMove = _rb.velocity.y;
            if (_wallOnRightSide)
            {
                _horizontalMove = Mathf.Clamp(_inputController.MoveInput() * _characterMoveSpeed, -8f, 0f);
            }
            else if (_wallOnLeftSide)
            {
                _horizontalMove = Mathf.Clamp(_inputController.MoveInput() * _characterMoveSpeed, 0f, 8f);
            }
            else _horizontalMove = _inputController.MoveInput() * _characterMoveSpeed;
        }
        // Final calculations
        _rb.velocity = new Vector2(_horizontalMove, _verticalMove);
    }
}
