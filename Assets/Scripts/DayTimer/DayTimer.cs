using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f;
    //[Range(0,1)]
    public float currentTimeOfDay = 0;
    //[HideInspector]
    public float timeMultiplier = 1f;
    public int frameCountUpdate = 100;

    public float startSunRise = 0.15f;
    public float endSunRise = 0.25f;
    public float startSunSet = 0.65f;
    public float endSunSet = 0.75f;

    float sunInitialIntensity;
    float sunColorTemperature;
    float periodSunRise;//endSunRise - startSunRise;
    float periodSunSet; //endSunSet - startSunSet;
    int frameCount = 0;
    float pasTime = 0f;
    



    // Start is called before the first frame update
    void Start()
    {
        sunInitialIntensity = sun.intensity;
        sunColorTemperature = sun.colorTemperature;
        periodSunRise = endSunRise - startSunRise;
        periodSunSet = endSunSet - startSunSet;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount += 1;
        pasTime += Time.deltaTime;
        if (frameCount == frameCountUpdate){
            frameCount = 0;
            //UpdateSun();
            currentTimeOfDay += (pasTime / secondsInFullDay) * timeMultiplier;
            if (currentTimeOfDay >= 1) {currentTimeOfDay = 0;}
            pasTime = 0;
        }
    }

    void UpdateSun() 
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 45);
        float intensityMultiplier = 1;
        float colorTemperature = 0;
        if (currentTimeOfDay <= startSunRise || currentTimeOfDay >= endSunSet){
            intensityMultiplier = 0;
            colorTemperature = 5000f;
        }
        else if (currentTimeOfDay <= endSunRise){
            intensityMultiplier = (currentTimeOfDay - startSunRise)/periodSunRise;
            colorTemperature = (1-((currentTimeOfDay - startSunRise)/periodSunRise)) * 5000f;
        }
        else if (currentTimeOfDay >=startSunSet) {
            intensityMultiplier = 1-((currentTimeOfDay - startSunSet)/periodSunSet);
            colorTemperature = ((currentTimeOfDay - startSunSet)/periodSunSet) * 5000f;
        }
        sun.intensity = sunInitialIntensity * intensityMultiplier;
        sun.colorTemperature = sunColorTemperature - colorTemperature;
    }
}
