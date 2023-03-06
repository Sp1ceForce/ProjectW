using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseBomb : BaseQuickslotItem
{
    protected BombGlobalData bombData;
    [SerializeField]
    List<BombEffectType> effectsList;
    [SerializeReference]
    List<IBombEffect> effectsClasses;
    [SerializeField]
    TestHandler tempHandler;
    public BaseBomb(TestHandler handler, List<BombEffectType> effectsList)
    {
        Name = "Bomb with effects: ";
        this.effectsList = effectsList;
        effectsClasses = new List<IBombEffect>();
        tempHandler = handler;
        foreach (var effect in effectsList)
        {
            effectsClasses.Add(BombEffectFactory.CreateEffect(effect));
            Name += effect.ToString() +" ";
        }
    }
    IEnumerator ThrowBomb(GameObject instigator)
    {
        if(bombData == null){
            if(BombGlobalData.Instance == null) Debug.LogError("Отсутствует Global Data для бомб");
            else bombData = BombGlobalData.Instance;
        }
        //Получение координат куда должна прилететь бомба
        Vector3 endPosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, bombData.RaycastMask);

        endPosition = hit.point;
        //Анимация броска бомбы
        Vector3 startPosition = instigator.transform.position + instigator.transform.forward * 0.7f;
        var bomb = GameObject.Instantiate(bombData.BombObject, startPosition, Quaternion.identity);
        float timePassed = 0f;

        while (timePassed < bombData.ThrowDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / bombData.ThrowDuration;
            float heightT = bombData.ThrowingArc.Evaluate(linearT);
            float height = Mathf.Lerp(0f, bombData.MaxHeight, heightT);
            bomb.transform.position = Vector3.Lerp(startPosition, endPosition, linearT) + new Vector3(0, height, 0);
            yield return new WaitForEndOfFrame();
        }

        //Ожидание взрыва гранаты
        yield return new WaitForSeconds(bombData.ExplosionTime);
        
        //Взрыв 
        var colliders = Physics.OverlapSphere(endPosition, tempHandler.Radius, bombData.PhysicsMask);
        ExplosionLogic(instigator, endPosition, colliders);
        GameObject.Destroy(bomb);
    }
    
    public override void Activate(GameObject instigator)
    {
        instigator.GetComponent<MonoBehaviour>().StartCoroutine(ThrowBomb(instigator));
    }
    protected void ExplosionLogic(GameObject instigator, Vector3 explosionPosition, Collider[] entitiesHit){
        foreach (var effectClass in effectsClasses)
        {
            effectClass.ActivateEffect(entitiesHit, explosionPosition, tempHandler);
        }
    }
}

