using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GAMESTATE { menu,play,pause,victory,gameover}

public class GameManager : MonoBehaviour, IEventHandler
{
    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    GAMESTATE m_State;
    public bool IsPlaying => m_State == GAMESTATE.play;

    int m_Score;
    [SerializeField] int m_ScoreToVictory;
    void SetScore(int newScore)
    {
        m_Score = newScore;
        EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eScore = m_Score, eCountDown = m_Chronos, eWave = m_wave });
    }
    int IncrementScore(int increment)
    {
        SetScore(m_Score + increment);
        return m_Score;
    }

    float m_Chronos;
    void SetChronos(float newCountdown)
    {
        m_Chronos = newCountdown;
        EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eScore = m_Score, eCountDown = m_Chronos, eWave = m_wave });
    }

    int m_wave;
    void SetWave(int wave)
    {
        m_wave = wave;
        EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eScore = m_Score, eCountDown = m_Chronos, eWave = m_wave });
    }

    float IncrementChronos(float increment)
    {
        SetChronos(m_Chronos + increment);
        return m_Chronos;
    }


    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<EnemyHasBeenKillEvent>(EnemyHasBeenKill);
        EventManager.Instance.AddListener<SpawnEnemyEvent>(SpawnEnemy);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<EnemyHasBeenKillEvent>(EnemyHasBeenKill);
        EventManager.Instance.RemoveListener<SpawnEnemyEvent>(SpawnEnemy);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Awake()
    {
        if (!m_Instance) m_Instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
        SetChronos(0);
        SetWave(0);
        MainMenu();
    }

    void Update()
    {
        if(IsPlaying)
        {
            IncrementChronos(Time.deltaTime);
     
        }
    }

    void SetState(GAMESTATE newState)
    {
        m_State = newState;

        switch (m_State)
        {
            case GAMESTATE.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
                break;
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
                break;
            case GAMESTATE.victory:
                EventManager.Instance.Raise(new GameVictoryEvent());
                break;
            case GAMESTATE.gameover:
                EventManager.Instance.Raise(new GameOverEvent());
                break;
        }

    }

    void MainMenu()
    {
        SetState(GAMESTATE.menu);
    }

    void InitGame()
    {
        SetScore(0);
        SetChronos(0);
        SetWave(0);
    }

    void Play()
    {
        InitGame();
        SetState(GAMESTATE.play);
    }

    void Victory()
    {
        SetState(GAMESTATE.victory);
    }
    void GameOver()
    {
        SetState(GAMESTATE.gameover);
    }


    // MenuManager events' callback
    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }

    void ReplayButtonClicked(ReplayButtonClickedEvent e)
    {
        Play();
    }
    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        MainMenu();
    }

    void EnemyHasBeenKill(EnemyHasBeenKillEvent e)
    {
        IncrementScore(1);
    }

    void SpawnEnemy(SpawnEnemyEvent e)
    {
        SetWave(e.eWaveNumber);
    }

}
