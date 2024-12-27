using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    public GroundCheck groundCheck;

    float timer = 0.0f;
    public float fallingDamageMinTime;
    public bool isCalculatingFallingDamage = false;
    public float fallingDamage;
    public float fallingDamageDependingOnTimeMultipliyer;

    private void Update()
    {
        bool isGrounded = groundCheck.isGrounded;

        if (!isGrounded)
        {
            timer += Time.deltaTime;
            if (timer >= fallingDamageMinTime)
            {
                isCalculatingFallingDamage = true;
            }
        }
        if (isCalculatingFallingDamage && isGrounded)
        {
            isCalculatingFallingDamage = false;
            GetComponentInParent<CombatStats>().DealDamageToThis((int)(timer * fallingDamageDependingOnTimeMultipliyer), "Falling");
            timer = 0;
        }
    }
}
