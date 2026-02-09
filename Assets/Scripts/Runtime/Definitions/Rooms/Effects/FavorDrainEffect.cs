using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Favor Drain", fileName = "Room Effect - Favor Drain")]
public class FavorDrainEffect : RoomEffectDefinition
{
    public float drainPerSecondPerContributor = 0.25f;

	private void OnEnable()
	{
		isPassive = true;
	}

	public override void Apply(RoomTickContext ctx)
	{
		if (ctx.contributors <= 0) return;
		if (drainPerSecondPerContributor <= 0f) return;

		float drain = ctx.contributors * drainPerSecondPerContributor * ctx.deltaTime;
		int drainInt = Mathf.FloorToInt(drain);
		if (drainInt <= 0) return;

		ctx.cult.favor = Mathf.Max(0, ctx.cult.favor - drainInt);
	}
}
