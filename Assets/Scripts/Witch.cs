using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class WitchData {
    //Сделал эти поля общедоступными для того, чтобы была возможность редактировать их в классе Witch(В документе сказано что именно он должен их изменять) 
    [Header("Здоровье")]
    public int MaxHealth;
    public int CurrentHealth;
    [Header("Проклятье")]
    public int MaxCurse;
    public int CurrentCurse;

    //Сюда можно добавить интервал с которым проклятие будет увеличиваться, а так же насколько сильно будет повышаться само проклятие, функцию реализую, но добавить это можем и позже 
}
public class Witch : MonoBehaviour, IHealthComponent {
    [Header("Статы")]
    [SerializeField] bool useGlobalData = true;
    public WitchData witchData;


    //События
    public Action<int> OnHealthChange;
    public Action OnDeath;
    public Action OnCurseLevelChange;
    
    private void Start()
    {
        LoadData();
        OnHealthChange?.Invoke(witchData.CurrentHealth);
    }

    private void LoadData()
    {
        if (WitchGlobalData.Instance.WitchData != null && useGlobalData) witchData = WitchGlobalData.Instance.WitchData;
        witchData.CurrentHealth = witchData.MaxHealth;
    }
    
    public void TakeDamage(int Damage){
        Debug.Log(Damage);
        int newHealth = witchData.CurrentHealth-Damage;
        if(witchData.CurrentHealth == 1){
            witchData.CurrentHealth = newHealth;
        }
        else {
            witchData.CurrentHealth = Math.Clamp(newHealth,1,witchData.MaxHealth);
        }
        OnHealthChange?.Invoke(witchData.CurrentHealth);
        if(witchData.CurrentHealth <=0) {
            OnDeath?.Invoke();
            Death();
        }
    }
    void Death(){
        Destroy(gameObject);
    }
    public void Heal(int Healing){
        witchData.CurrentHealth+=Healing;
        witchData.CurrentHealth = Math.Clamp(witchData.CurrentHealth,0,witchData.MaxHealth);
        OnHealthChange?.Invoke(witchData.CurrentHealth);
    }
    public IEnumerator CurseIncreaseCoroutine(){
        yield return new NotImplementedException();
    }
}