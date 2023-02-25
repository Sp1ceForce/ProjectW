using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseBomb : BaseQuickslotItem
{
    [Header("Полёт бомбы и эффекты")]
    [SerializeField] AnimationCurve throwingArc;
    [SerializeField] float throwDuration = 1f;
    [SerializeField] float maxHeight = 2.5f;
    [SerializeField] GameObject bombObject;
    [SerializeField] GameObject explosionParticles;
    [Header("Взрыв бомбы")]
    [SerializeField] LayerMask physicsMask;
    [SerializeField] float explosionRaidus;
    [SerializeField] float explosionTime;
    public override void Activate(GameObject instigator)
    {
        instigator.GetComponent<MonoBehaviour>().StartCoroutine(ThrowBomb(instigator));
    }
    public virtual void InitiateBomb(List<BombEffectType> effects, BombHandler bombHandler)
    {

    }
    protected virtual void ExplosionLogic(GameObject instigator, Vector3 explosionPosition, Collider[] entitiesHit){

    }
    protected void PushAway(Collider[] entitiesHit, float pushForce, Vector3 explosionPosition)
    {
        foreach (var entity in entitiesHit)
        {
            EnemyKnockbackComponent knockbackComponent = entity.GetComponent<EnemyKnockbackComponent>();
            if (knockbackComponent != null) knockbackComponent.KnockBack(explosionPosition, pushForce);
        }
    }
    protected void DealDamage(Collider[] entitiesHit, int damage)
    {
        foreach (var entity in entitiesHit)
        {
            EnemyHealthComponent healthComponent = entity.GetComponent<EnemyHealthComponent>();
            if (healthComponent != null) healthComponent.TakeDamage(damage);
        }
    }
    IEnumerator ThrowBomb(GameObject instigator)
    {
        //Получение координат куда должна прилететь бомба
        Vector3 endPosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));

        endPosition = hit.point;
        //Анимация броска бомбы
        Vector3 startPosition = instigator.transform.position + instigator.transform.forward * 0.7f;
        var bomb = Instantiate(bombObject, startPosition, Quaternion.identity);
        float timePassed = 0f;

        while (timePassed < throwDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / throwDuration;
            float heightT = throwingArc.Evaluate(linearT);
            float height = Mathf.Lerp(0f, maxHeight, heightT);
            bomb.transform.position = Vector3.Lerp(startPosition, endPosition, linearT) + new Vector3(0, height, 0);
            yield return new WaitForEndOfFrame();
        }

        //Ожидание взрыва гранаты
        yield return new WaitForSeconds(explosionTime);

        //Взрыв 
        var explosion = Instantiate(explosionParticles, bomb.transform.position, Quaternion.identity);
        var colliders = Physics.OverlapSphere(endPosition, explosionRaidus, physicsMask);
        ExplosionLogic(instigator, endPosition, colliders);
        Destroy(bomb);
        Destroy(explosion, 10f);
    }



}

