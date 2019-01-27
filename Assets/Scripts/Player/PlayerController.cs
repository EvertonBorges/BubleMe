using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehavior))]
public class PlayerController : MonoBehaviour {

    private int life;
    private int coins;
    private GameController gameController;
    private PlayerBehavior playerBehavior;

    [SerializeField]
    private float timeToRecover;
    private float actualTimeToRecover;

    private bool isHitable;
    private bool isHitade;

    [SerializeField]
    private AudioSource deadSound;
    [SerializeField]
    private AudioSource collectedSound;

    private bool isDead;

    void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController> ();
        playerBehavior = GetComponent<PlayerBehavior> ();
        isHitade = false;
        isHitable = true;
        coins = -1;
        isDead = false;
    }

    // Start is called before the first frame update
    void Start() {
        addLife();
        addCoin();
    }

    // Update is called once per frame
    void Update() {
        if (actualTimeToRecover > 0f && !isHitable) {
            actualTimeToRecover -= Time.deltaTime;
            if (actualTimeToRecover < 0f) {
                Debug.Log("Terminou de se recuperar");
                isHitable = true;
            }
        }

        checkDeath();
    }

    public void addLife() {
        Debug.Log("Pegou vida");
        addLife(1);
    }

    public void removeLife() {
        Debug.Log("Perdeu vida");
        addLife(-1);
    }

    public void addLife(int lifeToAdd) {
        life += lifeToAdd;
        //gameController.qtdeVidas(life);
    }

    public void addCoin() {
        Debug.Log("Pegou ficha");
        addCoin(1);
    }

    public void removeCoin() {
        Debug.Log("Perdeu ficha");
        addCoin(-1);
    }

    public void addCoin(int coinToAdd) {
        coins += coinToAdd;
        gameController.qtdeCoins(coins);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Life")) {
            GameObject.Destroy(collision.gameObject);
            addLife();
        }

        if (collision.CompareTag("Coin")) {
            collectedSound.Play();
            collision.GetComponentInParent<ItemController>().collected(this);

            //addCoin();
        }

        if (collision.CompareTag("CairLivros")) {
            gameController.CairLivros();
        }

        if (collision.CompareTag("SpikeEnemy")) {
            if (isHitable) {
                Debug.Log("Começou contato com spike");
                removeLife();
                isHitable = false;
                isHitade = true;

                if (life <= 0) {
                    death();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (isHitade) {
            if (collision.CompareTag("SpikeEnemy")) {
                Debug.Log("Terminou contato com spike");
                startTimeToRecover();
                isHitade = false;
            }
        }
    }

    private void startTimeToRecover() {
        Debug.Log("Começou a se recuperar");
        isHitable = false;
        actualTimeToRecover = timeToRecover;
    }

    private void checkDeath() {
        if (transform.position.y < -5f) {
            death();
        }
    }

    public void death() {
        if (!isDead) {
            isDead = true;
            playerBehavior.Morreu();
            deadSound.Play();
            gameController.gameOver();
        }
    }

}
