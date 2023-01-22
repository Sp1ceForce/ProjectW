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
public class Witch : MonoBehaviour {
    //Статы
    [SerializeField] WitchData witchData;


    //События
    public Action OnHealthChange;
    public Action OnDeath;
    public Action OnCurseLevelChange;
    
    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (WitchGlobalData.Instance.WitchData != null) witchData = WitchGlobalData.Instance.WitchData;
    }

    public void TakeDamage(int Damage){
        int newHealth = witchData.CurrentHealth-Damage;
        if(witchData.CurrentHealth == 1){
            witchData.CurrentHealth = newHealth;
        }
        else {
            witchData.CurrentHealth = Math.Clamp(newHealth,1,witchData.MaxHealth);
        }
        if(witchData.CurrentHealth <=0) OnDeath?.Invoke();
    }
    public void Heal(int Healing){
        witchData.CurrentHealth+=Healing;
        witchData.CurrentHealth = Math.Clamp(witchData.CurrentHealth,0,witchData.MaxHealth);
    }
    public IEnumerator CurseIncreaseCoroutine(){
        yield return new NotImplementedException();
    }
}