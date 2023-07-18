using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D rb;
    
    float jumpTimeCounter;

    [SerializeField] InputHandler _input;

    [Header("Movement variables")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime = 1;

    [Header("Ground check variables")]
    [SerializeField] float collisionRadius = .2f;
    [SerializeField] Transform feetPostion;
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        _input.isGrounded = Physics2D.OverlapCircle(feetPostion.position, collisionRadius, groundLayer);

        float inputH = Input.GetAxis("Horizontal");
        _input.move = new Vector2(inputH, rb.velocity.y);

        
        if(Input.GetKeyDown(KeyCode.Space) && _input.isGrounded) {
            _input.isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetKey(KeyCode.Space) && _input.isJumping == true) {
            if(jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                _input.isJumping = false;
            }
        }
        
        if(Input.GetKeyUp(KeyCode.Space)) {
            _input.isJumping = false;
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(_input.move.x * movementSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy") {
            if(Mathf.Abs(Vector3.Dot(Vector3.up, other.transform.position - transform.position)) >= 0.6f) {
                other.gameObject.GetComponent<EnemyUnit>().TakeDamage();
                rb.velocity = Vector2.up * jumpForce * 1.65f;
                
            }
        }
       
    }
}
