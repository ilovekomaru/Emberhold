using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aims : MonoBehaviour
{
    public float maxRayDistance = 200f;
    [SerializeField] GameObject fakeGameObject;

    public static Aims Instance;

    public GameObject SingleTargetRay(out bool enemyHit)
    {
        GameObject target = fakeGameObject;
        RaycastHit hit;
        hit = PlayerRayHit();
        if(hit.point == Vector3.zero)
        {
            hit.point = PlayerLookingAt();
        }
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            target = hit.collider.gameObject;
            enemyHit = true;
        }
        else
        {
            enemyHit = false;
        }
        return target;
    }

    /*
    public List<GameObject> MultipleTargetRay(out bool enemyHit)
    {
        List<GameObject> target = new List<GameObject>();
        float hitDistance = PlayerLookingDistance();
        
        BoxCollider collider = this.GetComponent<BoxCollider>();
        collider.size = new Vector3(1, 1, hitDistance);
       collider.center = new Vector3(0, 0, hitDistance / 2);
    }
    */



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, PlayerLookingAt());
        Gizmos.DrawCube(PlayerLookingAt(), Vector3.one * 0.1f);
    }

    public Vector3 PlayerLookingAt()
    {
        Vector3 rayStart = transform.position;
        if (!Physics.Raycast(rayStart, transform.forward, out RaycastHit hit, maxRayDistance))
            return rayStart + transform.forward * 100;
        return hit.point;
    }

    public float PlayerLookingDistance()
    {
        LayerMask mask = LayerMask.GetMask("GroundAndBuilds");
        Vector3 rayStart = transform.position;
        if (!Physics.Raycast(rayStart, transform.forward, out RaycastHit hit, maxRayDistance, mask))
            return 100f;
        return Vector3.Distance(rayStart, hit.point);
    }

    public RaycastHit PlayerRayHit()
    {
        Vector3 rayStart = transform.position;
        if (!Physics.Raycast(rayStart, transform.forward, out RaycastHit hit, maxRayDistance))
        {
            RaycastHit hit1 = new RaycastHit();
            hit1.point = Vector3.zero;
            return hit1;
        }            
        return hit;
    }
}