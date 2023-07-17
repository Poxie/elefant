using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    public bool isJumping {get; set;}
    public bool jumpedOnEnemy {get; set;} //maybe remove
    public bool isGrounded {get; set;}
    public Vector2 move {get;set;} 
}
