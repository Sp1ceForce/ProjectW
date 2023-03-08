using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BombGlobalData", menuName = "ProjectW/GlobalData/BombGlobalData", order = 0)]
public class BombGlobalData : ScriptableObject {
    public static BombGlobalData Instance {
        get{
            if(instance == null) instance = Resources.Load<BombGlobalData>("Data/BombGlobalData");
            return instance; 
        }
    }
    
    static BombGlobalData instance;
    
    [Header("Полёт бомбы и эффекты")]
    public AnimationCurve ThrowingArc;
    public float ThrowDuration = 1f;
    public float MaxHeight = 2.5f;
    public GameObject BombObject;
    public GameObject ExplosionParticles;
    public GameObject FreezeParticles;
    public LayerMask RaycastMask;  

    [Header("Взрыв бомбы")]
    public LayerMask PhysicsMask;    
    public float ExplosionTime = 0.5f;
    public PoisonousFog PosionFogObject;
    public PullingZone PullingZoneObject;    
}
