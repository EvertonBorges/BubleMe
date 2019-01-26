using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private float offsetX;
    private float startY;

    void Awake() {
        offsetX = transform.position.x - target.position.x;
        startY = transform.position.y;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(offsetX + target.position.x, startY, -10f);
    }
}
