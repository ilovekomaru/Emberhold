using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    public GameObject PlayerController;
    [SerializeField] GroundCheck groundCheck;

    Animator animator;
    FirstPersonMovement personMovement;
    Jump jump;
    Crouch crouch;
    bool wasInAir;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        personMovement = PlayerController.GetComponent<FirstPersonMovement>();
        jump = PlayerController.GetComponent<Jump>();
        crouch = PlayerController.GetComponent<Crouch>();
    }
    void Update()
    {
        if (wasInAir == false && !groundCheck.isGrounded)
            animator.SetTrigger("isJumped");

        animator.SetBool("isInAir", !groundCheck.isGrounded);
        animator.SetBool("isCrouched", crouch.IsCrouched);

        animator.SetBool("isWalking", false);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            animator.SetBool("isWalking", true);

        animator.SetBool("isRunning", personMovement.IsRunning);
        

        wasInAir = !groundCheck.isGrounded;
    }
}