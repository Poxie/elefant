using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline: MonoBehaviour {
    [SerializeField] float trampolineForce = 30;

    Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if(
            animator.GetCurrentAnimatorStateInfo(0).IsName("TrampolineOn") && 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        ) {
            animator.Play("TrampolineIdle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, trampolineForce);
            animator.Play("TrampolineOn");
        }
    }
}
