using Photon.Pun;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkspeed = 2.4f;
    [SerializeField] private float _runspeed = 10;
    private float _speed;
    [SerializeField] private float _rotationSpeed = 6.0f;

    private Vector3 _velocity = new Vector3();
    private float _gravity = -9.81f;

    private CharacterController _characterController;
    private Animator _animator;
    private PhotonView _photonView;
    private CameraController _cameraController;

    private PlayerControlls _input;
    private Vector2 _movementVector;
    private bool _isRunning;

    private void Awake()
    {
        _input = new PlayerControlls();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
        {
            _cameraController = FindObjectOfType<CameraController>();
            _cameraController.SetTarget(transform);
        }

        _input.KeyboardControlls.Movement.started += OnMoveInput;
        _input.KeyboardControlls.Movement.canceled += OnMoveInput;
        _input.KeyboardControlls.Movement.performed += OnMoveInput;
        _input.KeyboardControlls.Run.started += OnRunInput;
        _input.KeyboardControlls.Run.canceled += OnRunInput;
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {

        if (_photonView.IsMine)
        {

        Move();
        Rotate();

        }

    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }

    private void OnRunInput(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();
    }

    private void Move()
    {
        _speed = _isRunning ? _runspeed : _walkspeed;

        Vector3 desiredVelocity = new Vector3(_movementVector.x * _speed, _velocity.y + _gravity * Time.deltaTime, _movementVector.y * _speed) * Time.deltaTime;
        _velocity = Vector3.Lerp(_velocity, desiredVelocity, Time.deltaTime * 6);
        _velocity.y = _characterController.isGrounded ? 0 : _velocity.y;

        float speedParameter = (_movementVector * _speed).magnitude;
        _animator.SetFloat("Speed", speedParameter);

        _characterController.Move(_velocity);
    }

    private void Rotate()
    {
        if (_movementVector.magnitude == 0)
        {
            return;
        }
        Vector3 lookDirection = new Vector3(_velocity.x, 0, _velocity.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        transform.rotation = rotation;
    }
}
