using UnityEngine;

[CreateAssetMenu(fileName = "SpellGlobalData", menuName = "ProjectW/SpellGlobalData", order = 1)]
public class SpellGlobalData : ScriptableObject {
    public static SpellGlobalData Instance {
        get{
            if(instance == null) instance = (SpellGlobalData)Resources.Load("Data/SpellGlobalData");
            return instance; 
        }
    }
    static SpellGlobalData instance;
    private void OnEnable() {
        instance = (SpellGlobalData)Resources.Load("Data/SpellGlobalData");
    }
    public BlinkData BlinkData {get => blinkData;}
    [SerializeField] BlinkData blinkData;
    public ProjectileData ProjectileData {get => projectileData;}
    [SerializeField] ProjectileData projectileData;
    public WaveData WaveData => waveData;
    [SerializeField] WaveData waveData;
    public MeleeData MeleeData => meleeData;
    [SerializeField] MeleeData meleeData;
        
}