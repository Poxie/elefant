using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrap: MonoBehaviour {
    [SerializeField] float fanForce = 40;
    [SerializeField] float fanFloatVelocity = 25;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerStay2D(Collider2D collision) {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * fanFloatVelocity;
    }
}
