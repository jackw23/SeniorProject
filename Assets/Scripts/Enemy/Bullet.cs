using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    Rigidbody2D rigidBody2D;
    PlayerMovement playerMovement;
    public int bulletDamage = 0;
    float time;
    public float bulletLifeSpan;


    // Start is called before the first frame update
    void Start()
    {   
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = transform.right * bulletSpeed;
        time = Time.time;
    }

    void Update() {
        if (Time.time - time > bulletLifeSpan) {
            Debug.Log("Bullet expired!");
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        playerMovement = collider.GetComponent<PlayerMovement>();

        if (playerMovement != null) {
            playerMovement.takeDamage(bulletDamage);
        }
        //Debug.Log("Dealt " + bulletDamage + " damage to " + collider.name);
        Destroy(gameObject);
    }
}
