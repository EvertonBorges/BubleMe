using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private float startSize;
    private float offsetX;
    private float minX;
    private float maxX;
    private float startY;

    private Vector3 largeScale;
    private float largeSize;

    void Awake() {
        startSize = Camera.main.orthographicSize;
        offsetX = transform.position.x - target.position.x;
        minX = 01.7f;
        maxX = 87.4f;
        startY = transform.position.y;

        largeScale = new Vector3(43f, 21.4f, -10f);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(PosicaoCameraX(), startY, -10f);
    }

    private float PosicaoCameraX() {
        float suggestedX = offsetX + target.position.x;

        if (suggestedX < minX) {
            return minX;
        } else if (suggestedX > maxX) {
            return maxX;
        } else {
            return suggestedX;
        }
    }

}