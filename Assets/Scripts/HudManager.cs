using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.UI;
using TMPro;


public class HudManager : MonoBehaviour,IEventHandler
{
    [SerializeField] TMP_Text m_ScoreValue;
    [SerializeField] TMP_Text m_TimeValue;

    void SetStatisticsTexts(int score, float time)
    {
        m_ScoreValue.text = score.ToString();
        m_TimeValue.text = time.ToString("N01");
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
    }

    // GameManager events' callbaks
    void GameStatisticsChanged(GameStatisticsChangedEvent e)
    {
        SetStatisticsTexts(e.eScore, e.eCountDown);
    }
}
