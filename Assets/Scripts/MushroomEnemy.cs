using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour {

    [SerializeField] Transform maxWalkRight;
    [SerializeField] Transform maxWalkLeft;
    [SerializeField] float speed;
    [SerializeField] float idleTimer;

    Animator animator;
    Rigidbody2D rb;

    float maxWalkRightXPosition;
    float maxWalkLeftXPosition;

    float idleTempTimer;

    int dir = 1;
    // Start is called before the first frame update
    void Start() {
        maxWalkRightXPosition = maxWalkRight.position.x;
        maxWalkLeftXPosition = maxWalkLeft.position.x;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        Destroy(maxWalkRight.gameObject);
        Destroy(maxWalkLeft.gameObject);
    }

    // Update is called once per frame
    void Update() {

        if(transform.position.x <= maxWalkLeftXPosition) {
            idleTempTimer = idleTimer;
            dir = -dir;
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if(transform.position.x >= maxWalkRightXPosition) {
            idleTempTimer = idleTimer;
            dir = -dir;
            transform.eulerAngles = new Vector3(0,180,0);
        }
        if(idleTempTimer <= 0) {
            
        }
        animator.SetFloat("Move", rb.velocity.x);
    }
    private void FixedUpdate() {
        rb.MovePosition(speed * Vector3.left * dir * Time.fixedDeltaTime);
    }
}
