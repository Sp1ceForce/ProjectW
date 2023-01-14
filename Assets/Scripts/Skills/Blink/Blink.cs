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
    BlinkData spellData;
    public override void Activate(GameObject Instigator)
    {
        //if(spellData == null) spellData = SpellGlobalData.Instance.BlinkData;

        if(!canCast) return;
        Vector3 blinkDirection = Instigator.transform.forward;
        RaycastHit hit;
        
        if(!Physics.Raycast(Instigator.transform.position,blinkDirection,out hit, spellData.BlinkDistance)){
            Debug.DrawRay(Instigator.transform.position, blinkDirection * spellData.BlinkDistance,Color.white,6f);
            Instigator.transform.position = Instigator.transform.position + blinkDirection * spellData.BlinkDistance;
        }
        else{
            Debug.DrawRay(Instigator.transform.position, blinkDirection * spellData.BlinkDistance, Color.red,6f);

            Instigator.transform.position =Instigator.transform.position + ((hit.point - Instigator.transform.position) * onWallHitOffset);
        }
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));

    }
}