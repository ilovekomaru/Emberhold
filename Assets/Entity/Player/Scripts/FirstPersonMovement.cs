using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;
    public float targetMovingSpeed;
    public GameObject playerModel;
    public float localRotation;
    public PlayerAnimationTriggers animationTriggers;

    Rigidbody rigidBody;
    float y;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        WalkingAndRotation();
        if (Input.GetKeyDown(attackKey))
            Attack();


    }

    void WalkingAndRotation()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        localRotation = this.transform.eulerAngles.y;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            y = Input.GetAxis("Horizontal") * 90;
            if (Input.GetAxis("Vertical") > 0)
            {
                y = y / (1 + Input.GetAxis("Vertical"));
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                y = -y / (1 - Input.GetAxis("Vertical")) + 180;
            }

            y += localRotation;
        }

        Debug.Log(y);
        Debug.Log(localRotation);
        playerModel.transform.rotation = Quaternion.AngleAxis(y, Vector3.up);

        // Apply movement.
        rigidBody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidBody.velocity.y, targetVelocity.y);
    }

    void Attack()
    {
        //get weapon attack speed from weapon config
        //todo: attack register and hitbox
        animationTriggers.PlayAnimation("Attack");
    }
}