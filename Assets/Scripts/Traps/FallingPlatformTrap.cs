using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformTrap : MonoBehaviour
{   
    
    private Rigidbody2D theRB;
    private Animator animator;

    private Vector2 initialPosition;
    //float originalY;
    [SerializeField] private float speed = 5;
    [SerializeField] private float floatStrength = .1f;
    [SerializeField] private float offTime = 0.8f;

    bool isTouched = false;

    float timerBeforeOff = 0;
    ParticleSystem particles;
    bool iniateFall = false;
 
    void Start()
    {
        theRB = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        //initialPosition = transform.position;
        initialPosition = theRB.position;
        //this.originalY = this.transform.position.y;

        timerBeforeOff = offTime;
        particles = GetComponentInChildren<ParticleSystem>();

    }

    private void Update() {
        if(isTouched == true) {
            timerBeforeOff -= Time.deltaTime;
        }
        if(timerBeforeOff <= 0) {
            particles.Stop();
            animator.Play("Off");
        }
        if(timerBeforeOff + 0.6f <= 0) {
            iniateFall = true;
        }
    }
 
    void FixedUpdate()
    {
        //transform.position = new Vector2(transform.position.x, originalY + ((float)Mathf.Sin(Time.time * speed) * floatStrength));
 
        float newY = Mathf.Sin(Time.time * speed) * floatStrength;
        Vector2 position = new Vector2(0, newY) + initialPosition;
        if(iniateFall == false) {
            theRB.MovePosition(position);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(theRB.IsTouching(other.collider)) {
            isTouched = true;
        }
    }
}
