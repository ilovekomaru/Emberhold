using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CombatStats))]
public class CombatStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Получаем ссылку на редактируемый объект
        CombatStats stats = (CombatStats)target;

        // Настройка стиля заголовков
        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 12,
            normal = { textColor = Color.white }
        };

        // Общий фон для секций
        GUI.backgroundColor = new Color(0.2f, 0.3f, 0.4f, 1);

        EditorGUILayout.Space();

        // Секция здоровья
        GUILayout.BeginVertical("box");
        GUILayout.Label("Health Stats", headerStyle);
        stats.HP = EditorGUILayout.IntSlider("HP", stats.HP, 0, stats.maxHP);
        stats.maxHP = EditorGUILayout.IntField("Max HP", stats.maxHP);
        stats.hpRecoveryPerSecond = (int)EditorGUILayout.FloatField("HP Recovery Per Second", stats.hpRecoveryPerSecond);
        stats.shield = EditorGUILayout.IntField("Shield", stats.shield);
        GUILayout.EndVertical();

        EditorGUILayout.Space();

        // Секция атаки
        GUILayout.BeginVertical("box");
        GUILayout.Label("Attack Stats", headerStyle);
        stats.attack = EditorGUILayout.IntField("Attack", stats.attack);
        GUILayout.EndVertical();

        EditorGUILayout.Space();

        // Секция маны
        GUILayout.BeginVertical("box");
        GUILayout.Label("Mana Stats", headerStyle);
        stats.MP = EditorGUILayout.FloatField("MP", stats.MP);
        stats.maxMP = EditorGUILayout.FloatField("Max MP", stats.maxMP);
        stats.mpRecoveryPerSecond = EditorGUILayout.FloatField("MP Recovery Per Second", stats.mpRecoveryPerSecond);
        GUILayout.EndVertical();

        EditorGUILayout.Space();

        // Секция сопротивлений
        GUILayout.BeginVertical("box");
        GUILayout.Label("Resistances", headerStyle);
        stats.physicalResist = EditorGUILayout.FloatField("Physical Resist", stats.physicalResist);
        stats.magicalResist = EditorGUILayout.FloatField("Magical Resist", stats.magicalResist);
        GUILayout.EndVertical();

        EditorGUILayout.Space();

        // Секция прочего
        GUILayout.BeginVertical("box");
        GUILayout.Label("Other Stats", headerStyle);
        stats.buffs = EditorGUILayout.FloatField("Buffs", stats.buffs);
        stats.manualRecoveryPerSecond = EditorGUILayout.Toggle("Manual Recovery Per Second", stats.manualRecoveryPerSecond);
        GUILayout.EndVertical();

        // Применяем изменения, если что-то изменилось
        if (GUI.changed)
        {
            EditorUtility.SetDirty(stats);
        }
    }
}
