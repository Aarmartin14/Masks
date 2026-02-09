using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Room Effects/Generate Mask", fileName = "Room Effect - Generate Mask")]
public class GenerateMaskEffect : RoomEffectDefinition
{
    string maskId = "maskId.placeholder";

	private void OnEnable()
	{
		isPassive = false;
	}

	public override void Apply(RoomTickContext ctx)
	{
		if (!ctx.cycleCompleted) return;
		
		Debug.Log($"[GenerateMaskEffect] Would generate mask '{maskId}' for cult.");
	}
}
