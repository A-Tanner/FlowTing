using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RingBehavior : MonoBehaviour
{
    /*
    *   This class manages the pieces that make up a Ring.
    *   The pieces of a ring are: spikes, platforms, and maybe shader stuff?
    */

    private float _spikeRotationSpeed = -20f;
    private float _platformRotationSpeed = 20f;

    private float _rotationTimeScale = 0.01f;
    private float _radius = 10f;

    [SerializeField] private int _spikeCount = 3;
    [SerializeField] private int _platformCount = 3;

    public float SpikeRotationSpeed
    {
        get { return _spikeRotationSpeed; }
        set { _spikeRotationSpeed = value; }
    }

    public float PlatformRotationSpeed
    {
        get { return _platformRotationSpeed; }
        set { _platformRotationSpeed = value; }
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

    public int SpikeCount
    {
        get { return _spikeCount; }
        set { _spikeCount = value; }
    }

    public int PlatformCount
    {
        get { return _platformCount; }
        set { _platformCount = value; }
    }
    [SerializeField] private GameObject _spikeRingPrefab = null; //TODO hardcode it
    [SerializeField] private GameObject _platformRingPrefab = null; //TODO hardcode it
    [SerializeField] private float _spikeRingRadiusOffset = -3f; //hardcoded because the spike object has a different center than the platform object.
    [SerializeField] private float _spikeRingYOffset = 1f;

    private GameObject _spikeRing = null;
    private GameObject _platformRing = null;
    
    private SpinBehavior ApplySpinProperties(SpinBehavior ringLike, float radiusOffset = 0f){
        _spikeRing.transform.position = gameObject.transform.position + new Vector3(0, _spikeRingYOffset, 0);
        ringLike.Radius = _radius + radiusOffset;
        ringLike.RotationTimeScale = _rotationTimeScale;

        return ringLike;
    }
    void Start()
    {
        _spikeRing = Instantiate(_spikeRingPrefab, gameObject.transform);
        _platformRing = Instantiate(_platformRingPrefab, gameObject.transform);
        _spikeRing.transform.parent = gameObject.transform;
        _platformRing.transform.parent = gameObject.transform;
        
        


        //begin setting properties
        SpinBehavior _spikeRingBehaviour = _spikeRing.GetComponent<SpinBehavior>();
        SpinBehavior _platformRingBehaviour = _platformRing.GetComponent<SpinBehavior>();
        ApplySpinProperties(_spikeRingBehaviour, _spikeRingRadiusOffset);
        ApplySpinProperties(_platformRingBehaviour);
        _spikeRingBehaviour.BaseRotationSpeed = _spikeRotationSpeed;
        _platformRingBehaviour.BaseRotationSpeed = _platformRotationSpeed;
        _spikeRingBehaviour.ObjectCount = _spikeCount;
        _platformRingBehaviour.ObjectCount = _platformCount;

        //initialize rings
        _spikeRingBehaviour.Init();
        _platformRingBehaviour.Init();

    }
    void FixedUpdate()
    {
        // //update rings every frame in case we want cool dynamic updating
        // SpinBehavior _spikeRingBehaviour = _spikeRing.GetComponent<SpinBehavior>();
        // SpinBehavior _platformRingBehaviour = _platformRing.GetComponent<SpinBehavior>();
        // ApplySpinProperties(_spikeRingBehaviour, _spikeRingRadiusOffset);
        // ApplySpinProperties(_platformRingBehaviour);
    }
}
