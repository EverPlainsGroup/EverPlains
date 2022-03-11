using System.Collections;
using UnityEngine;

/// <summary>
/// Class for Enemy Controller, which is the parent 
/// class for all enemies in the game.
/// </summary>
public class EnemyController : MonoBehaviour {
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public PolygonCollider2D pc;
    public Animator animator;
    public Transform target;
    public float range = 0;
    public float speed = 0;
    public int maxHP = 0;
    private int currentHP;
    private bool isFacingRight;
    public GameObject attackPoint;
    public float attackTimer;
    public Rigidbody2D goldCoin;
    public float distance;
    public float attackRange;

    /// <summary>
    /// Constructor for EnemyController class.
    /// </summary>
    public EnemyController() {
        range = 5;
        speed = 1;
        maxHP = 50;
        isFacingRight = false;
    }

    /// <summary>
    /// Getter and setter for facingRight.
    /// </summary>
    /// <returns>return a boolean</returns>
    public bool GetIsFacingRight() {
        return isFacingRight;
    }

    public void SetIsFacingRight(bool facingRight) {
        isFacingRight = facingRight;
    }

    /// <summary>
    /// Checks if HP is high enough.
    /// </summary>
    /// <returns>return a boolean</returns>
    public bool IsHPHighEnough() {
        if (currentHP > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start() {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
        currentHP = maxHP;
        attackPoint.SetActive(false);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        attackTimer += Time.deltaTime;
        distance = Vector2.Distance(target.position, transform.position);

        if (distance < range) {
            animator.SetBool("Walk", true);

            if (GetIsFacingRight() && target.transform.position.x < transform.position.x) {
                Flip();
            } else if (!GetIsFacingRight() && target.transform.position.x > transform.position.x) {
                Flip();
            }

            ChaseTarget();
        }

        animator.SetBool("Walk", false);

        OrchestrateAttack();

        if (currentHP <= 0) {
            Die();
        }
    }

    /// <summary>
    /// Wrapper for attack movement.
    /// </summary>
    public virtual void OrchestrateAttack() {
        if (distance < attackRange && attackTimer > 1.25) {
            StartCoroutine(AttackMovement());
            attackTimer = 0.0f;
        }
    }

    /// <summary>
    /// Manages the attack and movement timings.
    /// </summary>
    /// <returns>return an IEnumerator</returns>
    public virtual IEnumerator AttackMovement() {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        attackPoint.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        attackPoint.SetActive(false);
    }

    void ChaseTarget() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public virtual void Die() {
        DropGold();
        this.enabled = false;
        pc.enabled = false;
    }

    /// <summary>
    /// When the enemy dies, it drops gold, which can be picked up
    /// by the user.
    /// </summary>
    void DropGold() {
        int rand = Random.Range(1, 6);
        Vector2 randDirection;
        int randSpeed;

        for (int i = 0; i < rand; ++i) {
            randSpeed = Random.Range(100, 200);
            randDirection = Random.insideUnitCircle.normalized;
            Rigidbody2D coin = Instantiate(goldCoin, transform.position, transform.rotation);
            coin.AddRelativeForce(randDirection * 200);
        }
    }

    public virtual IEnumerator DamageAnimation() {
        var originalScale = transform.localScale;
        transform.localScale = new Vector2(0.5f * originalScale.x, originalScale.y);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.4f);

        if (currentHP > 0) {
            sr.color = Color.white;
            transform.localScale = originalScale;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "AttackPoint") {
            TakeDamage(25);
        }
    }

    void TakeDamage(int damage) {
        currentHP -= damage;
        KnockBack();
        // flash red and squish
        StartCoroutine(DamageAnimation());
    }

    public virtual void KnockBack() {
        // knockback
        if (target.position.x < transform.position.x) {
            rb.velocity = new Vector2(5 * speed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(-5 * speed, rb.velocity.y);
        }
    }

    public void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
