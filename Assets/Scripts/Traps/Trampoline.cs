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
            InputHandler _playerInput = collision.GetComponent<InputHandler>();
            Debug.Log(transform.up);
            _playerInput.temporaryMove = transform.up * trampolineForce;
            animator.Play("TrampolineOn");
        }
    }
}
