using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "PlayerConfig")]
public class GameConfig : ScriptableObject
{   
    public float MoveSpeed => _moveSpeed;
    public float MouseSensitivity => _mouseSensitivity;
    public float RayCastDistance => _raycastDistance;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _mouseSensitivity = 2f;

    [SerializeField] private float _raycastDistance = 5f;

}