using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    private Animator _animator = null;
    private Sprite _sprite = null;

    private bool _caught = false;

    void Awake()
    {
        _sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        _animator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (GameController.Instance != null)
        {
            if (GameController.Instance.IsPaused && _animator.speed != 0f)
                Pause();
            else if (!GameController.Instance.IsPaused && _animator.speed == 0f)
                Unpause();
        }
    }

    private void Pause()
    {
        _animator.speed = 0f;
    }

    private void Unpause()
    {
        _animator.speed = 1f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_caught && other.CompareTag(Tags.Player.ToString()))
        {
            EventSystem.GameController_AddItem.Notify(_sprite);
            _animator.SetTrigger("Collected");
            _caught = true;
            Destroy(transform.parent.gameObject, 0.4f);
        }
    }

}
