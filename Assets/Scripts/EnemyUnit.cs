using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyUnit : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb; 
    
    [SerializeField] int currentHP = 1;

    public float speed;
    public float idleTimer;
    public bool isDead = false;

    private int rotateDir; 

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PlayerHitbox")) {
            if(Mathf.Abs(Vector3.Dot(Vector3.up, other.transform.position - transform.position)) < 0.4f) { 
                other.transform.parent.GetComponent<PlayerLife>().Die(); 
            }
        }
    }

    public void TakeDamage() {
        currentHP -= 1;

        if(currentHP == 0) {
            isDead = true;
            rotateDir = Random.Range(0,2)*2-1;
            Death();
        }
    }
    
    public void Death() {
        animator.Play("Hit");
        gameObject.layer = 15; // No Collision Layer
        rb.velocity = Vector2.up * 6;
    }

    private void Update() {
        if(isDead) {
            transform.Rotate(Vector3.forward * Time.deltaTime * 20 * rotateDir);        
        }
    }
}
