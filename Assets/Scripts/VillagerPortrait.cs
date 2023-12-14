using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VillagerPortraitHandler))]
public class VillagerPortrait : MonoBehaviour
{
    public PortraitLib portraitLib;

    /*
    [Range(1, 30)]
    public int face = 1;
    [Range(1, 30)]
    public int skinColor = 1;
    [Range(1, 30)]
    public int hair = 1;
    [Range(1, 30)]
    public int hairBehind = 1;
    [Range(1, 30)]
    public int hairColor = 1;
    [Range(1, 30)]
    public int beard = 1;
    [Range(1, 30)]
    public int eyeBrows = 1;
    [Range(1, 30)]
    public int eyes = 1;
    [Range(1, 30)]
    public int eyeColor = 1;
    [Range(1, 30)]
    public int nose = 1;
    [Range(1, 30)]
    public int mouth = 1;
    [Range(1, 2)]
    public int wrinkles = 1;
    */

    private VillagerPortraitHandler portrait;
    // private int[] prevValues = new int[12];

    // Start is called before the first frame update
    void Awake()
    {
        portrait = GetComponent<VillagerPortraitHandler>();
        // runInEditMode = true;
        // prevValues = new int[] { face, skinColor, hair, hairBehind, hairColor, beard, eyeBrows, eyes, eyeColor, nose, mouth, wrinkles };
        // InitColors();
    }

    /*
    private void Update()
    {
        int[] currentValues = new int[] { face, skinColor, hair, hairBehind, hairColor, beard, eyeBrows, eyes, eyeColor, nose, mouth, wrinkles };
        bool change = false;
        for(int i = 0; i < currentValues.Length; i++) { if (prevValues[i] != currentValues[i]) change = true; }
        if (change)
        {
            ChangePortrait();
            currentValues.CopyTo(prevValues,0);
        }
    }
    */

    void InitColors()
    {
        // Skin color
        portrait.face.material.SetTexture("_PaletteTex", portraitLib.skinPalette.texture);
        portrait.nose.material.SetTexture("_PaletteTex", portraitLib.skinPalette.texture);
        portrait.mouth.material.SetTexture("_PaletteTex", portraitLib.skinPalette.texture);
        portrait.wrinkles.material.SetTexture("_PaletteTex", portraitLib.skinPalette.texture);
        // Hair color
        portrait.hair.material.SetTexture("_PaletteTex", portraitLib.hairPalette.texture);
        portrait.hairBehind.material.SetTexture("_PaletteTex", portraitLib.hairPalette.texture);
        portrait.beard.material.SetTexture("_PaletteTex", portraitLib.hairPalette.texture);
        portrait.eyeBrows.material.SetTexture("_PaletteTex", portraitLib.hairPalette.texture);
        // Eye color
        portrait.eyes.material.SetTexture("_PaletteTex", portraitLib.eyePalette.texture);
    }

    public void SetPortraitLib(PortraitLib p)
    {
        portraitLib = p;
        InitColors();
    }

    public void ChangePortrait(int _face, int _hair, int _hairBehind, int _beard, int _eyeBrows, int _eyes, int _nose, int _mouth, int _wrinkles, int _skinColor, int _hairColor, int _eyeColor)
    {
        portrait.face.sprite = (_face < portraitLib.face.Count) ? portraitLib.face[_face] : portraitLib.face[0];
        portrait.hair.sprite = (_hair < portraitLib.hair.Count) ? portraitLib.hair[_hair] : portraitLib.hair[0];
        portrait.hairBehind.sprite = (_hairBehind < portraitLib.hairBehind.Count) ? portraitLib.hairBehind[_hairBehind] : portraitLib.hairBehind[0];
        portrait.beard.sprite = (_beard < portraitLib.beard.Count) ? portraitLib.beard[_beard] : portraitLib.beard[0];
        portrait.eyeBrows.sprite = (_eyeBrows < portraitLib.eyebrows.Count) ? portraitLib.eyebrows[_eyeBrows] : portraitLib.eyebrows[0];
        portrait.eyes.sprite = (_eyes < portraitLib.eyes.Count) ? portraitLib.eyes[_eyes] : portraitLib.eyes[0];
        portrait.nose.sprite = (_nose < portraitLib.nose.Count) ? portraitLib.nose[_nose] : portraitLib.nose[0];
        portrait.mouth.sprite = (_mouth < portraitLib.mouth.Count) ? portraitLib.mouth[_mouth] : portraitLib.mouth[0];
        portrait.wrinkles.sprite = (_wrinkles < portraitLib.wrinkles.Count) ? portraitLib.wrinkles[_wrinkles] : portraitLib.wrinkles[0];
        portrait.over.sprite = portraitLib.over;

        portrait.face.material.SetInteger("_IndexInt", _skinColor);
        portrait.mouth.material.SetInteger("_IndexInt", _skinColor);
        portrait.hair.material.SetInteger("_IndexInt", _hairColor);
        portrait.hairBehind.material.SetInteger("_IndexInt", _hairColor);
        portrait.eyeBrows.material.SetInteger("_IndexInt", _hairColor);
        portrait.beard.material.SetInteger("_IndexInt", _hairColor);
        portrait.eyes.material.SetInteger("_IndexInt", _eyeColor);
    }

    
}
