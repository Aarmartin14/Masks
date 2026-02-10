using UnityEngine;
using System.Collections.Generic;

public class RoomTickTest : MonoBehaviour
{
	private RoomTickSystem _tickSystem;
	private CultState _cult;
	private ChurchState _church;

    void Start()
    {
        _tickSystem = new RoomTickSystem();

		var balance = Definitions.Lib.balance;
		_cult = new CultState(balance.startingGold, balance.startingFavor, balance.startingGodHealth, balance.startingFollowers);

		var room = new RoomState("room.altar", -1)
		{
			level = 3,
			damage = 0,
			assignedFollowers = 2
		};

		_church = new ChurchState(new List<RoomState> { room });

		Debug.Log($"[SmokeTest] Start: health = {_cult.godHealth}, gold = { _cult.gold }, favor = { _cult.favor }");
    }

    void Update()
    {
        _tickSystem.TickRoom(_church.rooms[0], _church, _cult, Time.deltaTime);

		if (Time.frameCount % 120 == 0)
		{
			Debug.Log($"[SmokeTest] health = {_cult.godHealth}, gold = { _cult.gold }, favor = { _cult.favor }, prog={_church.rooms[0].cycleProgress:F2}");
		}
    }
}
