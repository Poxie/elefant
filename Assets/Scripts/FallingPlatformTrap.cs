using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformTrap : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float timeBeforeChangeDir;

    int dir = 1;
    float timerForDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {
        timerForDir += Time.deltaTime;
        Debug.Log(timerForDir);
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
}
