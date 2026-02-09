using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Recruit Follower", fileName = "Room Effect - Recruit Follower")]
public class RecruitFollowerEffect : RoomEffectDefinition
{
    public int followersPerCompletion = 1;

	private void OnEnable()
	{
		isPassive = false;
	}

	public override void Apply(RoomTickContext ctx)
	{
		if (!ctx.cycleCompleted) return;
		if (followersPerCompletion <= 0) return;

		ctx.cult.totalFollowers += 1;
	}
}
