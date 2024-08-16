using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PlayerScript.OnDeathEvent += GameOver;
        Debug.Log(PlayerScript.OnDeathEvent);
    }

    public void GameOver()
    {
        PlayerScript.OnDeathEvent -= GameOver;
        SceneManager.LoadScene("Results");
        GameStartButton.OnStartEvent += StartGame;
    }

    public void StartGame()
    {
        GameStartButton.OnStartEvent -= StartGame;
        SceneManager.LoadScene("Gameplay");
        Instantiate(_playerPrefab, new Vector3(0, 5, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
