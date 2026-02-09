using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Sacrifice Follower", fileName = "Room Effect - Sacrifice Follower")]
public class SacrificeFollowerEffect : RoomEffectDefinition
{
    public int followersConsumed = 1;
	public int damageToEnemyGod = 10;

	private void OnEnable()
	{
		isPassive = false;
	}

	public override void Apply(RoomTickContext ctx)
	{
		if (!ctx.cycleCompleted) return;
		if (followersConsumed <= 0) return;

		if (ctx.cult.totalFollowers < followersConsumed) return;

		ctx.cult.totalFollowers -= followersConsumed;

		Debug.Log($"[SacrificeFollowerEffect] Would deal {damageToEnemyGod} damage to enemy god.");
	}
}
