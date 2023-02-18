using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IEventHandler
{

    private static MenuManager m_Instance;
    public GameObject objectToDisplay;
    public static MenuManager Instance { get { return m_Instance; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        EventManager.Instance.AddListener<OpenControlsEvent>(OpenControls);
        EventManager.Instance.AddListener<CloseControlsEvent>(CloseControls);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        EventManager.Instance.RemoveListener<OpenControlsEvent>(OpenControls);
        EventManager.Instance.RemoveListener<CloseControlsEvent>(CloseControls);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
    }

    // GameManager events' callbacks
    void GameMenu(GameMenuEvent e)
    {
        SceneManager.LoadSceneAsync("IntroMenu");
    }

    void GamePlay(GamePlayEvent e)
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    void GameVictory(GameVictoryEvent e)
    {
        SceneManager.LoadSceneAsync("WinScene");
    }

    void GameOver(GameOverEvent e)
    {
        SceneManager.LoadSceneAsync("LoseScene");
    }

    void OpenControls(OpenControlsEvent e)
    {
        DisplayPanel(objectToDisplay);
    }

    void CloseControls(CloseControlsEvent e)
    {
        ClosePanel(objectToDisplay);
    }


    // UI events' callbacks
    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }
    public void ReplayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ReplayButtonClickedEvent());
    }
    public void MainMenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }
    public void ControlsButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ControlsButtonClickedEvent());
    }
    public void CloseControlsButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new CloseControlsButtonClickedEvent());
    }
}
