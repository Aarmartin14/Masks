using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Repair All Rooms", fileName = "Room Effect - Repair All Rooms")]
public class RepairAllRoomsEffect : RoomEffectDefinition
{
    public int damageReducedPerCompletion = 1;

	private void OnEnable()
	{
		isPassive = false;
	}

	public override void Apply(RoomTickContext ctx)
	{
		if (!ctx.cycleCompleted) return;
		if (damageReducedPerCompletion <= 0) return;

		foreach (var room in ctx.church.rooms)
		{
			if (room == null) continue;
			room.damage = Mathf.Max(0, room.damage - damageReducedPerCompletion);
		}
	}
}
