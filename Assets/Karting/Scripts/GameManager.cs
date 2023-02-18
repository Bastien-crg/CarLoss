using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMESTATE { menu, play, pause, victory, gameover }

public class GameManager : MonoBehaviour, IEventHandler
{

    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    GAMESTATE m_State;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<ControlsButtonClickedEvent>(ControlsButtonClicked);
        EventManager.Instance.AddListener<CloseControlsButtonClickedEvent>(CloseControlsButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<ControlsButtonClickedEvent>(ControlsButtonClicked);
        EventManager.Instance.RemoveListener<CloseControlsButtonClickedEvent>(CloseControlsButtonClicked);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
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


    void Play()
    {
        SetState(GAMESTATE.play);
    }

    void MainMenu()
    {
        SetState(GAMESTATE.menu);
    }

    void OpenControls()
    {
        EventManager.Instance.Raise(new OpenControlsEvent());
    }
    void CloseControls()
    {
        EventManager.Instance.Raise(new CloseControlsEvent());
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
    void ControlsButtonClicked(ControlsButtonClickedEvent e)
    {
        OpenControls();
    }
    void CloseControlsButtonClicked(CloseControlsButtonClickedEvent e)
    {
        CloseControls();
    }
}
