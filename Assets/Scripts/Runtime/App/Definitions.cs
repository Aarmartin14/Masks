using UnityEngine;

public class Definitions : MonoBehaviour
{
	public static Definitions Instance { get; private set; }

	[SerializeField] private DefinitionLibrary library;
	public static DefinitionLibrary Lib => Instance.library;
	
    void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
        Instance = this;
		DontDestroyOnLoad(gameObject);

		if (library == null)
		{
			Debug.LogError("Definition: DefinitionsLibrary not assigned");
			return;
		}

		library.BuildCache();
    }

	public static RoomTypeDefinition Room(string id) => Lib.GetRoom(id);
}
