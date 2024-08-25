using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float _rotateSpeed = 0;
    private float _scale = 60;
    private void Awake()
    {
        _rotateSpeed = (Random.value* 3f * _scale)- 1.5f * _scale;
        transform.Rotate(0, Random.value*180, 0);
    }
    private void Update()
    {
        transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
    }
}
