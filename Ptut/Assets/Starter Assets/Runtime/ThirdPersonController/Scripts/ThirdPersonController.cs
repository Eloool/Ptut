using UnityEngine;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Référence vers le gestionnaire de périphérique")]
        public GestionnairePeripherique inputManager;

        [Tooltip("Target camera's follow target")]
        public GameObject CinemachineCameraTarget;

        [Header("Player Settings")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;
        public float JumpHeight = 1.2f;
        public float Gravity = -15.0f;

        [Header("Grounded Settings")]
        public bool Grounded = true;
        public float GroundedOffset = -0.14f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;

        [Header("Camera Settings")]
        public float TopClamp = 70.0f;
        public float BottomClamp = -30.0f;
        public float CameraAngleOverride = 0.0f;
        public bool LockCameraPosition = false;

        private float _speed;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private CharacterController _controller;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        private bool _initialized = false;

        private void Awake()
        {
            // Trouver le GestionnairePeripherique s'il n'est pas assigné
            if (inputManager == null)
            {
                inputManager = FindObjectOfType<GestionnairePeripherique>();
                if (inputManager == null)
                {
                    Debug.LogError("Aucun GestionnairePeripherique trouvé dans la scène!");
                    return;
                }
            }

            // Configurer la caméra si nécessaire
            if (CinemachineCameraTarget == null)
            {
                // Créer un nouveau GameObject pour la cible de la caméra
                CinemachineCameraTarget = new GameObject("PlayerCameraRoot");
                CinemachineCameraTarget.transform.SetParent(transform);
                CinemachineCameraTarget.transform.localPosition = new Vector3(0, 1.375f, 0);
                CinemachineCameraTarget.transform.localRotation = Quaternion.identity;
            }

            // Trouver la caméra principale
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (_mainCamera == null)
            {
                Debug.LogError("Pas de caméra avec le tag MainCamera trouvée!");
                return;
            }

            // Obtenir ou ajouter le CharacterController
            _controller = GetComponent<CharacterController>();
            if (_controller == null)
            {
                Debug.Log("Ajout automatique du CharacterController");
                _controller = gameObject.AddComponent<CharacterController>();
                // Configuration de base du CharacterController
                _controller.height = 2f;
                _controller.radius = 0.3f;
                _controller.center = new Vector3(0, 1f, 0);
            }

            // Configurer les GroundLayers si non configurés
            if (GroundLayers.value == 0)
            {
                GroundLayers = LayerMask.GetMask("Default");
            }

            // Initialiser la rotation de la caméra
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _initialized = true;
            Debug.Log("ThirdPersonController initialisé avec succès");
        }

        private void Start()
        {
            if (!_initialized)
            {
                Debug.LogError("ThirdPersonController n'a pas été correctement initialisé!");
                enabled = false;
                return;
            }

            // Vérifier que tous les composants nécessaires sont présents
            if (inputManager == null || CinemachineCameraTarget == null || _controller == null || _mainCamera == null)
            {
                Debug.LogError("Il manque des composants requis pour le ThirdPersonController!");
                enabled = false;
                return;
            }

            // S'assurer que le curseur est verrouillé
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            // Vérifier l'état d'initialisation à chaque activation
            if (!_initialized)
            {
                Awake();
            }
        }

        private void Update()
        {
            if (!_initialized) return;

            GroundedCheck();
            HandleGravityAndJump();
            Move();
        }

        private void LateUpdate()
        {
            if (!_initialized) return;

            CameraRotation();
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        private void CameraRotation()
        {
            if (inputManager.Watch.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = Time.deltaTime * 100; // Ajusté pour une meilleure sensibilité

                _cinemachineTargetYaw += inputManager.Watch.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += -inputManager.Watch.y * deltaTimeMultiplier; // Inversé pour une rotation plus naturelle
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            float targetSpeed = inputManager.SprintOn ? SprintSpeed : MoveSpeed;
            if (inputManager.Deplacement == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
            float speedOffset = 0.1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(inputManager.Deplacement.x, 0.0f, inputManager.Deplacement.y).normalized;

            if (inputManager.Deplacement != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void HandleGravityAndJump()
        {
            if (Grounded)
            {
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                if (inputManager.JumpOn)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                }
            }
            else
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}