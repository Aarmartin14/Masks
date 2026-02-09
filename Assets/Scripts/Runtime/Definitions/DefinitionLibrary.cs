using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DefinitionLibrary", menuName = "MaskOfGod/Definitions/Definition Library")]
public class DefinitionLibrary : ScriptableObject
{
    public GameBalanceConfig balance;

	public List<RoomTypeDefinition> rooms = new();

	// TODO: List<MaskTypeDefinition> masks = new();

	private Dictionary<string, RoomTypeDefinition> _roomByID;

	public void BuildCache()
	{
		_roomByID = Build(rooms, r => r.id);
	}

	public RoomTypeDefinition GetRoom(string id) => Get(_roomByID, id, "Room");

	private static Dictionary<string, T> Build<T>(List<T> list, Func<T, string> getId)
	{
		var dict = new Dictionary<string, T>(StringComparer.Ordinal);
		foreach (var item in list)
		{
			if (item == null) continue;
			var id = getId(item);

			if(string.IsNullOrWhiteSpace(id))
			{
				Debug.LogError($"Definition has an empty id: {item}");
				continue;
			}

			if (!dict.TryAdd(id, item))
			{
				Debug.LogError($"Duplicate id '{id}' found in {typeof(T).Name} list.");
			}
		}

		return dict;
	}

	private static T Get<T>(Dictionary<string, T> dict, string id, string label)
	{
		if (dict == null)
		{
			throw new InvalidOperationException($"{label} cache not built. Call BuildCache() first.");
		}

		if (dict.TryGetValue(id, out var value))
		{
			return value;
		}

		throw new KeyNotFoundException($"{label} id not found: '{id}'");
	}
}
