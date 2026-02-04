using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFocus : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;

	private void OnEnable()
	{
		if (firstSelected == null) return;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(firstSelected);
	}
}
