using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class AxePIckaxe : MonoBehaviour
{
    public Camera tpsCam;
    public InventoryManager inventoryManager;

    public float mineDistance;
    public float timeBetweenShooting;

    private Item item;
    
    private int axeDamage, pickaxeDamage;
    private bool shooting, readyToShoot, allowInvoke = true;

    private void Awake()
    {
        item = GetComponent<Item>();
        axeDamage = item.stats.AxeDamage;
        pickaxeDamage = item.stats.PickaxeDamage;

        readyToShoot = true;
    }

    private void Update()
    {
        AttackInput();
    }

    private void AttackInput()
    {
        shooting = Input.GetKey(KeyCode.Mouse0);
        if (readyToShoot && shooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = tpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        GameObject target;
        if (Physics.Raycast(ray, out hit))
            target = hit.transform.gameObject;
        else
            target = null;

        if (target != null && Vector3.Distance(tpsCam.transform.position, target.transform.position) <= mineDistance)
        {
            int _wood, _stone;
            Resource resource;
            if (resource = target.GetComponent<Resource>())
            {
                resource.Mine(item.stats.AxeDamage, item.stats.PickaxeDamage, hit.point, out _wood, out _stone);
                inventoryManager.Instance.AddResources(_wood, _stone);
            }
        }

        if (allowInvoke)
        {
            allowInvoke = false;
            Invoke("ResetShot", timeBetweenShooting);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}