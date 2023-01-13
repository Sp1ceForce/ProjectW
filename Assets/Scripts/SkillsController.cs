using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SkillsController : MonoBehaviour
{
    [SerializeField] Blink BlinkSkill;

    //Класс отвечающий за вызов событий при нажатии на кнопки
    PlayerInputActions playerInputActions;


    public void OnBlinkUse(InputAction.CallbackContext obj){
        if(obj.performed){
            BlinkSkill.Activate(gameObject);
        }
    }
    public void OnMeleeUse(InputAction.CallbackContext obj){
        Debug.Log(obj.phase);
        BlinkSkill.Activate(gameObject);
    }
}
