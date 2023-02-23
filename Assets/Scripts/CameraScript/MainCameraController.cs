using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainVCam;

    [Header("Вращение камеры")]
    [SerializeField] float rotationSpeed = 3f;
    [Header("Приближение\\Отдаление камеры")]
    [SerializeField] AnimationCurve fovCurve;
    [SerializeField] float maxFov = 80;
    [SerializeField] float minFov = 50;
    [SerializeField] float maxDistance = 12;
    [SerializeField] float minDistance = 6;
    [SerializeField] float zoomSpeed = 0.5f;
    bool isRotating = false;
    CinemachineComponentBase componentBase;
    void Start()
    {
        isRotating = false;
        componentBase = mainVCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        SetStartFovAndDistance();
    }
    void SetStartFovAndDistance(){
          if (componentBase is CinemachineFramingTransposer)
            {

            float distance =(componentBase as CinemachineFramingTransposer).m_CameraDistance;
            distance = Math.Clamp(distance,minDistance,maxDistance);
            float linearDistance = (distance-minDistance)/(maxDistance-minDistance);
            float linearFovModifier = fovCurve.Evaluate(linearDistance);
            mainVCam.m_Lens.FieldOfView = minFov + (maxFov - minFov) * linearFovModifier; 
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = distance;
            }
    }
    public void HandleMiddleMouseButtonPress(InputAction.CallbackContext obj){
        switch(obj.phase){
            case InputActionPhase.Started:
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
            break;
            case InputActionPhase.Canceled:
            Cursor.lockState = CursorLockMode.None;
            isRotating = false;
            break;
        }
    }
      public void HandleMiddleMouseButtonScroll(InputAction.CallbackContext obj){
        if(obj.started){
            if (componentBase is CinemachineFramingTransposer)
            {
            float directionModifier = obj.ReadValue<float>();
            float newDistance =(componentBase as CinemachineFramingTransposer).m_CameraDistance + directionModifier * zoomSpeed;
            newDistance = Math.Clamp(newDistance,minDistance,maxDistance);
            float linearDistance = (newDistance-minDistance)/(maxDistance-minDistance);
            float linearFovModifier = fovCurve.Evaluate(linearDistance);
            mainVCam.m_Lens.FieldOfView = minFov + (maxFov - minFov) * linearFovModifier; 
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = newDistance;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isRotating){
            RotateCamera();
        }
    }
    void RotateCamera(){
        float newAngle = Input.GetAxis("Mouse X") * rotationSpeed;
        Vector3 mainRotation = mainVCam.transform.rotation.eulerAngles;
        mainRotation.y += newAngle;
        mainVCam.transform.rotation = Quaternion.Euler(mainRotation);
    }
}
