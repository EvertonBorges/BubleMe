using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //[SerializeField]
    //private Image[] lifes;

    [SerializeField]
    private Image[] coins;

    [SerializeField]
    private Text gameOverText;

    private void Awake() {
        gameOverText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void gameOver() {
        Debug.Log("Game Over");
        gameOverText.gameObject.SetActive(true);
        Application.Quit();
    }

    /*
    public void qtdeVidas(int lifes) {
        for (int i = 0; i < this.lifes.Length; i++) {
            if (i < lifes) {
                this.lifes[i].gameObject.SetActive(true);
            } else {
                this.lifes[i].gameObject.SetActive(false);
            }
        }
    }
    */

    public void qtdeCoins(int coins) {
        for (int i = 0; i < this.coins.Length; i++) {
            if (i < coins) {
                this.coins[i].gameObject.SetActive(true);
            } else {
                this.coins[i].gameObject.SetActive(false);
            }
        }

        if (coins == this.coins.Length) {
            Win();
        }
    }

    private void Win() {
        Debug.Log("Ganhou");
    }

}
