using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour {

    private Rigidbody2D _rigidbody2D = null;
    private Animator _animator = null;

    [SerializeField] private float speedForce = 15f;
    [SerializeField] private float maxSpeed = 15f;
    private float realSpeed;

    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private CameraBehavior cameraBehavior = null;
    [SerializeField] private Vector2 _forceDeath = new Vector2(200f, 500f);

    [SerializeField] private CircleCollider2D _bubbleCollider = null;

    [SerializeField] private Transform[] secondFloor = null;

    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _jumpClip = null;
    [SerializeField] private AudioClip _deathClip = null;

    private bool _isWalking = false;
    private bool _onGround = false;
    private bool _isFacingLeft = false;
    private bool _inBubble = true;
    private bool _isAlive = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _onGround = true;
        _isFacingLeft = false;
        _inBubble = true;
        _isAlive = true;
    }

    void Start()
    {
        realSpeed = speedForce;
    }

    void Update()
    {
        if (!_isAlive) return;

        OnGround();
        UpdateAnimation();
        TryMoviment(Input.GetAxis(InputKeys.Horizontal.ToString()));
        TryJump(Input.GetButtonDown(InputKeys.Jump.ToString()) ? 2 : 0);

        if (transform.position.y > secondFloor[0].position.y)
        {
            cameraBehavior.secondFloor();
            foreach (Transform floor in secondFloor) {
                floor.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }
        else
        {
            cameraBehavior.firstFloor();
            foreach (Transform floor in secondFloor) {
                floor.GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
    }

    private void OnGround()
    {
        var point = transform.position;
        var radius = transform.localScale.y * _bubbleCollider.radius;
        var layer = 1 << LayerMask.NameToLayer(Layers.Ground.ToString());

        var lastOnGround = _onGround;
        _onGround = Physics2D.OverlapCircle(point, radius, layer);

        if (_onGround != lastOnGround)
        {
            if (!lastOnGround)
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x / 5f, 0f);
            realSpeed = speedForce * (_onGround ? 1f : .5f);
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetBool("isWalking", _isWalking);
        _animator.SetBool("isInBubble", _inBubble);

        _bubbleCollider.enabled = _inBubble;

        if (!_inBubble)
            transform.rotation = Quaternion.identity;
    }

    public void TryMoviment(float horinzontalAxis)
    {
        _isWalking = (horinzontalAxis != 0);

        if (horinzontalAxis != 0f)
        {
            if (_inBubble)
                _rigidbody2D.AddForce(Vector2.right * (realSpeed * 2) * horinzontalAxis);
            else
                _rigidbody2D.velocity = new Vector2((realSpeed / 2) * horinzontalAxis, _rigidbody2D.velocity.y);
        }

        if (!_isFacingLeft && _rigidbody2D.velocity.x < 0)
        {
            _isFacingLeft = true;
            MirrorSprite(-1, 1);
        }
        else if (_isFacingLeft && _rigidbody2D.velocity.x > 0)
        {
            _isFacingLeft = false;
            MirrorSprite(-1, 1);
        }

        var abs = Mathf.Abs(_rigidbody2D.velocity.x);
        if (abs > maxSpeed)
        {
            var direction = _rigidbody2D.velocity.x >= 0f ? 1f : -1f;
            _rigidbody2D.velocity = new Vector2(maxSpeed * direction, _rigidbody2D.velocity.y);
        }
    }

    public void TryJump(int isToJump)
    {
        if ((isToJump > 0) && _onGround)
        {
            PlaySfx(_jumpClip);

            _rigidbody2D.AddForce(Vector2.up * (((_inBubble) ? jumpForce : jumpForce + 2) * isToJump));
            _onGround = false;
        }
    }

    private void MirrorSprite(float x, float y)
    {
		Vector3 localScale = transform.localScale;

        localScale.x *= x;
		localScale.y *= y;

		transform.localScale = localScale;
	}

    public void Death()
    {
        var direction = _isFacingLeft ? 1f : -1f;

        _animator.SetTrigger("Death");
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(new Vector2(_forceDeath.x * direction, _forceDeath.y));
        _isAlive = false;

        foreach(var collider in GetComponents<CircleCollider2D>())
            collider.enabled = false;

        PlaySfx(_deathClip);
    }

    private void PlaySfx(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private void OnEnable()
    {
        EventSystem.Player_Death.Add(Death);
    }

    private void OnDisable()
    {
        EventSystem.Player_Death.Remove(Death);
    }

}
