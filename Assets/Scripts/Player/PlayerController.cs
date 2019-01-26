using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(PlayerBehavior))]
public class PlayerController : MonoBehaviour {

    private int life;
    private GameController gameController;
    private PlayerBehavior playerBehavior;

    void Awake() {
        life = 1;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController> ();
        playerBehavior = GetComponent<PlayerBehavior> ();
    }

    // Start is called before the first frame update
    void Start() {
        gameController.qtdeVidas(life);
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
            gameController.qtdeVidas(life);
        }

        if (collision.CompareTag("SpikeEnemy")) {
            Debug.Log("Perde vida");
            playerBehavior.SpikeHit();
        }
    }

}
