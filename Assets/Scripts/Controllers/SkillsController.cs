using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SkillsController : MonoBehaviour
{
    [Header("Постоянные абилки")]
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
    [Header("Быстрые слоты")]

    [SerializeField] List<ISpellActivate> quickSlots;
    ISpellActivate activeSpell;
    public void OnQuickSlotButtonPress(InputAction.CallbackContext obj){
        if(obj.started){
            switch(obj.control.displayName)
            {
                case "1":
                ChangeQuickSlot(1);
                break;
                case "2":
                ChangeQuickSlot(2);
                break;
                case "3":
                ChangeQuickSlot(3);
                break;
                case "4":
                ChangeQuickSlot(4);
                break;
            }
        }
    }
    void ChangeQuickSlot(int slotIndex){
        int newSlotIndex = slotIndex - 1;
        if(quickSlots[newSlotIndex]!=null){
            activeSpell = quickSlots[newSlotIndex];
        }
    }
    public void OnBlinkButtonPress(InputAction.CallbackContext obj){
        if(obj.started) BlinkSkill.Activate(gameObject);
    }
    public void OnMeleeButtonPress(InputAction.CallbackContext obj){
        switch(obj.phase){
            case InputActionPhase.Started:
            OnAimingStart?.Invoke();
            OnActiveSpellChanged?.Invoke();
            break;
            case InputActionPhase.Canceled:
            MeleeAttack.Activate(gameObject);
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
