using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start() {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update() {

        transform.position = offset + target.position;

    }
}
