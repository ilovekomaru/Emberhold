using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIUpdater : MonoBehaviour
{
    public Slider HP;
    public Slider MP;
    public TMP_Text hpText;
    public TMP_Text mpText;
    public GameObject Shield;
    public GameObject ShieldText;

    public TMP_Text attack;
    public TMP_Text physical_resist;
    public TMP_Text magical_resist;
    public TMP_Text buffs;

    public CombatStats playerCombatStats;

    void Start()
    {
        HP.value = playerCombatStats.HP;
        MP.value = playerCombatStats.MP;

        hpText.text = $"{playerCombatStats.HP}/{playerCombatStats.maxHP}";
        mpText.text = $"{playerCombatStats.MP}/{playerCombatStats.maxMP}";
        Shield.GetComponent<Slider>().maxValue = playerCombatStats.maxHP;

        attack.text = playerCombatStats.attack.ToString();
        physical_resist.text = playerCombatStats.physicalResist.ToString();
        magical_resist.text = playerCombatStats.magicalResist.ToString();
    }

    void Update()
    {
        Shield.SetActive(!(playerCombatStats.shield <= 0));
        ShieldText.SetActive(!(playerCombatStats.shield <= 0));
        ShieldText.GetComponent<TMP_Text>().text = playerCombatStats.shield.ToString();

        HP.value = playerCombatStats.HP;
        MP.value = playerCombatStats.MP;

        hpText.text = $"{playerCombatStats.HP}/{playerCombatStats.maxHP}";
        mpText.text = $"{playerCombatStats.MP}/{playerCombatStats.maxMP}";
        Shield.GetComponent<Slider>().value = playerCombatStats.shield;

        attack.text = playerCombatStats.attack.ToString();
        physical_resist.text = playerCombatStats.physicalResist.ToString();
        magical_resist.text = playerCombatStats.magicalResist.ToString();
    }
}
