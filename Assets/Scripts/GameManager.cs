using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    private float _timeAliveInSeconds = 0;
    private bool _isPlaying = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PlayerScript.OnDeathEvent += GameOver;
        Debug.Log(PlayerScript.OnDeathEvent);
    }

    private void Update()
    {
       if (_isPlaying)
        {
            _timeAliveInSeconds += Time.deltaTime;
        }
    }

    public void GameOver()
    {
        PlayerScript.OnDeathEvent -= GameOver;
        SceneManager.LoadScene("Results");
        _isPlaying = false;
        GameStartButton.OnStartEvent += StartGame;
    }

    public void StartGame()
    {
        GameStartButton.OnStartEvent -= StartGame;
        SceneManager.LoadScene("Gameplay");
        Instantiate(_playerPrefab, new Vector3(0, 5, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    public float GetTimeAlive()
    {
        return _timeAliveInSeconds;
    }

    public void StartPlaying()
    {
        _isPlaying = true;
    }
}
