using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour {

    private new Rigidbody2D rigidbody2D;
    private PlayerController playerController;

    [SerializeField]
    private float speedForce;
    [SerializeField]
    private float maxSpeed;
    private float realSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask groundLayer;

    private bool isWalking;
    private bool isGround;
    private bool isFacingLeft;
    private bool isInBubble;
    private Animator animator;

    [SerializeField]
    private CircleCollider2D colliderInBuble;
    [SerializeField]
    private CircleCollider2D[] colliderOffBubles;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D> ();
        playerController = GetComponent<PlayerController> ();
        animator = GetComponent<Animator> ();
        
        isGround = true;
        isFacingLeft = false;
        isInBubble = true;
    }

    // Start is called before the first frame update
    void Start() {
        realSpeed = speedForce;
    }

    // Update is called once per frame
    void Update() {
        if (!animator.IsInTransition(0)) {
            UpdateAnimation();
            tryMoviment(Input.GetAxis("Horizontal"));
            tryJump(Input.GetKey(KeyCode.Space) ? 2 : 0);

            if (rigidbody2D.velocity.x > maxSpeed) {
                rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
            }
        }
    }

    public void tryMoviment(float horinzontalAxis) {
        isWalking = (horinzontalAxis != 0);

        if (horinzontalAxis != 0f) {
            if (isInBubble) {
                rigidbody2D.AddForce(Vector2.right * (realSpeed * 2) * horinzontalAxis);
            } else {
                rigidbody2D.velocity = new Vector2((realSpeed / 2) * horinzontalAxis, rigidbody2D.velocity.y);
            }
        }

        if (rigidbody2D.velocity.x < 0 && !isFacingLeft) {
            isFacingLeft = true;

            // Muda o megaman para o lado certo
            MirrorSprite(-1, 1);
        } else if (rigidbody2D.velocity.x > 0 && isFacingLeft) {
            isFacingLeft = false;

            // Muda o megaman para o lado certo
            MirrorSprite(-1, 1);
        }
    }

    public void tryJump(int isToJump) {
        if ((isToJump > 0) && isGround) {
            rigidbody2D.AddForce(Vector2.up * (((isInBubble) ? jumpForce : jumpForce / 2) * isToJump));
            isGround = false;
            Debug.Log("Pulou");
        }
    }

    public void UpdateAnimation() {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isInBubble", isInBubble);

        rigidbody2D.freezeRotation = !isInBubble;
        colliderInBuble.enabled = isInBubble;
        foreach (CircleCollider2D colliderOffBuble in colliderOffBubles) {
            colliderOffBuble.enabled = !isInBubble;
        }

        if (!isInBubble) {
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Ground")) {
            if (!isGround) {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / 5f, 0f);
            }

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

    void MirrorSprite(float x, float y) {
		Vector3 localScale = transform.localScale; // Usar sempre o local ao inves da variavel absoluta (scale)

        localScale.x *= x;
		localScale.y *= y;

		transform.localScale = localScale;
	}

}
