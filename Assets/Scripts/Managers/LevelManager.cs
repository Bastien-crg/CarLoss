using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using System.Linq;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour,IEventHandler
{
    private int EnemyKilledCounter;
    public Image HealthBar;
    public Image FuelBar;
    private GameObject playerGO;

    [SerializeField] GameObject m_PlayerPrefab;
    [SerializeField] Transform m_PlayerSpawnPos;
    [SerializeField] GameObject m_BonusShootPrefab;
    [SerializeField] Transform m_BonusShootSpawnPos;
    [SerializeField] GameObject m_SpaceShipPrefab;
    [SerializeField] Transform m_SpaceShipSpawnPos;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        EventManager.Instance.AddListener<EnemyHasBeenKillEvent>(EnemyHasBeenKill);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        EventManager.Instance.RemoveListener<EnemyHasBeenKillEvent>(EnemyHasBeenKill);
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
    }

    void CleanBalls()
    {
        GameObject.FindObjectsOfType<Ball>().ToList().ForEach(item => Destroy(item.gameObject));
    }

    void CleanEnemy()
    {
        GameObject.FindObjectsOfType<Enemy>().ToList().ForEach(item => Destroy(item.gameObject));
    }

    GameObject PlayerSpawning()
    {
        GameObject playerGO = Instantiate(m_PlayerPrefab);
        playerGO.transform.position = m_PlayerSpawnPos.position;
        Player player = playerGO.GetComponent<Player>();
        player.setHealthBar(HealthBar);
        player.setFuelhBar(FuelBar);
        return playerGO;
    }

    void SpaceShipSpawning()
    {
        GameObject playerGO = Instantiate(m_SpaceShipPrefab);
        playerGO.transform.position = m_SpaceShipSpawnPos.position;
    }

    // GameManager events' callbacks
    void GameMenu(GameMenuEvent e)
    {
        CleanBalls();
        CleanEnemy();
    }

    void GamePlay(GamePlayEvent e)
    {
        Cursor.lockState = CursorLockMode.Locked;
        CleanBalls();
        CleanEnemy();
        SpaceShipSpawning();
        playerGO = PlayerSpawning();
    }

    void GameVictory(GameVictoryEvent e)
    {
        Cursor.lockState = CursorLockMode.None;
        CleanBalls();
        CleanEnemy();
    }

    void GameOver(GameOverEvent e)
    {
        CleanBalls();
        CleanEnemy();
        Destroy(playerGO);
        Cursor.lockState = CursorLockMode.None;
    }

    // Ball events' callbacks
    void EnemyHasBeenHit(EnemyHasBeenHitEvent e)
    {
        IDestroyable destroyable = e.eEnemy.GetComponent<IDestroyable>();
        if (null != destroyable)
            destroyable.Damage(e.damage);
    }

    void EnemyHasBeenKill(EnemyHasBeenKillEvent e)
    {
        EnemyKilledCounter++;
        if (EnemyKilledCounter % 25 == 0)
        {
            GameObject playerGO = Instantiate(m_BonusShootPrefab);
            playerGO.transform.position = m_BonusShootSpawnPos.position;
        }
    }

}
