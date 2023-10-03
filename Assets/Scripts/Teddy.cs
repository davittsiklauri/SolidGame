using UnityEngine;

public class Teddy : Character
{
    [SerializeField] private float currentSpeed, walkSpeed, runSpeed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    private void Start()
    {
        CurrentSpeed = currentSpeed;
        WalkSpeed = walkSpeed;
        RunSpeed = runSpeed;
        CharacterController = characterController;
        Animator = animator;
    }
}