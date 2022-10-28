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
/// 
/// Recommended values to start out with:
/// 
/// Speed: 10
/// Jump Power: 5
/// Flight Power: 1
/// X and Y Wall Force: 3
/// Wall Jump Time: 0.05
/// Check Radiius: 3
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
    [SerializeField] private float checkRadius;
    [SerializeField] private int attacksRemaining = 99999;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float distToGround;
    private bool isTouchingFront;
    private bool wallJumping;

    public int attackSelected;
    public Transform frontCheck;
    //Animator anim;
    public float wallSlidingSpeed;
    public bool flightEnabled = true;
    public Animator animator;

    // Need float variable for animator movement use
    float horizontalMove = 0f;

    /// <summary>
    /// Init components
    /// </summary>
    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();

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
        horizontalMove = horizontalInput * speed;

        // Adds Animator Parameter that allows player to transition from idle state to run state, and vice versa
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (!wallJumping)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }


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

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, wallLayer);

        //wall jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isTouchingFront && horizontalInput != 0)
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
            else if (Input.GetKey(KeyCode.J) && canDoubleJump)
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

        // Attacking 
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsAttacking", true);
        }
    }

    /// <summary>
    /// helper function that disables wall jumping
    /// </summary>
    private void setWallJumpFalse()
    {
        wallJumping = false;
    }

    /// <summary>
    /// Character can attack when not facing an object directly in front of it.
    /// </summary>
    /// <returns></returns>
    public bool canAttack()
    {
        return !isTouchingFront && attacksRemaining > 0;
    }

    /// <summary>
    /// update the attack type
    /// 
    /// 0: fire
    /// 1: water
    /// 2: earth
    /// 3: air
    /// </summary>
    /// <param name="value"></param>
    public void updateAttackSelected(int value)
    {
        if (value > -1 && value < 4)
        {
            attackSelected = value;
        }
    }
}
