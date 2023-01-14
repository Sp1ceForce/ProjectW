using UnityEngine;

[System.Serializable]
public class ProjectileData:SpellData {
    public float Damage => damage;
    [SerializeField] float damage;

}
[CreateAssetMenu(fileName = "ProjectileSpell", menuName = "ProjectW/Skills/ProjectileSpell", order = 0)]
public class ProjectileSpell : Spell
{
    
    [SerializeField] ProjectileData spellData;
    public override void Activate(GameObject Instigator)
    {
        //if(spellData == null) spellData = SpellGlobalData.Instance.BlinkData;
        

    }
}