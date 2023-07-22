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

    // Instruction blueprint
    [System.Serializable]
    class Instruction {
        public int speed = 1;
        public Vector3 position;
    }

    void Awake() {
        // Checking if a start position has been set
        startPosition = startPosition == Vector3.zero ? instructions[0].position : startPosition;

        // Positioning the saw at the start position
        transform.position = startPosition;
    }

    void Update() {
        // If saw is static, prevent movement code from running
        if(instructions.Length == 1) {
            Destroy(this);
            return;
        }

        // Fetching current and next saw instruction - placement/speed of next position it should go to
        Instruction prevInstruction = instructions[instructionIndex - 1 < 0 ? instructions.Length - 1 : instructionIndex - 1];
        Vector2 prevPosition = prevInstruction.position;
        Instruction nextInstruction = instructions[instructionIndex];
        Vector2 nextPosition = nextInstruction.position;

        // If is initial iteration setup start time for animation
        // And use start position as current position instead of first instruction position
        if(isFirstIterration) {
            prevPosition = startPosition;
            startTime = Time.time;
        }

        // Determining where the saw is currently positioned
        float journeyLength = Vector3.Distance(prevPosition, nextPosition);
        float distanceCovered = (Time.time - startTime) * nextInstruction.speed;

        float fractionOfJourney = distanceCovered / journeyLength;

        // Updating saw position with above calculated portion of animation
        transform.position = Vector3.Lerp(prevPosition, nextPosition, fractionOfJourney);

        // If it reaches its next position, reset start time and move to next instruction
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
