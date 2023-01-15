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

        Vector2 inputVector = Instigator.GetComponent<MoveController>().InputVector;
        Vector3 blinkDirection = new Vector3(inputVector.x,0,inputVector.y);
        RaycastHit hit;
        
        if(!Physics.Raycast(Instigator.transform.position,blinkDirection,out hit, spellData.BlinkDistance)){
            Instigator.transform.position = Instigator.transform.position + blinkDirection * spellData.BlinkDistance;
        }
        else{
            Instigator.transform.position =Instigator.transform.position + ((hit.point - Instigator.transform.position) * onWallHitOffset);
        }
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));

    }
}