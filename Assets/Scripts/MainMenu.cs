using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            GameManager.SetGameState(GameState.MainMenu);
        }

        public void LoadLevel(string level)
        {
            SceneLoader.LoadLevel(level);
        }
    }
}