using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeButton {
    JOGAR, PAUSAR, VOLTAR, RESTART, SAIR
}

[RequireComponent(typeof(Animator))]
public class ButtonEnum : MonoBehaviour {

    [SerializeField]
    private TypeButton buttonType;

    [SerializeField]
    private AudioSource bubbleSound;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Acao(){
        if (bubbleSound != null) {
            bubbleSound.Play();
        }
        animator.SetTrigger("Pressed");
    }

    public void EventoAcao() {
        switch (buttonType) {
            case TypeButton.JOGAR: Jogar(); break;
            case TypeButton.PAUSAR: Pausar(); break;
            case TypeButton.VOLTAR: Voltar(); break;
            case TypeButton.RESTART: Restart(); break;
            case TypeButton.SAIR: Sair(); break;
        }
    }

    private void Jogar() {
        EventSystem.Scene_Gameplay.Notify();
    }

    private void Pausar() {
        EventSystem.Player_Death.Notify();
    }

    private void Voltar() {
        EventSystem.Scene_MainMenu.Notify();
    }

    private void Restart() {
        EventSystem.Scene_Gameplay.Notify();
    }

    private void Sair() {
        EventSystem.Scene_Quit.Notify();
    }

}
