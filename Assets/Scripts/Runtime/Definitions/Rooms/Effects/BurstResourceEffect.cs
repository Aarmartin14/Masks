using UnityEngine;

public enum BurstResourceType
{
	GodHealth,
	Favor,
	Gold
}

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Burst Resource", fileName = "Room Effect - Burst")]
public class BurstResourceEffect : RoomEffectDefinition
{
    public BurstResourceType resourceType;
	public int amountPerCompletion;
	
	private void OnEnable()
	{
		isPassive = false;
	}

	public override void Apply(RoomTickContext ctx)
	{
		if (!ctx.cycleCompleted) return;
		if (amountPerCompletion == 0) return;

		switch(resourceType)
		{
			case BurstResourceType.GodHealth:
				ctx.cult.godHealth += amountPerCompletion;
				break;
			case BurstResourceType.Favor:
				ctx.cult.favor += amountPerCompletion;
				break;
			case BurstResourceType.Gold:
				ctx.cult.gold += amountPerCompletion;
				break;
		}
	}
}
