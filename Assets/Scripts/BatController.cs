using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatController : EnemyController {
  public BatController() : base() {
      attackRange = 2;
  } 
  // Start is called before the first frame update
//   void Start() {
//     target = GameObject.FindWithTag("Player").transform;
//     animator = GetComponent<Animator>();
//     rb = GetComponent<Rigidbody2D>();
//     sr = GetComponent<SpriteRenderer>();
//     pc = GetComponent<PolygonCollider2D>();
//     currentHP = maxHP;
//     attackPoint.SetActive(false);
//   }

  // Update is called once per frame
//   void Update() {
//     attackTimer += Time.deltaTime;
//     distance = Vector2.Distance(target.position, transform.position);

//     if (distance < range) {
//       animator.SetBool("Walk", true);

//       if (GetIsFacingRight() && target.transform.position.x < transform.position.x) {
//         Flip();
//       } else if (!GetIsFacingRight() && target.transform.position.x > transform.position.x) {
//         Flip();
//       }

//       ChaseTarget();
//     }

//     animator.SetBool("Walk", false);

//     if (distance < attackRange) {
//       StartCoroutine(AttackMovement());
//     }

//     if (currentHP <= 0) {
//       Die();
//     }
//   }

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

//   void ChaseTarget() {
//     transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//   }

  public override void Die() {
    rb.gravityScale = 1;
    base.Die();
  }

//   void DropGold() {
//     int rand = Random.Range(1, 6);
//     Vector2 randDirection;
//     int randSpeed;

//     for (int i = 0; i < rand; ++i) {
//       randSpeed = Random.Range(100, 200);
//       randDirection = Random.insideUnitCircle.normalized;
//       Rigidbody2D coin = Instantiate(goldCoin, transform.position, transform.rotation);
//       coin.AddRelativeForce(randDirection * 200);
//     }
//   }

  public override IEnumerator DamageAnimation() {
    sr.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    sr.color = Color.white;
    yield return new WaitForSeconds(0.1f);
    sr.color = Color.red;
    yield return new WaitForSeconds(0.4f);

    if (!IsHPHighEnough()) {
      sr.color = Color.white;
    }

    rb.velocity = new Vector2(0 * speed, rb.velocity.y);
  }

//   void OnTriggerEnter2D(Collider2D collision) {
//     if (collision.gameObject.name == "AttackPoint") {
//       TakeDamage(25);
//     }
//   }

//   public override void TakeDamage(int damage) {
    // currentHP -= damage;

    public override void KnockBack() {
        // knockback
      if (target.transform.position.x < transform.position.x) {
        rb.velocity = new Vector2(3 * speed, rb.velocity.y);
      } else {
        rb.velocity = new Vector2(-3 * speed, rb.velocity.y);
      }    
    }

    // // flash red and squish
    // StartCoroutine(DamageAnimation());
}