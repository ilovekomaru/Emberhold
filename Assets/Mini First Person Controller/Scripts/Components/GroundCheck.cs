using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    float timer = 0.0f;
    public float fallingDamageMinTime;
    public bool isCalculatingFallingDamage = false; 
    public float fallingDamage; 
    public float fallingDamageDependingOnTimeMultipliyer; 

    [Tooltip("Maximum distance from the ground.")]
    public float distanceThreshold = .15f;

    [Tooltip("Whether this transform is grounded now.")]
    public bool isGrounded = true;
    /// <summary>
    /// Called when the ground is touched again.
    /// </summary>
    public event System.Action Grounded;

    const float OriginOffset = .001f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;


    void LateUpdate()
    {
        // Check if we are grounded now.
        bool isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

        // Call event if we were in the air and we are now touching the ground.
        if (isGroundedNow && !isGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        isGrounded = isGroundedNow;

        // Falling damage
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


    void OnDrawGizmosSelected()
    {
        // Draw a line in the Editor to show whether we are touching the ground.
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance, isGrounded ? Color.white : Color.red);
    }
}
