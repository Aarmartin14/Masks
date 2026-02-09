using UnityEngine;
using System.Collections.Generic;

public class ChurchState
{
    public List<RoomState> rooms = new();

	public ChurchState(List<RoomState> rooms)
	{
		this.rooms = rooms;
	}
}
