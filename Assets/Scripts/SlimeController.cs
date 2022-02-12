using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : EnemyController {
  public SlimeController() : base() {
      attackRange = 0.5f;
  } 
  // Start is called before the first frame update
//   void Start() {
//     target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//     animator = GetComponent<Animator>();
//     rb = GetComponent<Rigidbody2D>();
//     sr = GetComponent<SpriteRenderer>();
//     pc = GetComponent<PolygonCollider2D>();
//     currentHP = maxHP;
//     attackPoint.SetActive(false);
//   }

//   public override IEnumerator AttackMovement() {
//     animator.SetTrigger("Attack");
//     yield return new WaitForSeconds(0.7f);
//     attackPoint.SetActive(true);
//     yield return new WaitForSeconds(0.3f);
//     attackPoint.SetActive(false);
//   }

//   void ChaseTarget() {
//     transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//   }

//   void Die() {
//     DropGold();
//     this.enabled = false;
//     pc.enabled = false;
//   }

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

//   IEnumerator DamageAnimation() {
//     var originalScale = transform.localScale;
//     transform.localScale = new Vector2(0.5f * originalScale.x, originalScale.y);
//     sr.color = Color.red;
//     yield return new WaitForSeconds(0.1f);
//     sr.color = Color.white;
//     yield return new WaitForSeconds(0.1f);
//     sr.color = Color.red;
//     yield return new WaitForSeconds(0.4f);

//     if (currentHP > 0) {
//       sr.color = Color.white;
//       transform.localScale = originalScale;
//     }
//   }

//   void OnTriggerEnter2D(Collider2D collision) {
//     if (collision.gameObject.name == "AttackPoint") {
//       TakeDamage(25);
//     }
//   }

//   void TakeDamage(int damage) {
//     currentHP -= damage;

//     // knockback
//     if (target.position.x < transform.position.x) {
//       rb.velocity = new Vector2(5 * speed, rb.velocity.y);
//     } else {
//       rb.velocity = new Vector2(-5 * speed, rb.velocity.y);
//     }

//     // flash red and squish
//     StartCoroutine(DamageAnimation());
//   }
}
