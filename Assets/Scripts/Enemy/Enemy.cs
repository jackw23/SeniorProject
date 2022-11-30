using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool meleeAttack, mobile, verticalMovement, aggressive, constantAim, constantMelee;
    public float aggroRange = 3.0f;
    public float meleeRange = 0.5f;
    public float rangedAttackRange = 10.0f;
    public float bulletTravelTime = 5.0f;
    public int meleeDamage = 20;
    public int rangedDamage = 15;
    public int rangedAttackSpeed = 5;
    public int contactDamage = 5;
    public float meleeAttackRate = 0.5f;
    public float rangedAttackRate = 0.3f;
    public float idleTime = 5.0f;
    public float distanceMovedRight = 5.0f;
    public float distanceMovedLeft = 5.0f;
    public float distanceMovedUp = 5.0f;
    public float distanceMovedDown = 5.0f;
    StateMachine stateMachine;
    public Transform attackPoint, firePoint;
    public LayerMask playerLayer;
    public GameObject bulletPrefab;
    Health health;
    Animator animator;
    private float animatorTimer;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in animationClips) {
            if (clip.name == "Attack") {
                animatorTimer = clip.length;
            }
        }
    }

    public void MeleeAttack(Collider2D collider) {
        animator.SetBool("attack", true);
        if (collider != null) {
            PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();

            if (playerMovement != null) {
                playerMovement.takeDamage(meleeDamage);
            }
            //Debug.Log("The AI unit hit " + collider.name + " for " + meleeDamage + " damage!");
        } else {
            Debug.Log("Constant Attack");
        }
        
    }

    public void RangedAttack() {
        StartCoroutine(Ranged());
    }

    IEnumerator Ranged() {
        //animator.SetBool("attack", true);

        yield return new WaitForSeconds(animatorTimer / 2);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.bulletDamage = rangedDamage;
        bulletScript.bulletLifeSpan = bulletTravelTime;
        bulletScript.bulletSpeed = rangedAttackSpeed;
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Red Witch") {
            Debug.Log("Player lost " + contactDamage + " health!");
            //health.TakeDamage(100);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Red Witch") {
            Debug.Log("Red witch took 15 damage");
            PlayerMovement player = collider.GetComponent<PlayerMovement>();
            player.takeDamage(contactDamage);
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
