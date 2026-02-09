 using UnityEngine;
 using System.Collections.Generic;

public enum LoyaltyMode
{
	Drain,
	NoDrain,
	Regen
}

[CreateAssetMenu(fileName = "RoomTypeDefinition", menuName = "MaskOfGod/Definitions/Room Type")]
public class RoomTypeDefinition : ScriptableObject
{
    [Header("Identity")]
	public string id;
	public string displayName;
	public Sprite icon;

	[Header("Costs")]
	public int buildCostBase = 3;

	[Header("Leveling")]
	public int maxLevelOverride = 0;

	[Header("Production")]
	public float baseCycleSeconds = 5f;

	public float followerSpeedFactorOverride = -1f;

	[Header("Loyalty Rules")]
	public LoyaltyMode loyaltyMode = LoyaltyMode.Drain;

	[Header("Effects")]
	public List<RoomEffectDefinition> effects = new();

	public int GetMaxLevel(GameBalanceConfig balance) => maxLevelOverride > 0 ? maxLevelOverride : balance.globalMaxRoomLevel;

	public float GetFollowerSpeed(GameBalanceConfig balance) => followerSpeedFactorOverride >= 0f ? followerSpeedFactorOverride : balance.followerSpeedFactor;
}
