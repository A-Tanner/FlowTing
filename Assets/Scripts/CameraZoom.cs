using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;
    float deltaZoom;
    [SerializeField] float zoomSensitivity = 10f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 20f;

    void Start()
    {
        if(componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
    }
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0) //Zoom
        {
            deltaZoom = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;   
            if(componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= deltaZoom;
            }
        }
        if(componentBase is CinemachineFramingTransposer) //Clamp zoom
        {
            float currentDistance = (componentBase as CinemachineFramingTransposer).m_CameraDistance;
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = Mathf.Clamp(currentDistance,minZoom,maxZoom);
        }
    }
}
