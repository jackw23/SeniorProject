using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{

    public bool bossOne, bossTwo, bossThree;
    public bool stageOne, stageTwo, stageThree;
    public bool inStageOne, inStageTwo, inStageThree;
    public bool mobile;
    [HideInInspector] public bool fightStarted = false;
    public float speed;
    public int damageResistance;
    public int meleeOneDamage, meleeTwoDamage;
    [HideInInspector] public float meleeAttackRange;
    public float attackOneRange, attackTwoRange, rangedAttackRange;
    public int rangedOneDamage, rangedTwoDamage;
    public float rangedAttackTravelTime, projectileOneSpeed, projectileTwoSpeed;
    public int contactDamage;
    public float idleTime, idleStartUpTime, startUpTime, idleMeleeTime, idleRangedTime;

    public LayerMask layerMask, groundLayer;
    public Transform meleePointOne, meleePointTwo, rangedPoint, groundPoint;
    public Vector2 groundPointSize, jumpForce;
    public GameObject projectileOne, projectileTwo;
    BossHealth bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        meleeAttackRange = meleePointOne.position.x - transform.position.x;
        inStageOne = true;
        inStageTwo = false;
        inStageThree = false;
    }

    public void MeleeAttack(bool up, bool down) {
        if (bossOne) {
            if (up) {
                StartCoroutine(MeleeUp());
            } else if (down) {
                StartCoroutine(MeleeDown());
            } else {
                Debug.Log("Attack doesn't exist");
            }
        } else if (bossTwo) {
                StartCoroutine(MeleeGolem());
        }

    }

    public void RangedAttack() {
        if (bossTwo && inStageOne) {
            StartCoroutine(RangedGolem());
        } else if (bossTwo && inStageTwo) {
            StartCoroutine(RangedGlowingGolem());
        }
        //TODO: Instantiate projectile and set variables
    }

    IEnumerator MeleeUp() {
        yield return new WaitForSeconds(0.25f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(meleePointOne.position, attackOneRange, layerMask);

        if (colliders.Length > 0) {
            if (colliders[0].GetComponent<PlayerMovement>() != null) {
                colliders[0].GetComponent<PlayerMovement>().takeDamage(meleeOneDamage);
                Debug.Log("Attack Up inflicted " + meleeOneDamage + " to " + colliders[0].name + "!");
            }
        } else {
            Debug.Log("Attack up missed");
        }
    }

    IEnumerator MeleeDown() {
        yield return new WaitForSeconds(0.4167f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(meleePointTwo.position, attackTwoRange, layerMask);

        if (colliders.Length > 0) {
            if (colliders[0].GetComponent<PlayerMovement>() != null) {
                colliders[0].GetComponent<PlayerMovement>().takeDamage(meleeOneDamage);
                Debug.Log("Attack Down inflicted " + meleeOneDamage + " to " + colliders[0].name + "!");
            }  
        } else {
            Debug.Log("Attack down missed");
        }
    }

    IEnumerator MeleeGolem() {
        yield return new WaitForSeconds(1.16667f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(meleePointOne.position, attackOneRange, layerMask);

        if (colliders.Length > 0) {
            if (colliders[0].GetComponent<PlayerMovement>() != null) {
                colliders[0].GetComponent<PlayerMovement>().takeDamage(meleeOneDamage);
                Debug.Log("Golem melee Attack inflicted " + meleeOneDamage + " to " + colliders[0].name + "!");
            }
        } else {
            Debug.Log("Golem melee missed");
        }
    }

    IEnumerator RangedGolem() {
        yield return new WaitForSeconds(0.984f);

        GameObject golemProjectile = Instantiate(projectileOne, rangedPoint.position, rangedPoint.rotation);

        Bullet projectileScript = golemProjectile.GetComponent<Bullet>();
        projectileScript.bulletDamage = rangedOneDamage;
        projectileScript.bulletLifeSpan = rangedAttackTravelTime;
        projectileScript.bulletSpeed = projectileOneSpeed;
    }

    IEnumerator RangedGlowingGolem() {
        yield return new WaitForSeconds(0.984f);

        GameObject golemProjectile = Instantiate(projectileTwo, rangedPoint.position, rangedPoint.rotation);

        Bullet projectileScript = golemProjectile.GetComponent<Bullet>();
        projectileScript.bulletDamage = rangedOneDamage;
        projectileScript.bulletLifeSpan = rangedAttackTravelTime;
        projectileScript.bulletSpeed = projectileTwoSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Player") {
            Debug.Log("Inflicted " + contactDamage + " damage to " + collision.collider.name + "!");
        }
    }

    void OnDrawGizmos() {
        if (meleePointOne != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(meleePointOne.position, attackOneRange);
        }
        if (meleePointTwo != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(meleePointTwo.position, attackTwoRange);
        }
        if (rangedPoint != null) {
            Gizmos.DrawWireSphere(rangedPoint.position, rangedAttackRange);
        }

        if (groundPoint != null) {
            Gizmos.DrawCube(groundPoint.position, groundPointSize);
        }
        
    }

}
