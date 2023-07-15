using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D rb;

    float inputH;
    
    [Header("Movement variables")]
    [SerializeField] float movementSpeed = 8;
    [SerializeField] Transform feetPosition;
    [SerializeField] float circleRadius = .05f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 15;
    [SerializeField] float jumpTime = .5f;
    float jumpTimeCounter;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        inputH = Input.GetAxis("Horizontal");

        bool isGrounded = Physics2D.OverlapCircle(feetPosition.position, circleRadius, groundLayer);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetKey(KeyCode.Space)) {
            if(jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)) {
            jumpTimeCounter = 0;
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(inputH * movementSpeed, rb.velocity.y);
    }
}