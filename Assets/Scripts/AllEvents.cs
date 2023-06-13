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
	public int eWave { get; set; }
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ReplayButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
public class NextLevelButtonClickedEvent : SDD.Events.Event
{
}
#endregion

#region Player Event

public class EmitPositionEvent : SDD.Events.Event
{
	public Vector3 position;
}

public class PlayerHasBeenHitEvent : SDD.Events.Event
{
    public GameObject ePlayer;
}

public class PlayerHasShootEvent : SDD.Events.Event
{
    
}


#endregion


#region Bonus
public class PotionTriggerEvent : SDD.Events.Event
{
	public float life;
}

public class JetPackTriggerEvent : SDD.Events.Event
{
	public int fuel;
}

public class ShootBonusTriggerEvent : SDD.Events.Event
{
	public float cooldown;
	public float bonus_time;
}

public class JetpackFuelHasBeenUpdatedEvent : SDD.Events.Event
{
	public int eLeftFuel { get; set; }
}

#endregion

#region Enemy Event
public class EnemyHasBeenHitEvent: SDD.Events.Event
{
	public GameObject eEnemy;
	public int damage;
}

public class EnemyHasBeenKillEvent : SDD.Events.Event
{
	
}

public class SpawnEnemyEvent : SDD.Events.Event
{
	public int eNumberOfEnemy;
	public int eWaveNumber;
}
#endregion
