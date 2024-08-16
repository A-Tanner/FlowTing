using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class SpinBehavior : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1f;
    [SerializeField] float _radius = 1f;
    [SerializeField] GameObject _objectPrefab;
    [SerializeField] int _objectCount = 3;
    List<GameObject> _ringObjects = new List<GameObject>(); 
    
    private GameObject SetObjectTransform(GameObject obj, int index){
        // x = cos(radian conversion * fraction of circle), z = sin(radian conversion * fraction of circle)

        Vector3 ringCenter = gameObject.transform.position + Vector3.zero;
        obj.transform.position = new Vector3(_radius * (float)Math.Cos(2 * Math.PI * index / _objectCount), 0, _radius * (float)Math.Sin(2 * Math.PI * index / _objectCount));
        obj.transform.LookAt(ringCenter);
        return obj;
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
    }

    void Update()
    {
        gameObject.transform.eulerAngles += new Vector3(0,_rotationSpeed/(_radius*2*(float)Math.PI),0);
    }
}
