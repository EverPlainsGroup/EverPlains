using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < range)
        {
            animator.SetBool("Walk", true);

            if (isFacingRight && target.transform.position.x < transform.position.x)
            {
                FlipSlime();
            }
            else if (!isFacingRight && target.transform.position.x > transform.position.x)
            {
                FlipSlime();
            }

            ChaseTarget();
        }

        animator.SetBool("Walk", false);

        if (currentHP <= 0)
        {
            Die();
        }

    }

    void ChaseTarget()
    {

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void Die()
    {
        this.enabled = false;
        pc.enabled = false;
    }

    IEnumerator DamageAnimation()
    {
        transform.localScale = new Vector2(1, 2.5f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.4f);

        if (currentHP > 0)
        {
            sr.color = Color.white;
            transform.localScale = new Vector2(2.5f, 2.5f);
        }
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
            rb.velocity = new Vector2(5 * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-5 * speed, rb.velocity.y);
        }

        // flash red and squish
        StartCoroutine(DamageAnimation());
    }

    void FlipSlime()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
