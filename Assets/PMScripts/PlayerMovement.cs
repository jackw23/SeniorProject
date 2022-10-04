using UnityEngine;

/// <summary>
/// Player movement class.
/// Followed the following tutorials found here: 
/// https://www.youtube.com/watch?v=TcranVQUQ5U
/// https://www.youtube.com/watch?v=KCzEnKLaaPc
/// 
/// To use this class, the character must have a RigidBody2D component, a BoxCollider2D component,
/// as well as a transform added to check for walls for wall jumps.
/// and other items the player should detect (like the ground) should have BoxCollider2D components.
/// 
/// Walls should have a "wall" layer added and ground should have a "ground" layer added to them.
/// 
/// The serialized parameters below can be tuned as needed for different characters.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    [SerializeField] public float flightPower;
    [SerializeField] public bool canDoubleJump = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] public float xWallForce;
    [SerializeField] public float yWallForce;
    [SerializeField] public float wallJumpTime;
    [SerializeField] private bool hitGround = false;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float distToGround;
    private bool isTouchingFront;
    private bool wallJumping;

    public Transform frontCheck;
    public float wallSlidingSpeed;
    public bool flightEnabled = true;

    /// <summary>
    /// Init components
    /// </summary>
    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        groundLayer = LayerMask.GetMask("ground");
        wallLayer = LayerMask.GetMask("wall");
    }

    /// <summary>
    /// this is running regularly and is the main function for player movement capabilities
    /// </summary>
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //check to enable double jumping
        if (Physics2D.Raycast(transform.position, Vector2.down, distToGround + .1f, groundLayer))
        {
            canDoubleJump = true;
        }

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 1.0f, wallLayer);

        //wall kumping
        if (Input.GetKey(KeyCode.UpArrow) && isTouchingFront)
        {
            wallJumping = true;
            Invoke("setWallJumpFalse", wallJumpTime);
        }

        if (wallJumping)
        {
            body.velocity = new Vector2(xWallForce * -horizontalInput, yWallForce);
        }

        //jumping and double jumping
        if (Input.GetKey(KeyCode.Space))
        {

            distToGround = boxCollider.bounds.extents.y;
            if (Physics2D.Raycast(transform.position, Vector2.down, distToGround + .1f, groundLayer))
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else if (canDoubleJump)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                canDoubleJump = false;
            } 
        }

        //flight
        if (Input.GetKey(KeyCode.F) && flightEnabled)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    /// <summary>
    /// helper function that disables wall jumping
    /// </summary>
    private void setWallJumpFalse()
    {
        wallJumping = false;
    }

}
