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
    public BlinkData BlinkData {get => blinkData;}
    [SerializeField] BlinkData blinkData;
    public ProjectileData ProjectileData {get => projectileData;}
    [SerializeField] ProjectileData projectileData;
    public WaveData WaveData => waveData;
    [SerializeField] WaveData waveData;
        
}