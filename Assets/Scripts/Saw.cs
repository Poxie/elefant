using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw: MonoBehaviour {
    [SerializeField] Instruction[] instructions;
    [SerializeField] Vector3 startPosition = Vector3.zero;

    int instructionIndex = 1;
    bool preventAnimation = false;
    bool isFirstIterration = true;
    float startTime = 0;

    [System.Serializable]
    class Instruction {
        public int speed = 1;
        public Vector3 position;
    }

    // Awake is called before the start function
    void Awake() {
        startPosition = startPosition == Vector3.zero ? instructions[0].position : startPosition;
        transform.position = startPosition;
    }

    void Update() {
        if(instructions.Length == 1) {
            Destroy(this);
            return;
        }

        Instruction prevInstruction = instructions[instructionIndex - 1 < 0 ? instructions.Length - 1 : instructionIndex - 1];
        Vector2 prevPosition = prevInstruction.position;
        Instruction nextInstruction = instructions[instructionIndex];
        Vector2 nextPosition = nextInstruction.position;

        if(isFirstIterration) {
            prevPosition = startPosition;
        }
        if(startTime == 0 && transform.position.y == prevPosition.y && transform.position.x == prevPosition.x) {
            startTime = Time.time;
        }
        float journeyLength = Vector3.Distance(prevPosition, nextPosition);
        float distanceCovered = (Time.time - startTime) * nextInstruction.speed;

        float fractionOfJourney = distanceCovered / journeyLength;

        transform.position = Vector3.Lerp(prevPosition, nextPosition, fractionOfJourney);

        if(transform.position.y == nextPosition.y && transform.position.x == nextPosition.x) {
            isFirstIterration = false;
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
