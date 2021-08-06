// Sources:
// https://assetstore.unity.com/packages/2d/characters/hero-knight-pixel-art-165188

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float jumpForce = 4;
    private bool isFacingRight = true;

    // Variables for checking if player is grounded
    private bool grounded = false;
    public Transform feet;
    public float checkRadius;
    public LayerMask ground;

    // Jump count
    private int extraJumpCount;
    public int extraJumpCountValue = 1;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public PolygonCollider2D pc;
    public Animator animator;

    // For attack loop
    private int attackCount = 0;
    public float attackTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
        extraJumpCount = extraJumpCountValue;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (isFacingRight) FlipPlayer();
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            animator.SetInteger("AnimState", 1);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (!isFacingRight) FlipPlayer();
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetInteger("AnimState", 1);
        }

        // Idle
        else
        {
            animator.SetInteger("AnimState", 0);
        }
    }

    void Update()
    {
        // For attack loop
        attackTimer += Time.deltaTime;

        // Set animator AirSpeedY equal to vertical velocity
        animator.SetFloat("AirSpeedY", rb.velocity.y);

        // Check if character just landed on the ground
        if (!grounded && IsGrounded())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
            extraJumpCount = extraJumpCountValue;
        }

        // Check if character just started falling
        if (grounded && !IsGrounded())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (attackTimer > 0.3f) {
                Attack();
            }
        }

        // Block
        if (Input.GetKeyDown(KeyCode.K))
        {
            Block();
        }

        // Release Block
        if (Input.GetKeyUp(KeyCode.K))
        {
            animator.SetBool("IdleBlock", false);
        }

    }

    // Uses a sensor to detect if the player's "feet"
    // are touching the ground
    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, checkRadius, ground);

        if (groundCheck)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    // Flips the Player object, including all children
    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Jump()
    {
        if (extraJumpCount > 0)
        {
            animator.SetTrigger("Jump");
            grounded = false;
            animator.SetBool("Grounded", IsGrounded());
            rb.velocity = Vector2.up * jumpForce;
            --extraJumpCount;
        }

        else if (extraJumpCount == 0 && IsGrounded() == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Attack()
    {
        ++attackCount;

        if (attackCount > 3) {
            attackCount = 1;
        }

        if (attackTimer > 1.0f)
        {
            attackCount = 1;
        }

        animator.SetTrigger("Attack" + attackCount);

        attackTimer = 0.0f;
    }

    void Block()
    {
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Attack: " + attackCount);
    }
}
