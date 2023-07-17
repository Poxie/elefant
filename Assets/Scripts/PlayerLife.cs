using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife: MonoBehaviour {
    int deathCounter = 0;
    bool dead = false;

    Rigidbody2D rb;
    Animator animator;
    PlayerAnimations animationScript;
    PlayerController playerController;
    UIManager uiManager;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animationScript = transform.Find("Sprite").GetComponent<PlayerAnimations>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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

    void updateDeathCounter() {
        deathCounter++;
        uiManager.UpdateDeathCounter(deathCounter);
    }

    public void Die() {
        updateDeathCounter();
        dead = true;
        animationScript.enabled = false;
        animator.Play("Death");
        Destroy(rb);
        uiManager.ShowDeathScreen();
    }
}
