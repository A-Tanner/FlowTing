using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlock : MonoBehaviour
{
    private bool _isTouchingPlayer = true;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _isTouchingPlayer = false;
            transform.DetachChildren();
            FindAnyObjectByType<GameManager>().StartPlaying();
            Destroy(this.gameObject);
        }
    }

    private void LateUpdate()
    {
        if (!_isTouchingPlayer)
        {
        }
    }
}
