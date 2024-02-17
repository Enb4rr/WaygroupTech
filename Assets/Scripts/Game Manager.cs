using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void GameEvents();

    public GameEvents OnStart;
    public GameEvents OnLost;
    public GameEvents OnWin;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        instance = this;
    }

    private void StartGame()
    {
        OnStart?.Invoke();
    }

    private void LostGame()
    {
        OnLost?.Invoke();
    }

    private void WinGame()
    {
        OnWin?.Invoke();
    }
}
