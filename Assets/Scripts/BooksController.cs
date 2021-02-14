using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksController : MonoBehaviour
{
    
    [SerializeField] private Transform _booksTop;
    [SerializeField] private Transform _booksBottom;

    private bool _booksFell = false;

    private void BookFallDown()
    {
        if (!_booksFell) {
            _booksTop.gameObject.SetActive(false);
            _booksBottom.gameObject.SetActive(true);

            EventSystem.Sfx_BooksFall.Notify();
            _booksFell = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player.ToString()))
            BookFallDown();
    }

}
