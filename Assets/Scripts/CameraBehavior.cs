using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float camVelocity;

    private float startSize;
    private float offsetX;
    private float minX;
    private float maxX;
    private float startY;
    private float lastY;

    private Vector3 largeScale;
    private float largeSize;

    private float firstFloorY;
    private float secondFloorY;
    private float thirdFloorY;

    private bool playerIsAlive;

    void Awake() {
        startSize = Camera.main.orthographicSize;
        offsetX = transform.position.x - target.position.x;
        minX = 06.95f;
        maxX = 82.90f;
        startY = transform.position.y;
        lastY = startY;

        firstFloorY = 06.35f;
        secondFloorY = 16.35f;

        largeScale = new Vector3(43f, 21.4f, -10f);

        playerIsAlive = true;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (!playerIsAlive) return;

        if (startY > lastY) {
            transform.position = new Vector3(PosicaoCameraX(), (transform.position.y < startY) ? (transform.position.y + camVelocity * Time.deltaTime) : startY, -10f);
        } else if (startY < lastY) {
            transform.position = new Vector3(PosicaoCameraX(), (transform.position.y > startY) ? (transform.position.y - camVelocity * Time.deltaTime) : startY, -10f);
        } else {
            transform.position = new Vector3(PosicaoCameraX(), startY, -10f);
        }
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

    public void firstFloor() {
        lastY = startY;
        startY = firstFloorY;
    }

    public void secondFloor() {
        lastY = startY;
        startY = secondFloorY;
    }

    public void Morreu() {
        playerIsAlive = false;
    }

}