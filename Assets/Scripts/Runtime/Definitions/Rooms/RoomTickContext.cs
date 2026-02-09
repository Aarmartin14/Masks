using UnityEngine;

public sealed class RoomTickContext
{
    public CultState cult;
	public ChurchState church;
	public RoomState room;

	public GameBalanceConfig balance;
	public RoomTypeDefinition roomType;

	public float deltaTime;
	public int contributors;
	public bool cycleCompleted;
}
