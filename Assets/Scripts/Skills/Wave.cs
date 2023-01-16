using UnityEngine;

[System.Serializable]
public class WaveData:SpellData {
    public int Damage => damage;
    [SerializeField] int damage;
    public float WaveRadius => waveRadius;
    [SerializeField] float waveRadius;
    public float WaveKnockbackForce => waveKnockbackForce;
    [SerializeField] float waveKnockbackForce;

}
[CreateAssetMenu(fileName = "Wave", menuName = "ProjectW/Skills/Wave", order = 0)]
public class Wave : Spell
{
    [SerializeField] WaveData spellData;
    public override void Activate(GameObject Instigator)
    {
        if(SpellGlobalData.Instance.WaveData !=null) spellData = SpellGlobalData.Instance.WaveData;
        if(!canCast) return;
        //ОБЯЗАТЕЛЬНО СТАВИТЬ СЛОЙ Enemy на противников
        var colliders = Physics.OverlapSphere(Instigator.transform.position, spellData.WaveRadius,LayerMask.GetMask("Enemy"));
        foreach(Collider collider in colliders){
            Debug.Log(collider.name);
            //Добавить проверку на наличие класса EnemyKnockbackComponent и вызывать метод KnockAway
        }
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));
    }
}