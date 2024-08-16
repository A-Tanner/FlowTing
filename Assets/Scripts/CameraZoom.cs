using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    CinemachineComponentBase _componentBase;
    float _deltaZoom;
    [SerializeField] float _zoomSensitivity = 10f;
    [SerializeField] float _minZoom = 5f;
    [SerializeField] float _maxZoom = 20f;

    void Start()
    {
        if(_componentBase == null)
        {
            _componentBase = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
    }
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0) //Zoom
        {
            _deltaZoom = Input.GetAxis("Mouse ScrollWheel") * _zoomSensitivity;   
            if(_componentBase is CinemachineFramingTransposer)
            {
                (_componentBase as CinemachineFramingTransposer).m_CameraDistance -= _deltaZoom;
            }
        }
        if(_componentBase is CinemachineFramingTransposer) //Clamp zoom
        {
            float currentDistance = (_componentBase as CinemachineFramingTransposer).m_CameraDistance;
            (_componentBase as CinemachineFramingTransposer).m_CameraDistance = Mathf.Clamp(currentDistance,_minZoom,_maxZoom);
        }
    }
}
