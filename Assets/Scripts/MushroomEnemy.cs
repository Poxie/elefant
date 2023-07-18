using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour {

   
    Animator animator;
    Rigidbody2D rb;
    EnemyUnit unit;

    int dirX = 1;
    bool isIdle = false;
    float timerForIdle;
     

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        unit = GetComponent<EnemyUnit>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("EnemyWall")) {
            dirX *= -1;
            timerForIdle = unit.idleTimer;
            isIdle = true;
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
        if(unit.isDead) {
            Destroy(this);
        }

    }
    private void FixedUpdate() {
        rb.velocity = new Vector2(unit.speed * dirX * (isIdle ? 0 : 1), rb.velocity.y); 
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
