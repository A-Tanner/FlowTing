using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public delegate void OnStart();
    public static OnStart OnStartEvent;
    private float _waitTime = 0.3f;

    public static void StartAction()
    {
        OnStartEvent.Invoke();
    }
    void Update()
    {
        
        if(_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartAction();
        }
    }
}
