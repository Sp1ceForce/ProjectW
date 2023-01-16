using UnityEngine;

[System.Serializable]
public class MeleeData:SpellData {
    public int Damage => damage;
    [Header("Урон атаки")]
    [SerializeField] int damage;
    public float AttackRadius => attackRadius;
    [Header("Дальность атаки")]
    [SerializeField] float attackRadius;
    public float AttackAngle => attackAngle;
    [Header("Угол в котором наносится удар")]
    [Range(0,360)]
    [SerializeField] float attackAngle;
    public float AttackKnockbackForce => attackKnockbackForce;
    [Header("Сила отбрасывания")]
    [SerializeField] float attackKnockbackForce;

}
[CreateAssetMenu(fileName = "MeleeAttack", menuName = "ProjectW/Skills/MeleeAttack", order = 0)]
public class MeleeAttack : Spell
{
    [SerializeField] MeleeData spellData;
    public override void Activate(GameObject Instigator)
    {
        if(SpellGlobalData.Instance.MeleeData !=null) spellData = SpellGlobalData.Instance.MeleeData;
        if(!canCast) return;
        //ОБЯЗАТЕЛЬНО СТАВИТЬ СЛОЙ Enemy на противников
        var colliders = Physics.OverlapSphere(Instigator.transform.position, spellData.AttackRadius,LayerMask.GetMask("Enemy"));
        foreach(Collider collider in colliders){
            Transform target = collider.transform;
            Vector3 dirToTarget = (target.position - Instigator.transform.position).normalized;
            if(Vector3.Angle(Instigator.transform.forward,dirToTarget) < spellData.AttackAngle / 2)
            {
                Debug.Log(collider.name);
                //Тут нужно будет делать проверки наличия компонентов и после этого вызывать методы для отбрасывания и получения урона
            }  
        }
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));
    }
}