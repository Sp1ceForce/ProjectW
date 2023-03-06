using UnityEngine;
using System;
public class EnemyHealthComponent : MonoBehaviour, IHealthComponent
{
    //Пока что временно будут просто переменные, позже продумаю как грамотно сделать классы для статов
    public int MaxHealth;
    public int CurrentHealth;
    public Action OnDeath;
    public Action OnTakeDamage;
    public Action<int> OnHealthChange;
    private void Start() {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int Damage){
        CurrentHealth -=Damage;
        OnTakeDamage?.Invoke();
        if(CurrentHealth<=0){
            OnDeath?.Invoke();
            Death();
        }
    }
    public void Heal(int Healing){
        CurrentHealth+=Healing;
        CurrentHealth = Math.Clamp(CurrentHealth,0,MaxHealth);
        OnHealthChange?.Invoke(CurrentHealth);
    }
    void Death(){
        gameObject.SetActive(false);
    }
}
