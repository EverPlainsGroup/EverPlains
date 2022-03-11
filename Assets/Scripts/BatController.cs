using System.Collections;
using UnityEngine;

/// <summary>
/// BatController is the class that managers the Bat enemy.
/// Like all enemies, it is a child class of EnemyController.
/// </summary>
public class BatController : EnemyController {

    /// <summary>
    /// Constructor for the BatController class.
    /// </summary>
    public BatController() : base() {
        attackRange = 2;
    }

    /// <summary>
    /// Wrapper function for attack movement.
    /// </summary>
    public override void OrchestrateAttack() {
        if (distance < attackRange) {
            StartCoroutine(AttackMovement());
        }
    }

    /// <summary>
    /// Logic for attack movement based on the Bat
    /// enemy's speed and positions.
    /// </summary>
    /// <returns>return IEnumerator</returns>
    public override IEnumerator AttackMovement() {
        attackPoint.SetActive(true);
        animator.SetTrigger("Attack");
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed + 0.5f) * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        attackPoint.SetActive(false);
        transform.position = Vector2.MoveTowards(transform.position, target.position, -1 * (speed + 1) * Time.deltaTime);
        yield return new WaitForSeconds(2);
    }

    /// <summary>
    /// Wrapper function for attack movement.
    /// </summary>
    void Attack() {
        StartCoroutine(AttackMovement());
    }

    /// <summary>
    /// Function that gets called when a Bat enemy dies.
    /// </summary>
    public override void Die() {
        rb.gravityScale = 1;
        base.Die();
    }

    /// <summary>
    /// Controls visuals for damage animation.
    /// </summary>
    /// <returns>return IEnumerator</returns>
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

    /// <summary>
    /// Function to calculate knockback velocity.
    /// </summary>
    public override void KnockBack() {
        if (target.transform.position.x < transform.position.x) {
            rb.velocity = new Vector2(3 * speed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(-3 * speed, rb.velocity.y);
        }
    }
}