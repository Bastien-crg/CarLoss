using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class MenuManager : MonoBehaviour, IEventHandler
{
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_VictoryPanel;
    [SerializeField] GameObject m_GameOverPanel;
    [SerializeField] GameObject m_CreditsPanel;

    public float m_Difficulty1SpawnPeriod;
    public float m_Difficulty2SpawnPeriod;
    public GameObject m_FalsePlayerPrefab;
    public Transform m_FalsePlayerSpawnPos;
    private GameObject FalsePlayer;


    List<GameObject> m_Panels;
    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => item.SetActive(panel == item));
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
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
        m_Panels = new List<GameObject>(
            new GameObject[] { m_MainMenuPanel, m_VictoryPanel, m_GameOverPanel });
    }

    void Start()
    {
        FalsePlayer = Instantiate(m_FalsePlayerPrefab);
        FalsePlayer.transform.position = m_FalsePlayerSpawnPos.position;
        FalsePlayer.transform.rotation = m_FalsePlayerSpawnPos.rotation;

    }

    void SpawnFalsePlayer()
    {
        
    }

    void CleanFalsePlayer()
    {
        FalsePlayer.SetActive(false);
    }

    // GameManager events' callbacks
    void GameMenu(GameMenuEvent e)
    {
        SpawnFalsePlayer();
        OpenPanel(m_MainMenuPanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        CleanFalsePlayer();
        OpenPanel(null);
    }

    void GameVictory(GameVictoryEvent e)
    {
        OpenPanel(m_VictoryPanel);
    }

    void GameOver(GameOverEvent e)
    {
        OpenPanel(m_GameOverPanel);
    }



    // UI events' callbacks
    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }
    public void Difficulty1PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new DifficultyPlayButtonClickedEvent() { difficultySpawningPeriod = m_Difficulty1SpawnPeriod });
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }
    public void Difficulty2PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new DifficultyPlayButtonClickedEvent() { difficultySpawningPeriod = m_Difficulty2SpawnPeriod });
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

    public void CreditsButtonHasBeenClicked()
    {
        CleanFalsePlayer();
        //Ouverture du panel des crédits
        OpenPanel(m_CreditsPanel);
        
        m_CreditsPanel.SetActive(true);
        
    }

    public void RetourButtonHasBeenClicked() {
        // On revient au main menu
        m_CreditsPanel.SetActive(false);
        SpawnFalsePlayer();
        OpenPanel(m_MainMenuPanel);
    }



}
