using UnityEngine;

[System.Serializable]
public class MeleeData:SpellData {
    public int Damage => damage;
    [SerializeField] int damage;
    public float AttackRadius => attackRadius;
    [SerializeField] float attackRadius;
    public float AttackKnockbackForce => attackKnockbackForce;
    [SerializeField] float attackKnockbackForce;

}
[CreateAssetMenu(fileName = "MeleeAttack", menuName = "ProjectW/Skills/MeleeAttack", order = 0)]
public class MeleeAttack : Spell
{
    [SerializeField] WaveData spellData;
    public override void Activate(GameObject Instigator)
    {
        if(SpellGlobalData.Instance.WaveData !=null) spellData = SpellGlobalData.Instance.WaveData;
        if(!canCast) return;
        //ОБЯЗАТЕЛЬНО СТАВИТЬ СЛОЙ Enemy на противников
        var colliders = Physics.OverlapSphere(Instigator.transform.position, spellData.WaveRadius, LayerMask.NameToLayer("Default"));
        foreach(Collider collider in colliders){
            
            //Добавить проверку на наличие класса EnemyKnockbackComponent и вызывать метод KnockAway
        }
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));
    }
}