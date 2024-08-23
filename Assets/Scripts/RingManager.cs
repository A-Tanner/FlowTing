using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] float _baseRotationSpeed = 20f;
    [SerializeField] float _rotationTimeScale = 0.01f;
    [SerializeField] float _radiusGap = 1f;
    [SerializeField] int _ringCount;
    [SerializeField] GameObject _ringPrefab = null; //TODO replace this with a direct reference to the scriptable object
    private List<GameObject> _rings = new List<GameObject>(); 
    private GameManager _gameManagerReference = null;

    private int PlatformCountFunction(int index){
        return 3+index;
    }
    private int SpikeCountFunction(int index){
        return 3+index;
    }
    private GameObject SetRingProperties(GameObject ring, int index){
        ring.transform.position = gameObject.transform.position;
        ring.GetComponent<RingBehavior>().Radius = _radiusGap * index;
        ring.GetComponent<RingBehavior>().RotationTimeScale = _rotationTimeScale;
        ring.GetComponent<RingBehavior>().SpikeCount = SpikeCountFunction(index);
        ring.GetComponent<RingBehavior>().PlatformCount = PlatformCountFunction(index);
        ring.GetComponent<RingBehavior>().SpikeRotationSpeed = -20f;
        ring.GetComponent<RingBehavior>().PlatformRotationSpeed = 20f;
        
        return ring;
    }

    void Start()
    {
        for(int i = 0; i < _ringCount; i++)
        {
            GameObject currentRing = Instantiate(_ringPrefab, gameObject.transform);
            SetRingProperties(currentRing, i);
        }
        if (_gameManagerReference is null)
        {
            _gameManagerReference = FindAnyObjectByType<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
