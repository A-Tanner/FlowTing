using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class SpinBehavior : MonoBehaviour
{
    [SerializeField] float _baseRotationSpeed = 20f;
    [SerializeField] float _rotationTimeScale = 10f;
    [SerializeField] float _radius = 1f;
    [SerializeField] GameObject _objectPrefab;
    [SerializeField] int _objectCount = 3;
    [SerializeField] float _objectRotationOffset = 0f;

    List<GameObject> _ringObjects = new List<GameObject>(); 
    
    private GameManager _gameManagerReference = null;
    private float _rotationSpeed = 0f;

    private GameObject SetObjectTransform(GameObject obj, int index){
        // x = cos(radian conversion * fraction of circle), z = sin(radian conversion * fraction of circle)

        Vector3 ringCenter = gameObject.transform.position + Vector3.zero;
        obj.transform.position = new Vector3(_radius * (float)Math.Cos(2 * Math.PI * index / _objectCount) + ringCenter.x, ringCenter.y, _radius * (float)Math.Sin(2 * Math.PI * index / _objectCount) + ringCenter.z);
        obj.transform.LookAt(ringCenter);
        obj.transform.Rotate(0, _objectRotationOffset, 0);
        return obj;
    }
    private float TimedRotationFormula(float timeAlive){
        float a = _baseRotationSpeed / (_radius * 2 * (float)Math.PI);
        return a + timeAlive * _rotationTimeScale;
    }
    void Start()
    {
        for(int i = 0; i < _objectCount; i++)
        {
            //Instantiate using parent position and rotation
            GameObject currentObject = Instantiate(_objectPrefab, gameObject.transform);
            _ringObjects.Add(currentObject);
            //Set object position
            SetObjectTransform(currentObject, i);
        }
        if (_gameManagerReference is null)
        {
            _gameManagerReference = FindAnyObjectByType<GameManager>();
        }
    }

    void FixedUpdate()
    {
        _rotationSpeed = TimedRotationFormula(_gameManagerReference.GetTimeAlive());
        gameObject.transform.eulerAngles += new Vector3(0,_rotationSpeed,0);
    }
}
