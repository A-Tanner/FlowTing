using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public delegate void OnStart();
    public static OnStart OnStartEvent;

    public static void StartAction()
    {
        OnStartEvent.Invoke();
    }
}
