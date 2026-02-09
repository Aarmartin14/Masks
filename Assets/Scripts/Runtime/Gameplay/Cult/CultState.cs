using UnityEngine;

public class CultState
{
    public int gold;
	public int favor;
	public int godHealth;

	public int totalFollowers;

	public CultState(int gold, int favor, int godHealth, int totalFollowers)
	{
		this.gold = gold;
		this.favor = favor;
		this.godHealth = godHealth;
		this.totalFollowers = totalFollowers;
	}
}
