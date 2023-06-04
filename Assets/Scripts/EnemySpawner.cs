using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class EnemySpawner : MonoBehaviour, IEventHandler
{

    [SerializeField] GameObject m_EnemyPrefab;
    [SerializeField] Transform m_SpawnerSpawnPos;
    [SerializeField] float m_maxSpawningPeriod;
    float SpawningPeriod;
    float TimeNextSpawn;
    bool isGamePlay;

    // Start is called before the first frame update
    void Start()
    {
        isGamePlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
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

    GameObject SpawnEnemy()
    {
        GameObject newEnemyGO = Instantiate(m_EnemyPrefab);
        newEnemyGO.transform.position = m_SpawnerSpawnPos.position;
        Debug.Log(newEnemyGO.transform.position);
        return newEnemyGO;
    }

    private void FixedUpdate()
    {
        //Debug.Log(isGamePlay);
        if (isGamePlay && Time.time > TimeNextSpawn)
        {
            SpawnEnemy();
            if (SpawningPeriod > 2f)
            {
                SpawningPeriod = SpawningPeriod/2f;
            } else {
                SpawningPeriod = 2f;
            }
            TimeNextSpawn = Time.time + SpawningPeriod;
        }
    }

    void GameMenu(GameMenuEvent e)
    {
        isGamePlay = false;
    }

    void GamePlay(GamePlayEvent e)
    {
        SpawningPeriod = m_maxSpawningPeriod;
        isGamePlay = true;
    }

    void GameVictory(GameVictoryEvent e)
    {
        isGamePlay = false;
    }

    void GameOver(GameOverEvent e)
    {
        isGamePlay = false;
    }
}
