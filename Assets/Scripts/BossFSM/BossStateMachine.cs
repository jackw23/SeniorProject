using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{

    public Transform playerTransform;

    //List of states that will be used by the various bosses in the game
    [HideInInspector] public BossState idle, death, stageTransition, chase, meleeUp, meleeDown, meleeAttack, rangedAttack, fightStart, jump, jumpAway;
    
    [HideInInspector] public BossState nextState, currentState, previousState;
    [HideInInspector] public BoxCollider2D boxCollider2D;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public BossHealth bossHealth;
    [HideInInspector] public BossEnemy bossEnemy;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool endLoop;
    [HideInInspector] public Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();
        bossEnemy = GetComponent<BossEnemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        idle = (BossIdleState)ScriptableObject.CreateInstance(typeof(BossIdleState)); 
        death = (BossDeathState)ScriptableObject.CreateInstance(typeof(BossDeathState));
        chase = (BossChaseState)ScriptableObject.CreateInstance(typeof(BossChaseState));
        if (bossEnemy.bossOne) {
            meleeUp = (BossMeleeUpState)ScriptableObject.CreateInstance(typeof(BossMeleeUpState));
            meleeDown = (BossMeleeDownState)ScriptableObject.CreateInstance(typeof(BossMeleeDownState));
        } else if (bossEnemy.bossTwo) {
            meleeAttack = (BossMeleeAttackState)ScriptableObject.CreateInstance(typeof(BossMeleeAttackState));
        }
        rangedAttack = (BossRangedAttackState)ScriptableObject.CreateInstance(typeof(BossRangedAttackState));
        fightStart = (BossFightStartState)ScriptableObject.CreateInstance(typeof(BossFightStartState));
        jump = (BossJumpState)ScriptableObject.CreateInstance(typeof(BossJumpState));
        jumpAway = (BossJumpAwayState)ScriptableObject.CreateInstance(typeof(BossJumpAwayState));

        currentState = idle;
        originalPosition = transform.position;

        currentState.Enter(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.Execute(this);
    }

    public void TransitionState(BossState bossState) {
        previousState = currentState;
        currentState = bossState;
        currentState.Enter(this);
    }
}
