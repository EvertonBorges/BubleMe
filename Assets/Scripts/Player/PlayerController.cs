using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour {

    private int life;

    void Awake() {
        life = 1;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void addLife() {
        addLife(1);
    }

    public void removeLife() {
        addLife(-1);
    }

    public void addLife(int lifeToAdd) {
        life += lifeToAdd;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Life")) {
            GameObject.Destroy(collision.gameObject);
            addLife();
        }
    }

}
