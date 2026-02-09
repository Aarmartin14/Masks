using UnityEngine;

public abstract class RoomEffectDefinition : ScriptableObject
{
    public bool isPassive = false;

	public abstract void Apply(RoomTickContext ctx);
}
