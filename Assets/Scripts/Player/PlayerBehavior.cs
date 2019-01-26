using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour {

    private new Rigidbody2D rigidbody2D;

    [SerializeField]
    private float speedForce;
    private float realSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask groundLayer;

    private bool isJumping;
    private bool isGround;

    // Start is called before the first frame update
    void Start() {
        realSpeed = speedForce;
    }

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();

        isJumping = false;
        isGround = true;
    }

    // Update is called once per frame
    void Update() {
        float horinzontalAxis = Input.GetAxis("Horizontal");
        float jumpAxis = Input.GetAxis("Jump");

        if (horinzontalAxis != 0f) {
            rigidbody2D.AddForce(Vector2.right * realSpeed * horinzontalAxis);
        }

        if (jumpAxis > 0f && !isJumping) {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            isJumping = true;
            Debug.Log("Pulou");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isGround) {
            if (collision.collider.CompareTag("Ground")) {
                realSpeed = speedForce;
                isJumping = false;
                isGround = true;
                Debug.Log("Está no chão");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (isGround) {
            if (collision.collider.CompareTag("Ground")) {
                realSpeed = speedForce / 2;
                isJumping = true;
                isGround = false;
                Debug.Log("Fora do chão");
            }
        }
    }
}
