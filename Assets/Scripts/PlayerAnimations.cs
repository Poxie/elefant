using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetAxis("Horizontal") != 0) {
            animator.Play("Run");

            if(Input.GetAxis("Horizontal") < 0) {
                transform.eulerAngles = new Vector3(0,180,0);
            } else {
                transform.eulerAngles = new Vector3(0,0,0);
            }
        } else {
            animator.Play("Idle");
        }
    }
}