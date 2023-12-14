using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PortraitLib", order = 1)]
public class PortraitLib : ScriptableObject
{
    public Sprite emptySprite;
    public List<Sprite> face;
    public List<Sprite> nose;
    public List<Sprite> mouth;
    public List<Sprite> hair;
    public List<Sprite> hairBehind;
    public List<Sprite> beard;
    public List<Sprite> eyebrows;
    public List<Sprite> eyes;
    public List<Sprite> wrinkles;
    public Sprite over;

    public Sprite skinPalette;
    public Sprite hairPalette;
    public Sprite eyePalette;
}
