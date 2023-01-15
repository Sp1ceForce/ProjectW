using UnityEngine;

[CreateAssetMenu(fileName = "WitchGlobalData", menuName = "ProjectW/WitchGlobalData", order = 0)]
public class WitchGlobalData : ScriptableObject {
    public static WitchGlobalData Instance {
        get{
            if(instance == null) instance = Resources.Load<WitchGlobalData>("Data/WitchGlobalData");
            return instance; 
        }
    }
    static WitchGlobalData instance;
    public MoveControllerData MoveControllerData => moveControllerData;
    [SerializeField] MoveControllerData moveControllerData;

    public WitchData WitchData => witchData;
    [SerializeField] WitchData witchData;
        
}