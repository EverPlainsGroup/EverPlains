// Sources:
// https://assetstore.unity.com/packages/2d/characters/hero-knight-pixel-art-165188

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public PolygonCollider2D pc;
    public Animator animator;

    public float speed = 2;
    public float jumpForce = 4;
    private bool isFacingRight = true;
    public int maxHP = 100;
    private int currentHP;
    public int maxMana = 100;
    private int currentMana;
    private int currentGold;
    private bool isColliding = false;

    // For checking if player is grounded
    private bool grounded = false;
    public Transform feet;
    public float checkRadius;
    public LayerMask ground;

    // Jump count
    private int extraJumpCount;
    public int extraJumpCountValue = 1;

    // For attack loop
    private int attackCount = 0;
    public float attackTimer;
    public GameObject attackPoint;

    // For block
    public GameObject shield;
    private bool isBlocking = false;


    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
        extraJumpCount = extraJumpCountValue;
        currentHP = maxHP;
        currentMana = 0;
        attackPoint.SetActive(false);
        shield.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && !isBlocking)
        {
            if (isFacingRight) FlipPlayer();
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            animator.SetInteger("AnimState", 1);
        }

        else if (Input.GetKey(KeyCode.D) && !isBlocking)
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

        // For player falling animation
        animator.SetFloat("AirSpeedY", rb.velocity.y);

        // Check if player landed on the ground
        if (!grounded && IsGrounded())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
            extraJumpCount = extraJumpCountValue;
        }

        // Check if player is falling
        if (grounded && !IsGrounded())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isBlocking)
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
            isBlocking = true;
            shield.SetActive(true);
            Block();
        }

        // Release Block
        if (Input.GetKeyUp(KeyCode.K))
        {
            isBlocking = false;
            animator.SetBool("IdleBlock", false);
            shield.SetActive(false);
        }

        // Death
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
        GameObject.Destroy(this.gameObject);
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
        if (collision.gameObject.name == "EnemyAttackPoint")
        {
            if (isColliding) return;
            isColliding = true;
            TakeDamage(35, collision.gameObject);
        }

        else if (collision.transform.tag == "Mana")
        {
            Destroy(collision.gameObject);
            GainMana();
        }

        else if (collision.transform.tag == "Gold")
        {
            Destroy(collision.gameObject);
            GainGold();
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

    void GainMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += 25;
        }

        if (currentMana > maxMana)
        {
            currentMana = 100;
        }
    }

    void GainGold()
    {
        currentGold += 1;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "HP: " + currentHP);
        GUI.Label(new Rect(10, 40, 100, 20), "Gold: " + currentGold);
        GUI.Label(new Rect(10, 70, 100, 20), "Mana: " + currentMana);
    }
}
