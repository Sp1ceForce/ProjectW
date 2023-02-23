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
    [SerializeField] AnimationCurve throwingArc;
    [SerializeField] float throwDuration = 1f;
    [SerializeField] float maxHeight = 2.5f;
    [SerializeField] GameObject bombObject;
    [SerializeField] GameObject explosionParticles;
    [Header("Взрыв бомбы")]
    [SerializeField] LayerMask physicsMask;    

    public PoisonousFog PosionFogObject;
    public PullingZone PullingZoneObject;    
}
