using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public FirstPersonLook look;
    public bool isAiming = false;

    void Start()
    {
        isAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = look.PlayerLookingAt();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }
    }

    public void ActivateCircleAim()
    {
         isAiming = true;
    }
}
