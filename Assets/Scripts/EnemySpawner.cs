using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class EnemySpawner : MonoBehaviour, IEventHandler
{

    [SerializeField] GameObject m_EnemyPrefab;
    [SerializeField] Transform m_SpawnerSpawnPos;
    

    // Start is called before the first frame update
    void Start()
    {

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
        EventManager.Instance.AddListener<SpawnEnemyEvent>(SpawnEnemy);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<SpawnEnemyEvent>(SpawnEnemy);
    }


    void SpawnEnemy(SpawnEnemyEvent e)
    {
        StartCoroutine(SpawningCoroutine(e.eNumberOfEnemy));
    }

    IEnumerator SpawningCoroutine(int nbOfEnemy)
    {
        for (int i = 0; i < nbOfEnemy; i++)
        {
            GameObject newEnemyGO = Instantiate(m_EnemyPrefab);
            newEnemyGO.transform.position = m_SpawnerSpawnPos.position;
            Debug.Log(newEnemyGO.transform.position);
            yield return new WaitForSeconds(1.0f);
        }

    }
}
