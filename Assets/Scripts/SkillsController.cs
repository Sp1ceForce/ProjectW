using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SkillsController : MonoBehaviour
{
    [SerializeField] Blink BlinkSkill;
    [SerializeField] Spell ProjectileSkill;

    //Класс отвечающий за вызов событий при нажатии на кнопки
    PlayerInputActions playerInputActions;


    public void OnBlinkButtonPress(InputAction.CallbackContext obj){
        if(obj.performed){
            BlinkSkill.Activate(gameObject);
        }
    }
    public void OnMeleeButtonPress(InputAction.CallbackContext obj){
        Debug.Log(obj.phase);
        BlinkSkill.Activate(gameObject);
    }
    public void OnProjectileButtonPress(InputAction.CallbackContext obj){
        switch(obj.phase){
            case InputActionPhase.Started:
            break;
            case InputActionPhase.Canceled:
            ProjectileSkill.Activate(gameObject);
            break;
        }
    }
}
