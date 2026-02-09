using UnityEngine;

public class RoomTickSystem
{
    public void TickRoom(RoomState room, ChurchState church, CultState cult, float dt)
	{
		var lib = Definitions.Lib;
		var balance = lib.balance;
		var roomType = Definitions.Room(room.roomTypeId);

		int maxDamage = balance.globalMaxRoomDamage;
		room.damage = Mathf.Clamp(room.damage, 0, maxDamage);

		int effectiveLevel = Mathf.Max(0, room.level - room.damage);
		int contributors = Mathf.Min(room.assignedFollowers, effectiveLevel);

		if (contributors > 0 && room.damage > 0 && balance.passiveRepairPerSecond > 0f)
		{
			room.passiveRepairAccumulator += contributors  * balance.passiveRepairPerSecond * dt;
			while (room.passiveRepairAccumulator >= 1f && room.damage > 0)
			{
				room.passiveRepairAccumulator -= 1f;
				room.damage -= 1;
			}
		}

		var ctx = new RoomTickContext
		{
			cult = cult,
			church = church,
			room = room,
			balance = balance,
			roomType = roomType,
			deltaTime = dt,
			contributors = contributors,
			cycleCompleted = false
		};

		foreach (var effect in roomType.effects)
		{
			if (effect == null) continue;
			if (effect.isPassive)
			{
				effect.Apply(ctx);
			}
		}

		if (roomType.baseCycleSeconds > 0f && contributors > 0)
		{
			float speedFactor = roomType.GetFollowerSpeed(balance);
			float rateMultiplier = ComputeDiminishingMultiplier(contributors, speedFactor);

			float progressPerSecond = rateMultiplier / roomType.baseCycleSeconds;

			room.cycleProgress += progressPerSecond * dt;

			while (room.cycleProgress >= 1f)
			{
				room.cycleProgress -= 1f;
				ctx.cycleCompleted = true;

				foreach (var effect in roomType.effects)
				{
					if (effect == null) continue;
					if (!effect.isPassive)
					{
						effect.Apply(ctx);
					}
				}

				ctx.cycleCompleted = false;
			}
		}
	}

	public static float ComputeDiminishingMultiplier(int contributors, float k)
	{
		if (contributors <= 1) return 1f;
		return 1f + (contributors - 1) * Mathf.Clamp(k, 0f, 10f);
	}
}
