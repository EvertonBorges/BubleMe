using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour {

    private new Rigidbody2D rigidbody2D;
    private PlayerController playerController;

    [SerializeField]
    private float speedForce;
    private float realSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask groundLayer;

    private bool isJumping;
    private bool isGround;
    

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D> ();
        playerController = GetComponent<PlayerController> ();

        isJumping = false;
        isGround = true;
    }

    // Start is called before the first frame update
    void Start() {
        realSpeed = speedForce;
    }

    // Update is called once per frame
    void Update() {
        float horinzontalAxis = Input.GetAxis("Horizontal");
        bool isToJump = Input.GetKey(KeyCode.Space);

        if (horinzontalAxis != 0f) {
            rigidbody2D.AddForce(Vector2.right * realSpeed * horinzontalAxis);
        }

        if (isToJump && isGround) {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            isGround = false;
            Debug.Log("Pulou");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Ground")) {
            realSpeed = speedForce;
            isGround = true;
            Debug.Log("Está no chão");
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag("Ground")) {
            realSpeed = speedForce / 2;
            isGround = false;
            Debug.Log("Fora do chão");
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.CompareTag("Ground")) {
            realSpeed = speedForce;
            isGround = true;
            Debug.Log("Está no chão");
        }
    }

}
