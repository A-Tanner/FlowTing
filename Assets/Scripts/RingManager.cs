using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] float _baseSpikeRotationSpeed = -20f; // how fast it is without the time bonus
    [SerializeField] float _basePlatformRotationSpeed = 20f; 
    [SerializeField] float _rotationTimeScale = 0.01f;
    [SerializeField] float _radiusGap = 1f;
    [SerializeField] float _innermostRadius = 10f;
    [SerializeField] int _ringCount = 3;
    [SerializeField] GameObject _ringPrefab = null; //TODO replace this with a direct reference to the scriptable object
    private List<GameObject> _rings = new List<GameObject>(); 
    private GameManager _gameManagerReference = null;

    private int PlatformCountFunction(int index){
        return 3+index;
    }
    private int SpikeCountFunction(int index){
        return 3+index;
    }
    private float RadiusFunction(int index){
        return _innermostRadius+index*_radiusGap;
    }
    private GameObject ApplyRingProperties(GameObject ring, int index){
        RingBehavior ringScript = ring.GetComponent<RingBehavior>();

        ring.transform.position = gameObject.transform.position;

        ringScript.Radius = RadiusFunction(index);
        ringScript.RotationTimeScale = _rotationTimeScale;
        ringScript.SpikeCount = SpikeCountFunction(index);
        ringScript.PlatformCount = PlatformCountFunction(index);
        ringScript.SpikeRotationSpeed = _baseSpikeRotationSpeed;
        ringScript.PlatformRotationSpeed = _basePlatformRotationSpeed;
        
        return ring;
    }

    void Start()
    {
        for(int i = 0; i < _ringCount; i++)
        {
            GameObject currentRing = Instantiate(_ringPrefab, gameObject.transform);
            ApplyRingProperties(currentRing, i);
        }
        if (_gameManagerReference is null)
        {
            _gameManagerReference = FindAnyObjectByType<GameManager>();
        }
    }
    void FixedUpdate()
    {
        
    }
}
