using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Variavel))]
public class VariavelDrawerUI : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 80f;
    }

    public override void OnGUI(Rect position, SerializedProperty var, GUIContent label)
    {
        Rect propriedadeRect = position;
        propriedadeRect.yMax = propriedadeRect.yMin + EditorGUI.GetPropertyHeight(var.FindPropertyRelative("nome"));

        EditorGUI.PropertyField(propriedadeRect, var.FindPropertyRelative("nome"));

        Rect valorRect = propriedadeRect;
        valorRect.yMin = propriedadeRect.yMax;
        valorRect.yMax = propriedadeRect.yMax + EditorGUI.GetPropertyHeight(var.FindPropertyRelative("valor"));

        if (((TipoVariavel)var.FindPropertyRelative("tipoVariavel").enumValueIndex) == TipoVariavel.TEXTO)
        {
            EditorGUI.PropertyField(valorRect, var.FindPropertyRelative("valor"));
        }
        else if (((TipoVariavel)var.FindPropertyRelative("tipoVariavel").enumValueIndex) == TipoVariavel.BOOLEANO)
        {
            if (!bool.TryParse(var.FindPropertyRelative("valor").stringValue, out _))
            {
                var.FindPropertyRelative("valor").stringValue = "false";
            }

            var.FindPropertyRelative("valor").stringValue = EditorGUI.Toggle(valorRect, "Valor ", bool.Parse(var.FindPropertyRelative("valor").stringValue)).ToString();
        }
        else
        {
            if (!int.TryParse(var.FindPropertyRelative("valor").stringValue, out _))
            {
                var.FindPropertyRelative("valor").stringValue = "0";
            }

            var.FindPropertyRelative("valor").stringValue = EditorGUI.IntField(valorRect, "Valor ", int.Parse(var.FindPropertyRelative("valor").stringValue)).ToString();
        }


        Rect tipoVarRect = valorRect;
        tipoVarRect.yMin = valorRect.yMax;
        tipoVarRect.yMax = valorRect.yMax + EditorGUI.GetPropertyHeight(var.FindPropertyRelative("tipoVariavel"));

        EditorGUI.PropertyField(tipoVarRect, var.FindPropertyRelative("tipoVariavel"));
    }
}