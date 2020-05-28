using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    MainMenu,
    Loading,
    Game,
    GamePause
}

public class GameManager : MonoBehaviour
{
    

    private static GameState currentGameState;

    public static GameState CurrentGameState
    {
        get => currentGameState;
    }

    public IPlayer Player;
    public List<IEnemy> Enemies = new List<IEnemy>();


    private static int coins;
    public static int Coins
    {
        get => coins;
        set
        {
            coins = value;
            Debug.Log("Coins" + coins);
        }
        
    }
    
    public static Action<GameState> GameStateAction;

    public static void SetGameState(GameState state)
    {
        currentGameState = state;
        GameStateAction?.Invoke(state);
    }

    private void Start()
    {
        SetGameState(GameState.Game);
        print(Player);
        print($"{Enemies.Count} - enemies");
    }
}
