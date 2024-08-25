using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpinBehavior : MonoBehaviour
{
    [SerializeField] private float _baseRotationSpeed = 20f;
    [SerializeField] private float _rotationTimeScale = 10f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _objectCount = 3;
    [SerializeField] private float _objectRotationOffset = 0f;

    private List<GameObject> _ringObjects = new List<GameObject>(); 
    
    private GameManager _gameManagerReference = null;
    private float _rotationSpeed = 0f;
    private bool _initialized = false;

    public float BaseRotationSpeed
    {
        get { return _baseRotationSpeed; }
        set { _baseRotationSpeed = value; }
    }

    public float RotationTimeScale
    {
        get { return _rotationTimeScale; }
        set { _rotationTimeScale = value; }
    }

    public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

    public GameObject ObjectPrefab
    {
        get { return _objectPrefab; }
        set { _objectPrefab = value; }
    }

    public int ObjectCount
    {
        get { return _objectCount; }
        set { _objectCount = value; }
    }

    public float ObjectRotationOffset
    {
        get { return _objectRotationOffset; }
        set { _objectRotationOffset = value; }
    }

    private GameObject SetObjectTransform(GameObject obj, int index)
    {
        // x = cos(radian conversion * fraction of circle), z = sin(radian conversion * fraction of circle)

        Vector3 ringCenter = gameObject.transform.position + Vector3.zero;
        obj.transform.position = new Vector3(_radius * (float)Math.Cos(2 * Math.PI * index / _objectCount) + ringCenter.x, ringCenter.y, _radius * (float)Math.Sin(2 * Math.PI * index / _objectCount) + ringCenter.z);
        obj.transform.LookAt(ringCenter);
        obj.transform.Rotate(0, _objectRotationOffset, 0);
        return obj;
    }

    private float TimedRotationFormula(float timeAlive)
    {
        float a = _baseRotationSpeed / (_radius * 2 * (float)Math.PI);
        return a + timeAlive * _rotationTimeScale;
    }

    public void Init()
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
        _initialized = true;
    }

    void FixedUpdate()
    {
        if (_initialized)
        {
            _rotationSpeed = TimedRotationFormula(_gameManagerReference.GetTimeAlive());
            gameObject.transform.eulerAngles += new Vector3(0,_rotationSpeed,0);
            // for(int i = 0; i < _ringObjects.Count; i++)
            // {
            //     SetObjectTransform(_ringObjects[i], i);
            // }
        }
    }
}
