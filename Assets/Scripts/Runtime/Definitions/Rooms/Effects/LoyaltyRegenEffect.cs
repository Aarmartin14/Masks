using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Loyalty Regen", fileName = "Room Effect - Loyalty Regen")]
public class LoyaltyRegenEffect : RoomEffectDefinition
{
    public float regenMultiplier = 1f;

	private void OnEnable()
	{
		isPassive = true;
	}

	public override void Apply(RoomTickContext ctx)
	{
		
	}
}
