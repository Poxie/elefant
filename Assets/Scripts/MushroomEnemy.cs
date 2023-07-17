using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float idleTimer;

    Animator animator;
    Rigidbody2D rb;

    int dirX = 1;
    bool isIdle = false;
    float timerForIdle;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("EnemyWall")) {
            dirX *= -1;
            timerForIdle = idleTimer;
            isIdle = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("PlayerHitbox")) {
            Debug.Log("fu");
            other.transform.parent.GetComponent<PlayerLife>().Die();
        }
    }

    // Update is called once per frame
    void Update() {
        animator.SetFloat("Move", Mathf.Abs(rb.velocity.x));

        if(isIdle == true) {
            if(timerForIdle > 0 ) {
                timerForIdle -= Time.deltaTime;
            } else {
                isIdle = false;
            }
        }

    }
    private void FixedUpdate() {
        rb.velocity = new Vector2(speed * dirX * (isIdle ? 0 : 1), rb.velocity.y); //To fast.
    }
    
    private void LateUpdate() {
        if(timerForIdle <= 0) {
            if(dirX > 0) {
            transform.eulerAngles = new Vector3(0,180,0);
            } else if (dirX < 0) {
                transform.eulerAngles = new Vector3(0,0,0);
            }
        }
        
    }
}
