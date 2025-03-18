using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "PlayerConfig")]
public class GameConfig : ScriptableObject
{   
    public float MoveSpeed => _moveSpeed;
    public float MouseSensitivity => _mouseSensitivity;
    public float RayCastDistance => _raycastDistance;
    public int SlotCount => _slotCount;
    public GameObject SlotPrefab => _slotPrefab;
    public float AnimationSpeed => _animationSpeed;


    [Header("Speed and Sensivity")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _mouseSensitivity = 2f;

    [Header("Interaction Settings")]
    [SerializeField] private float _raycastDistance = 5f;

    [Header("Inventory Settings")]
    [SerializeField] private int _slotCount = 5;
    [SerializeField] GameObject _slotPrefab;

    [Header("Animation Settings")]
    [SerializeField] private float _animationSpeed = 0.2f;

}