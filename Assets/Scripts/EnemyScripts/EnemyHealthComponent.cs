using UnityEngine;
using System;
public class EnemyHealthComponent : MonoBehaviour
{
    //Пока что временно будут просто переменные, позже продумаю как грамотно сделать классы для статов
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    public Action OnDeath;
    public Action OnTakeDamage;
    public Action<int> OnHealthChange;
    private void Start() {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int Damage){
        currentHealth -=Damage;
        OnTakeDamage?.Invoke();
        if(currentHealth<=0){
            OnDeath?.Invoke();
            Death();
        }
    }
    void Death(){
        Destroy(gameObject);
    }
}
