using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "Scriptable Objects/PlayerStatsConfig")]
public class PlayerStatsConfig : ScriptableObject
{
    public int Health => playerHealth;
    private int playerHealth = 100;
    public float Speed => playerSpeed;
    private float playerSpeed = 5f;
    public int BasicDamage => playerBasicDamage;
    private int playerBasicDamage = 20;
}
