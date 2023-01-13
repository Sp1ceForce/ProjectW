using UnityEngine;

[CreateAssetMenu(fileName = "SpellGlobalData", menuName = "ProjectW/SpellGlobalData", order = 1)]
public class SpellGlobalData : ScriptableObject {
    public static SpellGlobalData Instance {
        get{
            if(instance == null) instance = Resources.Load<SpellGlobalData>("SpellGlobalData");
            return instance; 
        }
    }
    static SpellGlobalData instance;
    public BlinkData BlinkData {get => blinkData;}
    [SerializeField] BlinkData blinkData;
        
}