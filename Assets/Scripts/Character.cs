using UnityEngine;

public class Character : MonoBehaviour,IMovable
{
   [SerializeField] private float speedChangeRate;
   protected float CurrentSpeed, WalkSpeed, RunSpeed;
   private Vector3 _moveVector;
   private float _horizontal, _vertical;
   protected CharacterController CharacterController;
   protected Animator Animator;
   private float _targetRotation;
   private float _rotationVelocity;
   private const float SmoothRotation = 0.1f;
   private bool _isRunning;
   private float _animationBlend;
   private static readonly int Speed = Animator.StringToHash("Speed");
   private static readonly int MotionSpeed = Animator.StringToHash("MotionSpeed");
   
   private void Update()
   {
      CharacterInput();
      CheckSpeed();
      SetAnimation();
   }

   private void FixedUpdate()
   {
      BasicMovement();
   }

   public void BasicMovement()
   {
      _moveVector = new Vector3(_horizontal, 0f, _vertical).normalized;
      if (_moveVector == Vector3.zero) return;
      _targetRotation = Mathf.Atan2(_horizontal, _vertical) * Mathf.Rad2Deg;
      var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
         SmoothRotation);
      transform.rotation = Quaternion.Euler(0.0f,rotation,0.0f);
      var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
      CharacterController.Move(targetDirection.normalized * (CurrentSpeed * Time.deltaTime));
   }

   private void CheckSpeed()
   {
      _isRunning = Input.GetKey(KeyCode.LeftShift);
      CurrentSpeed = _moveVector != Vector3.zero ? _isRunning ? RunSpeed : WalkSpeed : 0;
   }

   private void SetAnimation()
   {
      _animationBlend = Mathf.Lerp(_animationBlend, CurrentSpeed, Time.deltaTime * speedChangeRate);
      Animator.SetFloat(Speed,_animationBlend);
      Animator.SetFloat(MotionSpeed,_moveVector.magnitude);
   }

   private void CharacterInput()
   {
      _horizontal = Input.GetAxisRaw("Horizontal");
      _vertical = Input.GetAxisRaw("Vertical");
   }
}