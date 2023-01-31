using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class SpellData {
    public float SpellCooldown => cooldown;
    [SerializeField] float cooldown;

} 

public enum SpellType {
    ST_SKILLSHOT,
    ST_AOE,
    ST_NO_OUTLINE
}
public abstract class Spell : ScriptableObject, ISpellActivate {
    public float TimeLeft {get;protected set;}
    
    public bool CanCast =>canCast;
    [SerializeField] protected bool canCast = true;
    public GameObject VFX;
    public Sprite SpellOutline;
    public SpellType SpellType;
    public abstract void Activate(GameObject Instigator);
    private void OnEnable() {
        canCast = true;
    }
    public virtual IEnumerator StartCooldown(float cooldownTime){
        TimeLeft = cooldownTime;
            WaitForSeconds timeStep = new WaitForSeconds(0.1f);
            while(TimeLeft>0){
                yield return timeStep;
                TimeLeft = TimeLeft - 0.1f;
            }
            canCast= true;
    }
}