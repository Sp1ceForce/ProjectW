using UnityEngine;

[System.Serializable]
public class ProjectileData:SpellData {
    public int Damage => damage;
    [SerializeField] int damage;
    public float ProjectileSpeed => projectileSpeed;
    [SerializeField] float projectileSpeed;
    public float ProjectileLifetime => projectileLifetime;
    [SerializeField] float projectileLifetime;

}
[CreateAssetMenu(fileName = "ProjectileSpell", menuName = "ProjectW/Skills/ProjectileSpell", order = 0)]
public class ProjectileSpell : Spell
{
    [SerializeField] ProjectileScript ProjectileObject;
    [SerializeField] ProjectileData spellData;
    //Мне не особо нравится идея пустого объекта который является начальной точкой для запуска снаряда, поэтому попробую просто Forward вектор игрока умножать на forwardOffset
    [SerializeField] float forwardOffset =1.1f;

    public override void Activate(GameObject Instigator)
    {

        if(SpellGlobalData.Instance.ProjectileData !=null) spellData = SpellGlobalData.Instance.ProjectileData;
        if(!canCast) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        Vector3 projectileSpawnPoint =Instigator.transform.position + Instigator.transform.forward * forwardOffset;
        Instantiate(ProjectileObject,projectileSpawnPoint,Instigator.transform.rotation).Init(spellData.ProjectileSpeed,spellData.Damage,spellData.ProjectileLifetime);
        canCast = false;
        Instigator.GetComponent<SkillsController>().StartCoroutine(StartCooldown(spellData.SpellCooldown));
    }
}