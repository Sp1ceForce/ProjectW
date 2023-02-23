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

    [SerializeField] public List<BaseQuickslotItem> quickslotItems;
    [SerializeField] public List<InventoryQuickSlot> InventoryQuickSlotItems;

    BaseQuickslotItem activeItem;
    public void OnQuickSlotButtonPress(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            switch (obj.control.displayName)
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
    void ChangeQuickSlot(int slotIndex)
    {
        int newSlotIndex = slotIndex - 1;
        if (quickslotItems[newSlotIndex] != null)
        {
            activeItem = quickslotItems[newSlotIndex];
        }
    }
    public void OnBlinkButtonPress(InputAction.CallbackContext obj)
    {
        if (obj.started) BlinkSkill.Activate(gameObject);
    }
    public void OnMeleeButtonPress(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
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

    public void OnRightClickPress(InputAction.CallbackContext obj)
    {
        if (activeItem == null) return;
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                OnAimingStart?.Invoke();
                OnActiveSpellChanged?.Invoke();
                break;
            case InputActionPhase.Canceled:
                InventoryQuickSlot quickSLot = InventoryQuickSlotItems[quickslotItems.IndexOf(activeItem)];
                if (quickSLot.transform.GetChild(0).TryGetComponent<BombHandler>(out BombHandler bombHandler))
                {
                    ExplosiveBomb bombItem = activeItem as ExplosiveBomb;

                    if (bombItem == null) { Debug.Log("Casting Failed"); }
                    else
                    {
                        bombItem.useBombHandler(bombHandler);
                    }
                }
                if (quickSLot.transform.GetChild(0).TryGetComponent<PotionHandler>(out PotionHandler potionHandler))
                {
                    HealingPotion potionItem = activeItem as HealingPotion;

                    if (potionItem == null) { Debug.Log("Casting Failed"); }
                    else
                    {
                        potionItem.usePotionHandler(potionHandler);
                    }
                }
                activeItem.Activate(gameObject);
                quickSLot.removeItemFromSkillController();
                OnAimingEnd?.Invoke();
                break;
        }
    }
    public void OnProjectileButtonPress(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
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
    public void OnWaveButtonPress(InputAction.CallbackContext obj)
    {
        if (obj.started) WaveSkill.Activate(gameObject);
    }
}
