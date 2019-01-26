using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Image[] lifes;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void gameOver() {

    }

    public void qtdeVidas(int lifes) {
        for (int i = 0; i < this.lifes.Length; i++) {
            if (i < lifes) {
                this.lifes[i].gameObject.SetActive(true);
            } else {
                this.lifes[i].gameObject.SetActive(false);
            }
        }
    }

}
