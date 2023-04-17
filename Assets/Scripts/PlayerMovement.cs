using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [Header("Move Metrics")]
    public float moveSpeed = 10f;
    public float acceleration = 1f;
    public float decceleration = 1f;
    public float velPower = 2f;
    public float jumpForce = 40f;
    public float frictionValue = 1f;
    public float horizontalInput;

    [Header("Dash Metrics")]
    public float dashSpeed = 75f;
    public float dashDuration = 0.2f;
    public float dashUpSpeed = 50f;
    public float dashUpDuration = 0.2f;
    public float diagonalDashSpeed = 1f;
    public KeyCode dashButton = KeyCode.F;

    [Header("Indicators")]
    public bool isGrounded = false;
    public bool canDash = false;
    public bool isDashing = false;
    public bool canJump = false;
    public bool canAirJump = false;
    public int dir = 1;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private bool jumpPressed = false;
    private bool dashPressed = false;
    private bool upPressed = false;

    [SerializeField] LayerMask GroundLayer;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        canJump = false;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && (isGrounded || canAirJump))
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(dashButton))
        {
            dashPressed = true;
        }
        if (dashPressed && !canDash)
        {
            dashPressed = false;
        }
        upPressed = Input.GetKey(KeyCode.UpArrow);
    }
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        //rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        float targetSpeed = horizontalInput * moveSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        if (isGrounded && horizontalInput == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionValue));

            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            jumpPressed = false;
        }
        if (jumpPressed && canAirJump && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canAirJump = false;
            jumpPressed = false;
        }


        // set current dir
        if (Input.GetKeyDown("left") || (Input.GetKey("left") && !Input.GetKey("right")))
        {
            dir = -1;
        }
        if (Input.GetKeyDown("right") || (Input.GetKey("right") && !Input.GetKey("left")))
        {
            dir = 1;
        }

        if (dashPressed && canDash && !upPressed)
        {
            StartCoroutine(Dash("default"));
            dashPressed = false;
        }

        if (dashPressed && upPressed || (Input.GetKeyDown(dashButton) && Input.GetKey(KeyCode.UpArrow)) && canDash)
        {
            StartCoroutine(Dash("up"));
            dashPressed = false;
        }
    }
    private void CheckIfGrounded()
    {
        RaycastHit2D castHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down,0.1f, GroundLayer) ;
        isGrounded = castHit.collider != null;
        if (castHit.collider != null)
        {
            canAirJump = true;
        }
    }

    private IEnumerator Dash(string direction)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (direction == "default")
        {
            rb.velocity = new Vector2(dir * dashSpeed, 0f);
            yield return new WaitForSeconds(dashDuration);
            rb.gravityScale = originalGravity;
            isDashing = false;
        }
        if (direction == "up")
        {
            rb.velocity = new Vector2(0f, dashUpSpeed);
            yield return new WaitForSeconds(dashUpDuration);
            rb.gravityScale = originalGravity;
            isDashing = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "ResetDash")
        {
            canDash = true;
        }
    }
}
