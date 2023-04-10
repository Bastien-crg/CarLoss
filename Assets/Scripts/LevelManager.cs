using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using System.Linq;


public class LevelManager : MonoBehaviour,IEventHandler
{
    List<Cube> m_Cubes;

    [SerializeField] GameObject m_PlayerPrefab;
    [SerializeField] Transform m_PlayerSpawnPos;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
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
    // Start is called before the first frame update
    void Start()
    {

        GameObject playerGO = Instantiate(m_PlayerPrefab);
        playerGO.transform.position = m_PlayerSpawnPos.position;
        
        
        m_Cubes = GameObject.FindObjectsOfType<Cube>().ToList();
    }

    void CleanBalls()
    {
        GameObject.FindObjectsOfType<Ball>().ToList().ForEach(item => Destroy(item.gameObject));
    }

    void ActivateCubes()
    {
        m_Cubes.ForEach(item => item.gameObject.SetActive(true));
    }

    // GameManager events' callbacks
    void GameMenu(GameMenuEvent e)
    {
        CleanBalls();
    }

    void GamePlay(GamePlayEvent e)
    {
        CleanBalls();
        ActivateCubes();
    }

    void GameVictory(GameVictoryEvent e)
    {
        CleanBalls();
    }

    void GameOver(GameOverEvent e)
    {
        CleanBalls();
    }

    // Ball events' callbacks
    void EnemyHasBeenHit(EnemyHasBeenHitEvent e)
    {
        IDestroyable destroyable = e.eEnemy.GetComponent<IDestroyable>();
        if (null != destroyable)
            destroyable.Kill();
    }
}
