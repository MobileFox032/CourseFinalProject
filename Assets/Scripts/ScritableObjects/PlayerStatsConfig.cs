using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "Scriptable Objects/PlayerStatsConfig")]
public class PlayerStatsConfig : ScriptableObject
{
    public int Health => playerHealth;
    [SerializeField] private int playerHealth = 100;
    public float Speed => playerSpeed;
    [SerializeField] private float playerSpeed = 5f;
    public int BasicDamage => playerBasicDamage;
    [SerializeField] private int playerBasicDamage = 20;
}
