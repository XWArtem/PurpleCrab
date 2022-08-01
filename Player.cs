using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _boxCollider;
    private InputController _inputController;

    // ===== MOVEMENT =====
    private float horizontalMove;
    private float verticalMove;
    [SerializeField] private float CharacterMoveSpeed = 8f;
    [SerializeField] private float CharacterJumpForce = 8f;
    private float extraHeightText = 0.05f;
    private float extraWidthText = 0.05f;
    [SerializeField] private LayerMask PlatformLayerMask;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool WallOnRightSide = false;
    [SerializeField] private bool WallOnLeftSide = false;

    // ===== CHARACTER STATES =====
    [SerializeField] private bool _CharacterIsDead = false;
    public bool CharacterIsDead
    {
        get { return _CharacterIsDead; }
        set { _CharacterIsDead = value; }
    }

    public Animator animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _inputController = FindObjectOfType<InputController>();
    }
    private void Start()
    {
        // get the value of move speed and jump forse 
        CharacterMoveSpeed = GameManager.Instance.CharacterMoveSpeed;
        CharacterJumpForce = GameManager.Instance.CharacterJumpForce;

        GameManager.Instance.SetPlayer(this);
    }
    private void Update()
    {
        HandleFinalMovement();
        animator.SetFloat(CONSTSTRINGS.HorizontalMove, Mathf.Abs(horizontalMove));
        
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
            (_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, extraHeightText, PlatformLayerMask);

        Color rayColor;
        if (raycastHitBottom.collider != null) 
            rayColor = Color.green;
            else rayColor = Color.red;
        Debug.DrawRay(_boxCollider.bounds.center + 
            new Vector3(_boxCollider.bounds.extents.x, 0),
            Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);
        
        Debug.DrawRay(_boxCollider.bounds.center -
            new Vector3(_boxCollider.bounds.extents.x, 0),
            Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);
        
        Debug.DrawRay(_boxCollider.bounds.center -
            new Vector3(_boxCollider.bounds.extents.x, _boxCollider.bounds.extents.y + extraHeightText),
            Vector2.right * (_boxCollider.bounds.extents.x*2), rayColor);

        isGrounded = (raycastHitBottom.collider != null) ? true : false;
    }

    private void CheckWallsRightSide()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast
            (_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.right, extraWidthText, PlatformLayerMask);
        WallOnRightSide = (raycastHitRight.collider != null) ? true : false;
    }
    private void CheckWallsLeftSide()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast
            (_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.left, extraWidthText, PlatformLayerMask);
        WallOnLeftSide = (raycastHitLeft.collider != null) ? true : false;
    }

    private void HandleFinalMovement()
    {
        if (isGrounded)
        {
            horizontalMove = _inputController.MoveInput() * CharacterMoveSpeed;
            verticalMove = _inputController.JumpInput() * CharacterJumpForce;
        }
        else
        {
            verticalMove = _rb.velocity.y;
            if (WallOnRightSide)
                horizontalMove = Mathf.Clamp(_inputController.MoveInput() * CharacterMoveSpeed, -8f, 0f);
            else if (WallOnLeftSide) 
                horizontalMove = Mathf.Clamp(_inputController.MoveInput() * CharacterMoveSpeed, 0f, 8f);
            else 
                horizontalMove = _inputController.MoveInput() * CharacterMoveSpeed;
        }
        // Final calculations
        _rb.velocity = new Vector2(horizontalMove, verticalMove);
    }
}
