using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    bool isGamePlay;
    float TimeNextSpawn;
    public float SpawningPeriod;
    int numberOfEnemy;
    int previousNumberOfEnemy;
    int wave_number;
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
    // Start is called before the first frame update
    void Start()
    {
        isGamePlay = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isGamePlay);
        if (isGamePlay && Time.time > TimeNextSpawn)
        {
            wave_number++;
            EventManager.Instance.Raise(new SpawnEnemyEvent() { eNumberOfEnemy = previousNumberOfEnemy + numberOfEnemy, eWaveNumber = wave_number});
            int temp = previousNumberOfEnemy;
            previousNumberOfEnemy = numberOfEnemy;
            numberOfEnemy += temp;
            TimeNextSpawn = Time.time + SpawningPeriod;
        }
    }

    void GameMenu(GameMenuEvent e)
    {
        isGamePlay = false;
    }

    void GamePlay(GamePlayEvent e)
    {
        isGamePlay = true;
        numberOfEnemy = 1;
        previousNumberOfEnemy = 0;
        wave_number = 0;
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
