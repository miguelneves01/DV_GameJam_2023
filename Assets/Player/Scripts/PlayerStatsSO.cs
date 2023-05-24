using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStats", menuName = "Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [field: SerializeField] public float Health { set; get; }
    [field: SerializeField] public float Speed { set; get; }
    [field: SerializeField] public float DashingPower { set; get; }
    [field: SerializeField] public float DashingTime { set; get; }
    [field: SerializeField] public float DashingCooldown { set; get; }

}