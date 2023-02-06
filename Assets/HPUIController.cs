using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HPUIController : MonoBehaviour
{
    Witch hpController;
    TMP_Text HPText;
    private void Awake() {
        HPText = GetComponent<TMP_Text>();
        hpController = FindObjectOfType<Witch>();
        hpController.OnHealthChange+=UpdateHealth;  
    }
    void UpdateHealth(){
        HPText.text = hpController.witchData.CurrentHealth.ToString();   
    }
}
