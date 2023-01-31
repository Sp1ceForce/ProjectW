using UnityEngine;

[System.Serializable]
public class BlinkData:SpellData {
    public float BlinkDistance => blinkDistance;
    [SerializeField] float blinkDistance;

}
[CreateAssetMenu(fileName = "Blink", menuName = "ProjectW/Skills/Blink", order = 0)]
public class Blink : Spell
{
    //Отступ от стены в случае попадания луча в стену 
    [SerializeField] float onWallHitOffset = 0.5f;
    [SerializeField] BlinkData spellData;
    public override void Activate(GameObject Instigator)
    {
        if(SpellGlobalData.Instance.BlinkData == null) spellData = SpellGlobalData.Instance.BlinkData;
        if(!canCast) return;

        Vector3 blinkDirection = Instigator.GetComponent<MoveController>().InputVector;
        RaycastHit hit;
        //Костыль, но не знаю как пофиксить
        //UPD: Нашёл как пофиксить - сменил в настройках Auto Sync Transforms(ProjectSettings/Physics), на всякий случай оставил прошлый вариант если появятся проблемы
        //Instigator.GetComponent<CharacterController>().enabled = false;
        if(!Physics.Raycast(Instigator.transform.position,blinkDirection,out hit, spellData.BlinkDistance)){
            Instigator.transform.position = Instigator.transform.position + blinkDirection * spellData.BlinkDistance;

        }
        else{
            Instigator.transform.position =Instigator.transform.position + ((hit.point - Instigator.transform.position) * onWallHitOffset);
        }
        //Instigator.GetComponent<CharacterController>().enabled = true;
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));

    }
}