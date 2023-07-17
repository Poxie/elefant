using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife: MonoBehaviour {
    Animator animator;
    PlayerAnimations animationScript;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        playerController = GetComponent<PlayerController>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animationScript = transform.Find("Sprite").GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update() {
        if(
            animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        ) {
            // Fix better system to disable controls/remove sprite
            Destroy(playerController);
            Destroy(transform.Find("Sprite").gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Fire")) {
            die();
        }
    }

    void die() {
        animationScript.enabled = false;
        animator.Play("Death");
    }
}
