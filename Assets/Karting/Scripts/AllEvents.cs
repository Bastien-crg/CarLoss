using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GameVictoryEvent : SDD.Events.Event
{
}
public class GameStatisticsChangedEvent : SDD.Events.Event
{
	public int eScore { get; set; }
	public float eCountDown { get; set; }
}
public class OpenControlsEvent : SDD.Events.Event
{
}
public class CloseControlsEvent : SDD.Events.Event
{
}
#endregion

#region MenuManager Events
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ReplayButtonClickedEvent : SDD.Events.Event
{
}
public class ControlsButtonClickedEvent : SDD.Events.Event
{
}
public class CloseControlsButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
#endregion


#region Enemy Event
public class EnemyHasBeenHitEvent: SDD.Events.Event
{
	public GameObject eEnemy;
}
#endregion
