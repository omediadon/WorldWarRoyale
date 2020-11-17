using UnityEngine;

[CreateAssetMenu(fileName = "Soldier Pro", menuName = "Data/SoldierPro")]
public class SoldierPro : ScriptableObject {
	public float SpeedFactor = 1f;
	public float CrouchSpeed = 3;
	public float WalkSpeed = 4;
	public float RunSpeed = 5;
	public float SprintSpeed = 8;
}
