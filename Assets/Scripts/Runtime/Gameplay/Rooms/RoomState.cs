using UnityEngine;

public sealed class RoomState
{
    public string roomTypeId;

	public int SlotIndex { get; private set; }

	public int level = 1;
	public int damage = 0;

	public int assignedFollowers = 0;

	public float cycleProgress = 0f;

	public float passiveRepairAccumulator = 0f;

	public RoomState(string roomTypeId, int slotIndex)
	{
		this.roomTypeId = roomTypeId;
		SlotIndex = slotIndex;
	}
}
