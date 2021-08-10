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
    public int maxHP = 100;
    private int currentHP;
    private bool isColliding = false;

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
    public GameObject attackPoint;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
        extraJumpCount = extraJumpCountValue;
        currentHP = maxHP;
        attackPoint.SetActive(false);
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
            if (attackTimer > 0.429 && attackCount != 3)
            {
                Attack();
            }
            else if (attackTimer > 0.571 && attackCount == 3)
            {
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

        if (attackCount > 3)
        {
            attackCount = 1;
        }

        if (attackTimer > 1.0f)
        {
            attackCount = 1;
        }

        animator.SetTrigger("Attack" + attackCount);
        StartCoroutine(AttackCollision());

        attackTimer = 0.0f;
    }

    IEnumerator AttackCollision()
    {
        yield return new WaitForSeconds(.1f);
        attackPoint.SetActive(true);

        if (attackCount == 3)
        {
            attackPoint.GetComponent<CircleCollider2D>().offset = new Vector2(0.15f, 0);
            yield return new WaitForSeconds(.4f);
        }
        else
        {
            yield return new WaitForSeconds(.3f);
        }

        attackPoint.SetActive(false);
        attackPoint.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0);
    }

    void Block()
    {
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    IEnumerator DamageAnimation()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.4f);

        if (currentHP > 0)
        {
            sr.color = Color.white;
        }

        isColliding = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (isColliding) return;
            isColliding = true;
            TakeDamage(35, collision.gameObject);
        }
    }

    void TakeDamage(int damage, GameObject target)
    {
        currentHP -= damage;

        // knockback
        if (target.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(2 * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-2 * speed, rb.velocity.y);
        }

        StartCoroutine(DamageAnimation());
    }

    void OnGUI()
    {
        if (isFacingRight)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Facing Right");
        }
        else
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Facing Left");
        }

    }
}
