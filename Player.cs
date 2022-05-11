using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D _boxCollider;
    public InputController _inputController;

    // ===== MOVEMENT =====
    private float horizontalMove;
    private float verticalMove;
    [SerializeField] private float CharacterMoveSpeed = 9f;
    [SerializeField] private float CharacterJumpForce = 9f;
    private float extraHeightText = 0.2f;
    private float extraWidthText = 0.1f;
    [SerializeField] private LayerMask PlatformLayerMask;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool WallOnRightSide = false;
    [SerializeField] private bool WallOnLeftSide = false;

    // ===== CHARACTER STATES =====
    [SerializeField] private bool m_CharacterIsDead = false;
    public bool CharacterIsDead
    {
        get { return m_CharacterIsDead; }
        set { m_CharacterIsDead = value;}
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _inputController = FindObjectOfType<InputController>();
    }
    private void Start()
    {
        GameManager.instance.SetPlayer(this);
    }
    private void Update()
    {
        HandleFinalMovement();
    }

    private void FixedUpdate()
    {
        // check if the Character is Grounded or not
        CheckGround();
        // check the walls on the right side of the Character
        CheckWallsRightSide();
        // check the walls on the left side of the Character
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
            new Vector3(_boxCollider.bounds.extents.x, 0), Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);
        
        Debug.DrawRay(_boxCollider.bounds.center -
            new Vector3(_boxCollider.bounds.extents.x, 0), Vector2.down * (_boxCollider.bounds.extents.y + extraHeightText), rayColor);
        
        Debug.DrawRay(_boxCollider.bounds.center -
            new Vector3(_boxCollider.bounds.extents.x, _boxCollider.bounds.extents.y + extraHeightText), Vector2.right * (_boxCollider.bounds.extents.x*2), rayColor);
        //Debug.Log(raycastHit.collider);
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
            verticalMove = rb.velocity.y;
            if (WallOnRightSide)
                horizontalMove = Mathf.Clamp(_inputController.MoveInput() * CharacterMoveSpeed, -1f, 0f);
            else if (WallOnLeftSide)
                horizontalMove = Mathf.Clamp(_inputController.MoveInput() * CharacterMoveSpeed, 0f, -1f);
            else 
                horizontalMove = _inputController.MoveInput() * CharacterMoveSpeed;
        }
        // Make the final calculations of Character movements
        rb.velocity = new Vector2(horizontalMove , verticalMove );
    }
}
