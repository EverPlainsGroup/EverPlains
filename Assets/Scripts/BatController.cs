using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public PolygonCollider2D pc;
    public Animator animator;

    public Transform target;
    public float range = 5;
    public float speed = 1;
    public int maxHP = 50;
    private int currentHP;
    private bool isFacingRight = false;
    public GameObject attackPoint;
    public float attackRange = 3;
    public float attackTimer;
    public Rigidbody2D goldCoin;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
        currentHP = maxHP;
        attackPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < range)
        {
            animator.SetBool("Walk", true);

            if (isFacingRight && target.transform.position.x < transform.position.x)
            {
                FlipBat();
            }
            else if (!isFacingRight && target.transform.position.x > transform.position.x)
            {
                FlipBat();
            }

            ChaseTarget();
        }

        animator.SetBool("Walk", false);

        if (distance < attackRange)
        {
            StartCoroutine(AttackMovement());
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    IEnumerator AttackMovement()
    {
        attackPoint.SetActive(true);
        animator.SetTrigger("Attack");
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed + 1) * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        attackPoint.SetActive(false);
        transform.position = Vector2.MoveTowards(transform.position, target.position, -1 * (speed + 1) * Time.deltaTime);
        yield return new WaitForSeconds(2);
    }

    void Attack()
    {
        StartCoroutine(AttackMovement());
    }

    void ChaseTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void Die()
    {
        DropGold();
        rb.gravityScale = 1;
        this.enabled = false;
        pc.enabled = false;
    }

    void DropGold()
    {
        int rand = Random.Range(1, 6);
        Vector2 randDirection;
        int randSpeed;

        for (int i = 0; i < rand; ++i)
        {
            randSpeed = Random.Range(100, 200);
            randDirection = Random.insideUnitCircle.normalized;
            Rigidbody2D coin = Instantiate(goldCoin, transform.position, transform.rotation);
            coin.AddRelativeForce(randDirection * 200);
        }
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

        rb.velocity = new Vector2(0 * speed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackPoint")
        {
            TakeDamage(25);
        }
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;

        // knockback
        if (target.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(3 * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-3 * speed, rb.velocity.y);
        }

        // flash red and squish
        StartCoroutine(DamageAnimation());
    }

    void FlipBat()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
