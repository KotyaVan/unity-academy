using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_Stick;
    [SerializeField] private StickHeroPlayer m_Player;
    [SerializeField] private StickHeroPlatform m_BasePlatform;

    private int counter; //это счетчик платформ
    private int score;
    private List<StickHeroPlatform> platforms = new List<StickHeroPlatform>();

    private enum EGameState
    {
        Wait,
        Scaling,
        Rotate,
        Movement,
        Defeate
    }

    private EGameState currentGameState;

    // Use this for initialization
    private void Start()
    {
        currentGameState = EGameState.Wait;
        counter = 0;

        platforms.Add(m_BasePlatform);
        GeneratePlatform();
        
        m_Stick.ResetStick(platforms[0].GetStickPosition());
    }


    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        //нужна ли реакция на нажитие кнопки мыши
        switch (currentGameState)
        {
            //если не нажата кнопка старт
            case EGameState.Wait:
                currentGameState = EGameState.Scaling;
                m_Stick.StartScaling();
                break;

            //стик увеличивается - прерываем увеличением и запускаем поворот
            case EGameState.Scaling:
                currentGameState = EGameState.Rotate;
                m_Stick.StopScaling();
                break;

            //ничего не делать
            case EGameState.Rotate:
                break;

            //ничего не делать
            case EGameState.Movement:
                break;

            //перезапускаем игру
            case EGameState.Defeate:
                print("Game restarted");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public void StopStickScale()
    {
        currentGameState = EGameState.Rotate;
        m_Stick.StartRotate();
    }

    public void StopStickRotate()
    {
        currentGameState = EGameState.Movement;
    }

    public void StartPlayerMovement(float lenght)
    {
        currentGameState = EGameState.Movement;
        StickHeroPlatform nextPlatform = platforms[counter + 1];
        //находим минимальную длину стика для успешного перехода
        float targetLenght = nextPlatform.transform.position.x - m_Stick.transform.position.x;//от стика до платформы
        float platformSize = nextPlatform.GetPlatformSize();//размер след платформы
        float min = targetLenght - platformSize * 0.5f;
        // min -= m_Player.transform.localScale.x; //i think it's bad idea

        //находим максимальную длину стика для успешного перехода
        float max = targetLenght + platformSize * 0.5f;

        //при успехе переходим в центр платформы, иначе падаем
        if (lenght < min || lenght > max)
        {
            float targetPosition = m_Stick.transform.position.x + lenght + m_Player.transform.localScale.x * 0.5f;
            m_Player.StartMovement(targetPosition, true);
        }
        else
        {
            float targetPosition = nextPlatform.transform.position.x;
            m_Player.StartMovement(targetPosition, false);
            IncrementScore();
            GeneratePlatform();
        }
    }

    public void StopPlayerMovement()
    {
        currentGameState = EGameState.Wait;
        counter++;
        m_Stick.ResetStick(platforms[counter].GetStickPosition());
    }

    public void ShowScores()
    {
        currentGameState = EGameState.Defeate;
        print("Game Over, your score " + score);
    }

    private void IncrementScore()
    {
        score++;
    }

    private void GeneratePlatform()
    {
        var component = Instantiate(m_BasePlatform);
        var lastPlatformPosition = platforms[platforms.Count - 1].transform.position;
        var lastPlatformWidth = platforms[platforms.Count - 1].transform.localScale.x;
        component.transform.position = new Vector3(lastPlatformPosition.x + Random.Range(lastPlatformWidth / 2 + 1.5f, lastPlatformWidth / 2 + 4f), lastPlatformPosition.y, lastPlatformPosition.z);
        platforms.Add(component);
    }
}