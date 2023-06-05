using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using System.Linq;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour,IEventHandler
{
    List<Cube> m_Cubes;
    public Image HealthBar;
    private GameObject playerGO;

    [SerializeField] GameObject m_PlayerPrefab;
    [SerializeField] Transform m_PlayerSpawnPos;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        //EventManager.Instance.AddListener<PlayerHasBeenHitEvent>(PlayerHasBeenHit);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        //EventManager.Instance.RemoveListener<PlayerHasBeenHitEvent>(PlayerHasBeenHit);
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
        m_Cubes = GameObject.FindObjectsOfType<Cube>().ToList();
    }

    void CleanBalls()
    {
        GameObject.FindObjectsOfType<Ball>().ToList().ForEach(item => Destroy(item.gameObject));
    }

    void CleanEnemy()
    {
        GameObject.FindObjectsOfType<Enemy>().ToList().ForEach(item => Destroy(item.gameObject));
    }

    void ActivateCubes()
    {
        m_Cubes.ForEach(item => item.gameObject.SetActive(true));
    }

    GameObject PlayerSpawning()
    {
        GameObject playerGO = Instantiate(m_PlayerPrefab);
        playerGO.transform.position = m_PlayerSpawnPos.position;
        Player player = playerGO.GetComponent<Player>();
        player.setHealthBar(HealthBar);
        return playerGO;
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
        ActivateCubes();
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

    

    /*void PlayerHasBeenHit(PlayerHasBeenHitEvent e)
    {
        IDestroyable destroyable = e.ePlayer.GetComponent<IDestroyable>();
        if (null != destroyable)
        destroyable.Damage();
    }*/
    
}
