using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    [SerializeField]
    private AudioSource som1;
    [SerializeField]
    private AudioSource som2;
    [SerializeField]
    private AudioSource somLivrosCaindo;

    // Start is called before the first frame update
    void Awake() {
        som1.Play();
        som1.loop = false;
        
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Song Time: " + som1.time);

        if (!som1.isPlaying) {
            if (!som2.isPlaying) {
                Debug.Log("Trocou o Som");
                som2.Play();
                som2.loop = true;
            }
        }
    }

    public void Morreu() {
        if (som1.isPlaying) {
            som1.Stop();
        }
        if (som2.isPlaying) {
            som2.Stop();
        }
    }


}
