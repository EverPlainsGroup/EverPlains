using System.Collections;
using UnityEngine;

public class MummyController : EnemyController
{
    public MummyController() : base() {
        attackRange = 2;
        range = 10;
        speed = 0.5f;
        maxHP = 80;
    }

    public override void OrchestrateAttack() {
        if (distance < attackRange && attackTimer > 1.25) {
            StartCoroutine(AttackMovement());
            attackTimer = 0.0f;
        }
    }

    public override IEnumerator AttackMovement() {
        float originalSpeed = speed;
        speed = 1.0f;
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        attackPoint.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        attackPoint.SetActive(false);
        speed = originalSpeed;
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
    }
}
