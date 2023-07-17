using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife: MonoBehaviour {
    bool dead = false;

    Rigidbody2D rb;
    Animator animator;
    PlayerAnimations animationScript;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
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
            transform.Find("Sprite").gameObject.SetActive(false);
        }

        if(dead && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Die() {
        dead = true;
        animationScript.enabled = false;
        animator.Play("Death");
        Destroy(rb);
    }
}
