using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw: MonoBehaviour {
    [SerializeField] Instruction[] instructions;

    int instructionIndex = 1;
    bool preventAnimation = false;
    float startTime = 0;

    [System.Serializable]
    class Instruction {
        public int speed = 1;
        public Vector2 position;
    }

    // Awake is called before the start function
    void Awake() {
        transform.position = new Vector3(instructions[0].position.x, instructions[0].position.y, 0);
    }

    // Update is called once per frame
    void Update() {
        if(instructions.Length == 1) {
            Destroy(this);
            return;
        }

        Instruction prevInstruction = instructions[instructionIndex - 1 < 0 ? instructions.Length - 1 : instructionIndex - 1];
        Instruction nextInstruction = instructions[instructionIndex];
        Vector2 nextPosition = nextInstruction.position;

        if(startTime == 0 && transform.position.y == prevInstruction.position.y && transform.position.x == prevInstruction.position.x) {
            startTime = Time.time;
        }
        float journeyLength = Vector3.Distance(prevInstruction.position, nextPosition);
        float distanceCovered = (Time.time - startTime) * nextInstruction.speed;

        float fractionOfJourney = distanceCovered / journeyLength;

        transform.position = Vector3.Lerp(prevInstruction.position, nextPosition, fractionOfJourney);

        if(transform.position.y == nextPosition.y && transform.position.x == nextPosition.x) {
            startTime = Time.time;
            instructionIndex++;
            if(instructionIndex >= instructions.Length) {
                instructionIndex = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerLife>().Die();
        }
    }
}
