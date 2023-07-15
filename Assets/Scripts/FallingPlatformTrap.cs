using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformTrap : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float timeBeforeChangeDir;

    int dir = 1;
    float timerForDir;

    Vector2 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {
        timerForDir += Time.deltaTime;
                Debug.Log(playerVelocity.y);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timerForDir >= timeBeforeChangeDir) {
            timerForDir = 0;
            dir = -dir;
        }
        

        transform.Translate(dir * Vector3.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
        playerVelocity = playerRb.velocity;
    }

    private void OnTriggerStay2D(Collider2D other) {
        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
        Debug.Log("CIRREMT:" + playerRb.velocity.y);
        if(playerRb.velocity.y == 0) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -playerVelocity.y * Time.deltaTime, transform.position.z), Time.deltaTime);
        }
    }
}
