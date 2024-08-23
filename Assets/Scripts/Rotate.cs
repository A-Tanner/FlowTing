using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float _rotateSpeed = 0;
    private void Awake()
    {
        _rotateSpeed = (Random.value* 3f)- 1.5f;
        transform.Rotate(0, Random.value*180, 0);
    }
    private void Update()
    {
        transform.Rotate(0, _rotateSpeed, 0);
    }
}
