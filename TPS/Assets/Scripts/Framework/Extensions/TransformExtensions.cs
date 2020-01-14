using UnityEngine;

namespace Framework.Extensions {
	public static class TransformExtensions {
		/// <summary>
		///	Determines if the target is in LOS
		///	</summary>
		/// <param name="origin"></param>
		/// <param name="target"></param>
		/// <param name="fieldOfView"></param>
		/// <param name="collisionMask"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static bool IsInLineOfSight(this Transform origin, Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 offset) {
			Vector3 direction = target - origin.position;

			if(Vector3.Angle(origin.forward, direction.normalized) < fieldOfView / 2) {
				float distance = Vector3.Distance(origin.position, target);
				if(Physics.Raycast(origin.position + offset + Vector3.forward * .3f, direction.normalized, distance, collisionMask)) {
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
