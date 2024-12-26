using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIUpdater : MonoBehaviour
{
    public Slider HP;
    public Slider MP;
    public GameObject Shield;

    public TMP_Text attack;
    public TMP_Text physical_resist;
    public TMP_Text magical_resist;
    public TMP_Text buffs;

    public CombatStats playerCombatStats;

    void Start()
    {
        HP.value = playerCombatStats.HP;
        MP.value = playerCombatStats.MP;
        Shield.GetComponent<Slider>().maxValue = playerCombatStats.maxHP;

        attack.text = playerCombatStats.attack.ToString();
        physical_resist.text = playerCombatStats.physicalResist.ToString();
        magical_resist.text = playerCombatStats.magicalResist.ToString();
        buffs.text = playerCombatStats.buffs.ToString();
    }

    void Update()
    {
        Shield.SetActive(!(playerCombatStats.shield <= 0));

        HP.value = playerCombatStats.HP;
        MP.value = playerCombatStats.MP;
        Shield.GetComponent<Slider>().value = playerCombatStats.shield;

        attack.text = playerCombatStats.attack.ToString();
        physical_resist.text = playerCombatStats.physicalResist.ToString();
        magical_resist.text = playerCombatStats.magicalResist.ToString();
        buffs.text = playerCombatStats.buffs.ToString();
    }
}
