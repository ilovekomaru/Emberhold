using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryManager Instance;
    public List<GameObject> weaponList;
    public int currentWeapon = 0;

    public Dictionary<string, int> resources;


    public KeyCode spellMenuKeycode;
    public KeyCode spell1UseKeycode;
    public KeyCode spell2UseKeycode; //Edit in settings menu

    public List<GameObject> slotPanels;
    public List<Animator> animators;
    public List<GameObject> statBars;
    public List<TextMeshProUGUI> statText;
    public List<TextMeshProUGUI> resText;
    public float statBarDefaultWidth = 192f;
    public float statBarSize;

    public int wood, stone;

    void Start()
    {
        Instance = this;
        foreach (GameObject weapon in weaponList)
        {
            weapon.SetActive(false);
        }
        animators[currentWeapon].SetBool("Selected", true);
        statText.Append(statBars[0].GetComponentInChildren<TextMeshProUGUI>());
        statText.Append(statBars[1].GetComponentInChildren<TextMeshProUGUI>());
    }

    void Update()
    {
        MyInputs();
        GetAndDisplayStats();
        DisplayResources();
    }

    public void AddResources(int _wood, int _stone)
    {
        wood += _wood;
        stone += _stone;
    }

    private void MyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animators[currentWeapon].SetBool("Selected", false);
            currentWeapon = 0;
            animators[currentWeapon].SetBool("Selected", true);
            SetWeaponActive();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animators[currentWeapon].SetBool("Selected", false);
            currentWeapon = 1;
            animators[currentWeapon].SetBool("Selected", true);
            SetWeaponActive();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animators[currentWeapon].SetBool("Selected", false);
            currentWeapon = 2;
            animators[currentWeapon].SetBool("Selected", true);
            SetWeaponActive();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animators[currentWeapon].SetBool("Selected", false);
            currentWeapon = 3;
            animators[currentWeapon].SetBool("Selected", true);
            SetWeaponActive();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") == -0.1f)
        {
            animators[currentWeapon].SetBool("Selected", false);
            currentWeapon++;
            if (currentWeapon > 3)
                currentWeapon = 3;
            animators[currentWeapon].SetBool("Selected", true);
            SetWeaponActive();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") == 0.1f)
        {
            animators[currentWeapon].SetBool("Selected", false);
            currentWeapon--;
            if (currentWeapon < 0)
                currentWeapon = 0;
            animators[currentWeapon].SetBool("Selected", true);
            SetWeaponActive();
        }
    }

    private void SetWeaponActive()
    {
        foreach (GameObject weapon in weaponList)
        {
            weapon.SetActive(false);
        }
        weaponList[currentWeapon].SetActive(true);
    }

    private void GetAndDisplayStats()
    {
        GameObject magicWeapon = weaponList[0];
        MagicWeapon magicWeaponStats = magicWeapon.GetComponentInChildren<MagicWeapon>();
        float health = 100;
        float healthpool = 100;
        float mana = magicWeaponStats.mana;
        float manapool = magicWeaponStats.stats.manapool;

        statBars[0].transform.localScale = new Vector3(health / healthpool, 1f, 1f);
        statText[0].text = $"{health} / {healthpool}";

        statBars[1].transform.localScale = new Vector3(mana / manapool, 1f, 1f);
        statText[1].text = $"{mana} / {manapool}";
    }

    private void DisplayResources()
    {
        resText[0].text = wood.ToString();
        resText[1].text = stone.ToString();
    }
}