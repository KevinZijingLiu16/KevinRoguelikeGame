using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Target Frame Rate Setting")]
    [SerializeField] private int targetFrameRate = 60;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        SetGameState(GameState.MENU);
    }

 
    public void SetGameState(GameState gamestate)
    { 
        IEnumerable<IGameStateListener> gameStateListeners = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IGameStateListener>();

        foreach (IGameStateListener gameStatelistener in gameStateListeners)
        {
            gameStatelistener.GameStateChangedCallback(gamestate);
        }



    }

    public void ManageGameover()
    {
     
       
            SceneManager.LoadScene(0);
     
    }

    public void StartGame()
    {
        SetGameState(GameState.GAME);
    }
    public void StartWeaponSelection()
    {
        SetGameState(GameState.WEAPONSELECTION);
    }
    public void StartShop()
    {
       SetGameState(GameState.SHOP);
    }


    public void WaveCompletedCallback()
    { 
        if(Player.instance.HasLeveledUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
           SetGameState(GameState.SHOP);
        }
    }
}

public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}