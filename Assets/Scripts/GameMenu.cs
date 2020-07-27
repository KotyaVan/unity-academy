using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);    
        pausePanel.SetActive(false);

        exitButton.onClick.AddListener(OnExitButton);
        pauseButton.onClick.AddListener(delegate {OnPause(true);});
        resumeButton.onClick.AddListener(delegate {OnPause(false);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPause(bool isPause)
    {
        Time.timeScale = isPause ? 0f : 1f;
        
        mainPanel.SetActive(!isPause);
        pausePanel.SetActive(isPause);
    }

    private void OnExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
