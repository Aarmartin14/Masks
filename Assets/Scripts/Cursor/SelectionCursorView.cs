using UnityEngine;

namespace Masks.Cursor
{
	public sealed class SelectionCursorView : MonoBehaviour
	{
		[SerializeField] private float _followSpeed = 25f;

		private Transform _target;

		public void SetTarget(Transform target) => _target = target;

		private void Update()
		{
			if (_target == null) return;
			var desiredPosition = _target.position;
			transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _followSpeed);
		}
	}
}
