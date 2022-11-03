using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool meleeAttack, mobile, verticalMovement, aggressive, constantAim, constantMelee;
    public float meleeRange = 0.5f;
    public float rangedRange = 10.0f;
    public float meleeDamage = 20.0f;
    public float rangedDamage = 15.0f;
    public float contactDamage = 5.0f;
    public float meleeAttackRate = 0.5f;
    public float rangedAttackRate = 0.3f;
    public float idleTime = 5.0f;
    public float distanceMovedRight = 5.0f;
    public float distanceMovedLeft = 5.0f;
    public float distanceMovedUp = 5.0f;
    public float distanceMovedDown = 5.0f;
    public float bulletTravelTime = 5.0f;
    StateMachine stateMachine;
    public Transform attackPoint, firePoint;
    public LayerMask playerLayer;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    public void MeleeAttack(Collider2D collider) {
        //Animator;
        if (collider != null) {
            Debug.Log("The AI unit hit " + collider.name);
        } else {
            Debug.Log("Constant Attack");
        }
        
    }

    public void RangedAttack() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.bulletDamage = rangedDamage;
        bulletScript.bulletLifeSpan = bulletTravelTime;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Player") {
            Debug.Log("Player lost " + contactDamage + " health!");
        }
    }

    void OnDrawGizmos() {
        if (attackPoint != null) {
            Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
        }
        if (firePoint != null) {
            Gizmos.DrawWireSphere(firePoint.position, meleeRange);
        }
        
    }

}
