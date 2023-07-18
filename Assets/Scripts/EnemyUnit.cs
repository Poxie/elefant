using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] int currentHP = 1;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb; 
    [SerializeField] CapsuleCollider2D collider2D; 
    [SerializeField] MushroomEnemy script; 

    public void TakeDamage() {
        currentHP -= 1;

        if(currentHP == 0) {
            Death();
        }
    }
    
    public void Death() {
        animator.Play("Hit");
        collider2D.enabled = false;
        script.enabled = false;
        rb.velocity = Vector2.up * 6;
    }
    private void Update() {
        if(currentHP == 0) {
            transform.Rotate(Vector3.forward * Time.deltaTime * 20);        
        }
    }
}
