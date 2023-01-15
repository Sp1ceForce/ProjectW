using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SkillsController : MonoBehaviour
{
    [SerializeField] Blink BlinkSkill;
    [SerializeField] ProjectileSpell ProjectileSkill;
    [SerializeField] Wave WaveSkill;
    [SerializeField] MeleeAttack MeleeAttack;

    //События вызывающиеся при:
    //Смене активного заклинания(пригодится когда буду реализовывать отрисовку индикаторов)
    public Action OnActiveSpellChanged;
    //Начале активации спелла требующего поворот персонажа к курсору
    public Action OnAimingStart;
    //Выпускании спелла и возвращения к обычному режиму поворота персонажа
    public Action OnAimingEnd;


    public void OnBlinkButtonPress(InputAction.CallbackContext obj){
        if(obj.started) BlinkSkill.Activate(gameObject);
    }
    public void OnMeleeButtonPress(InputAction.CallbackContext obj){
        switch(obj.phase){
            case InputActionPhase.Started:
            OnAimingStart?.Invoke();
            OnActiveSpellChanged?.Invoke();
            break;
            case InputActionPhase.Performed:
            MeleeAttack.Activate(gameObject);
            break;
            case InputActionPhase.Canceled:
            OnAimingEnd?.Invoke();
            break;
        }

    }
    public void OnProjectileButtonPress(InputAction.CallbackContext obj){
        switch(obj.phase){
            case InputActionPhase.Started:
            OnAimingStart?.Invoke();
            OnActiveSpellChanged?.Invoke();
            break;
            case InputActionPhase.Canceled:
            ProjectileSkill.Activate(gameObject);
            OnAimingEnd?.Invoke();
            break;
        }
    }
    public void OnWaveButtonPress(InputAction.CallbackContext obj){
        if(obj.started) WaveSkill.Activate(gameObject);
    }
}
