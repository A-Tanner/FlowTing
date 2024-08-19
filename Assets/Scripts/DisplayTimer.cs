using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    private GameManager _gameManagerReference = null;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if (_gameManagerReference is null)
        {
            _gameManagerReference = FindAnyObjectByType<GameManager>();
            
            if (_gameManagerReference is not null)
            {
                _text.text = "Your Time Alive: " + _gameManagerReference.GetTimeAlive().ToString("n2") + "s";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
