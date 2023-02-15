using UnityEngine;
using System.Collections;
public abstract class BaseBomb : ScriptableObject, ISpellActivate
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
    public void Activate(GameObject instigator)
    {
        instigator.GetComponent<MonoBehaviour>().StartCoroutine(ThrowBomb(instigator));
    }

    protected abstract void ExplosionLogic(GameObject instigator, Vector3 explosionPosition, Collider[] entitiesHit);

    protected void DealDamage(Collider[] entitiesHit, int damage){
        foreach(var entity in entitiesHit){
            EnemyHealthComponent healthComponent = entity.GetComponent<EnemyHealthComponent>();
            if(healthComponent!=null) healthComponent.TakeDamage(damage);
        }
    }
    IEnumerator ThrowBomb(GameObject instigator){
        //Получение координат куда должна прилететь бомба
        Vector3 endPosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
        endPosition = hit.point;
        //Анимация броска бомбы
        Vector3 startPosition = instigator.transform.position + instigator.transform.forward * 0.7f;
        var bomb = Instantiate(bombObject,startPosition,Quaternion.identity);
        float timePassed = 0f;
        while(timePassed < throwDuration){
            float linearT = timePassed / throwDuration;
            float heightT = throwingArc.Evaluate(linearT);

            float height = Mathf.Lerp (0f,maxHeight,heightT);
            bombObject.transform.position = Vector3.Lerp(startPosition,endPosition,linearT) + new Vector3(0,height,0);
        }

        //Ожидание взрыва гранаты
        yield return new WaitForSeconds(explosionTime);

        //Взрыв 
        var colliders = Physics.OverlapSphere(endPosition,explosionRaidus,physicsMask);
        ExplosionLogic(instigator,endPosition,colliders);
        Destroy(bomb);
    }

    protected void PushAway(Collider[] entitiesHit, float pushForce, Vector3 explosionPosition){
        foreach(var entity in entitiesHit){
            EnemyKnockbackComponent knockbackComponent = entity.GetComponent<EnemyKnockbackComponent>();
            if(knockbackComponent!=null) knockbackComponent.KnockBack(explosionPosition,pushForce);
        }
    }

}

