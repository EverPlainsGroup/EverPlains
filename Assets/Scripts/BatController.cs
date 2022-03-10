using System.Collections;
using UnityEngine;

public class BatController : EnemyController {
    public BatController() : base() {
        attackRange = 2;
    }

    public override void OrchestrateAttack() {
        if (distance < attackRange) {
            StartCoroutine(AttackMovement());
        }
    }

    public override IEnumerator AttackMovement() {
        attackPoint.SetActive(true);
        animator.SetTrigger("Attack");
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed + 0.5f) * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        attackPoint.SetActive(false);
        transform.position = Vector2.MoveTowards(transform.position, target.position, -1 * (speed + 1) * Time.deltaTime);
        yield return new WaitForSeconds(2);
    }

    void Attack() {
        StartCoroutine(AttackMovement());
    }

    public override void Die() {
        rb.gravityScale = 1;
        base.Die();
    }

    public override IEnumerator DamageAnimation() {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);


        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.4f);

        if (IsHPHighEnough()) {
            sr.color = Color.white;
        }

        rb.velocity = new Vector2(0 * speed, rb.velocity.y);
    }
    public override void KnockBack() {
        // knockback
        if (target.transform.position.x < transform.position.x) {
            rb.velocity = new Vector2(3 * speed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(-3 * speed, rb.velocity.y);
        }
    }
}