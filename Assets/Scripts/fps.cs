using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fps : MonoBehaviour
{
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var text = 1 / Time.deltaTime;
        _text.text = text.ToString();
    }
}
