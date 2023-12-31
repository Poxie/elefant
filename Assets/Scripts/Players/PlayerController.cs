using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerController : NetworkBehaviour {
    Rigidbody2D rb;
    
    float jumpTimeCounter;
    float knockbackTimer = 0.5f;

    [SerializeField] InputHandler _input;

    [Header("Movement variables")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime = 1;

    [Header("Ground check variables")]
    [SerializeField] float collisionRadius = .2f;
    [SerializeField] Transform feetPostion;
    [SerializeField] LayerMask groundLayer;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData {
            _int = 0, 
            _bool = false
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner
    );

    public struct MyCustomData : INetworkSerializable{
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }


    public override void OnNetworkSpawn() {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) => {
            Debug.Log(OwnerClientId + "; Random Number:" + newValue._int + "; " + newValue._bool);    
        };
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if(!IsOwner) return; 

        if(Input.GetKeyDown(KeyCode.P)) {
            randomNumber.Value = new MyCustomData {
                _int =  Random.Range(0,100),
                _bool = (Random.value > 0.5f),
            };
        }
        
        _input.isGrounded = Physics2D.OverlapCircle(feetPostion.position, collisionRadius, groundLayer);


        float inputH = Input.GetAxis("Horizontal");
        _input.move = new Vector2(inputH, rb.velocity.y);

        
        if(Input.GetKeyDown(KeyCode.Space) && _input.isGrounded) {
            _input.isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetKey(KeyCode.Space) && _input.isJumping == true) {
            if(jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                _input.isJumping = false;
            }
        }
        
        if(Input.GetKeyUp(KeyCode.Space)) {
            _input.isJumping = false;
        }

        if(_input.temporaryMove.magnitude != 0) {
            float moveInputAmpfliy = 1.5f;
            knockbackTimer -= Time.deltaTime;
            if(_input.temporaryMove.x != 0) {
                _input.temporaryMove += new Vector2(_input.move.x * Time.deltaTime * movementSpeed * moveInputAmpfliy, 0);
            }
            if(_input.temporaryMove.y != 0) {
                _input.temporaryMove += new Vector2(0, Physics2D.gravity.y * rb.gravityScale * Time.deltaTime);
            }

            if(_input.isGrounded && knockbackTimer <= 0) {
                knockbackTimer = 0.5f;
                _input.temporaryMove = Vector2.zero;
            }
        }
    } 

    void FixedUpdate() {
        rb.velocity = new Vector2((_input.temporaryMove.x != 0 ? _input.temporaryMove.x : (_input.move.x * movementSpeed)), _input.temporaryMove.y != 0 ? _input.temporaryMove.y : rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy") {
            if(Mathf.Abs(Vector3.Dot(Vector3.up, other.transform.position - transform.position)) >= 0.4f) {
                other.gameObject.GetComponent<EnemyUnit>().TakeDamage();
                rb.velocity = Vector2.up * jumpForce * 1.65f;
                
            }
        }
       
    }
}
