using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float idleTimer;

    Animator animator;
    Rigidbody2D rb;


    int dir = 1;
    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
    }

    // Update is called once per frame
    void Update() {


        animator.SetFloat("Move", Mathf.Abs(rb.velocity.x));
    }
    private void FixedUpdate() {
        rb.velocity = new Vector2(speed * dir * (isIdle ? 0 : 1), rb.velocity.y); //To fast.
    }
    
    private void LateUpdate() {
        if(dir > 0) {
            transform.eulerAngles = new Vector3(0,180,0);
        } else if (dir < 0) {
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}
