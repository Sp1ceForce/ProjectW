using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    [SerializeField] Blink BlinkSkill;

    private void Update() {
        Debug.DrawRay(transform.position,transform.forward * 8,Color.black);
        if(Input.GetKeyDown(KeyCode.Space)){
            BlinkSkill.Activate(gameObject);
        }
    }
}
