using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMoviment : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private PlayerBehavior player;
    private Vector2 startedPressPosition;
    private Vector2 actualPressPosition;
    private bool isPressed;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior> ();
    }

    void Update() {
        if (isPressed) {
            Vector2 point1 = startedPressPosition;
            Vector2 point2 = actualPressPosition;

            float axis = Mathf.Abs(Vector2.Distance(point2, point1));
            axis /= 5;
            axis = (axis > 1) ? 1 : axis;

            player.tryMoviment((point2.x < point1.x) ? -axis : axis);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        isPressed = true;
        startedPressPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        isPressed = false;
        startedPressPosition = player.transform.position;
        actualPressPosition = player.transform.up;
    }

    public void OnDrag(PointerEventData eventData) {
        actualPressPosition = eventData.position;
    }
}
