using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hourHand;
    //public Transform minuteHand;

    float hoursToDegrees = 360f / 24f;
    //float minutesToDegrees = 360f / 60f;
    DayTimer dayTimer;

    void Awake() {
        dayTimer = GameObject.Find("DayTimer").GetComponent<DayTimer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentHour = 24 * dayTimer.currentTimeOfDay;
        //float currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));

        hourHand.localRotation = Quaternion.Euler(0, 0, - currentHour * hoursToDegrees);
       // minuteHand.localRotation = Quaternion.Euler(0, 0, - currentMinute * minutesToDegrees);
    }
}
