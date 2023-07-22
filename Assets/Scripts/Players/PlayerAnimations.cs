using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    
    Animator animator;

    float lookingRight = 1;

    [SerializeField] InputHandler _input;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if(!Mathf.Approximately(_input.move.x, 0))
            lookingRight = _input.move.x;

        if(lookingRight < 0) {
            transform.eulerAngles = new Vector3(0,180,0);
        } else if (lookingRight > 0) {
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if(_input.isGrounded != true) {
            if(_input.move.y > 0) {
                animator.Play("Jump"); 
                Debug.Log("He");
            }
                
            else if (_input.move.y < 0){
                animator.Play("Fall");
            }
                
        }

        else if(Input.GetAxisRaw("Horizontal") != 0) {
            animator.Play("Run");

            
        } else {
            animator.Play("Idle");
        }
    }
}