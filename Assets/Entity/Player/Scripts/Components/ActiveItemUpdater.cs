using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemUpdater : MonoBehaviour
{
    public GameObject[] itemsInHand;

    public void ChangeItem(GameObject item)
    {
        foreach (var currentItem in itemsInHand)
        {
            currentItem.SetActive(false);
        }
        item.SetActive(true);
    }
}
