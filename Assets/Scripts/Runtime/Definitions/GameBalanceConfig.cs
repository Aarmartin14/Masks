using UnityEngine;

[CreateAssetMenu(menuName = "MaskOfGod/Balance/Game Balance Config", fileName = "GameBalanceConfig")]
public class GameBalanceConfig : ScriptableObject
{
	// Rooms
	[Header("Room Settings")]
	[Header("Sanctuary")]
	public float sanctuaryLoyaltyRecoveryPerSecond = 2f;
	public int sanctuaryCost = 5;
	public int sanctuaryStartLevel = 1;

	[Header("Altar")]
	public float altarDuration = 20f;
	public int alterHealthPerTrigger = 6;
	public int altarCost = 8;
	public int altarStartLevel = 2;

	[Header("Pews")]
	public float pewsDuration = 10f;
	public int pewsFavorPerTrigger = 1;
	public int pewsCost = 6;
	public int pewsStartLevel = 1;

	[Header("Mission")]
	public float missionDuration = 20f;
	public int missionFollowersPerTrigger = 1;
	public int missionCost = 10;
	public int missionStartLevel = 2;

	[Header("Tithe")]
	public float titheDuration = 30f;
	public int titheMoneyPerTrigger = 3;
	public int titheFavorCost = 2;
	public int titheCost = 7;
	public int titheStartLevel = 2;

	[Header("Workshop")]
	public float workshopDuration = 20f;
	public int workshopCost = 10;
	public int workshopStartLevel = 1;

	[Header("Ritual Hall (Wrath)")]
	public float ritualWrathDuration = 20f;
	public int ritualWrathCost = 10;
	public int ritualWrathStartLevel = 1;
	
	[Header("Ritual Hall (Lightning)")]
	public float ritualLightningDuration = 30f;
	public int ritualLightningCost = 10;
	public int ritualLightningStartLevel = 0;

	[Header("Ritual Hall (Flood)")]
	public float ritualFloodDuration = 60f;
	public int ritualFloodCost = 10;
	public int ritualFloodStartLevel = 0;

	[Header("Ritual Hall (Shield)")]
	public float ritualShieldDuration = 35f;
	public int ritualShildCost = 10;
	public int ritualShieldStartLevel = 0;

	[Header("Sacrificial Altar")]
	public float sacrificialAltarDuration = 40f;
	public int sacrificialAltarDamagePerTrigger = 10;
	public int sacrificialAltarCost = 15;
	public int sacrificialAltarStartLevel = 0;

	// Masks
	[Header("Mask Settings")]
	[Header("Wrath")]
	public int wrathFavorCost = 0;
	public int wrathDamageValue = 3;
	public float wrathTimer = 30f;

	[Header("Lightning")]
	public int lightningFavorCost = 3;
	public int lightningDamageValue = 1;
	public float lightningTimer = 30f;

	[Header("Flood")]
	public int floodFavorCost = 3;
	public int floodDamageValue = 2;
	public float floodTimer = 10f;

	[Header("Shield")]
	public int shieldFavorCost = 2;
	public float shieldTimer = 20f;

	// Logic
	// Room Logic
	[Header("Room Mechanics")]
	public float loyaltyDamagePerLevel = 15f;
	public float repairRatePerFollowerPerSecond = 0.1f;
	public float repairThreshold = 1f;
	public int maxRoomLevel = 6;

	// Follower Logic
	[Header("Follower Mechanics")]
	public float followerStartingLoyalty = 100f;
	public float followerMaximumLoyalty = 100f;
	public float followerDecayRatePerSecond = 1f;

	// God Logic
	public int godStartingHealth = 100;
	public int godMaximumHealth = 100;
	public int godStartingFavor = 5;
	public float godPassiveRegenRate = 0f;
	
	// Cult Logic
	public int cultStartingMoney = 0;
}
