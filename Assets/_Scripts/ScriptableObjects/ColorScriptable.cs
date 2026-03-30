using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colors", menuName = "ColorScriptable")]
public class ColorScriptable : ScriptableObject
{
    [SerializeField] private List<ColorValues> colorList;

    [Serializable]
    public struct ColorValues
    {
        public Color color;
        public ColorEnum colorEnum;
    }


    public Color GetColor(int value)
    {
        return colorList.Find(x => (int)x.colorEnum == value).color;
    }

    public enum ColorEnum
    {
        None,
        color1,
        color2,
        color3,
        color4,
        color5,
        color6,
        color7,
        color8,
        color9,
        color10,
        color11
    }
}