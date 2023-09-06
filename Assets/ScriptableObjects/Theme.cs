using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ThemeScriptableObject", order = 1)]
public class Theme : ScriptableObject
{
    [SerializeField]
    public Color backgroundColor;
    [SerializeField]
    public Color relationContFontColor;
    [SerializeField]
    public Color buttonFontColor;
    [SerializeField]
    public Color relativesTitleFontColor;
    [SerializeField]
    public Color yearFontColor;
    [SerializeField]
    public Color villagerFontColor;
    [SerializeField]
    public Color sliderHandleColor;
    [SerializeField]
    public Color sliderPressedColor;
    [SerializeField]
    public Color sliderFillColor;
    [SerializeField]
    public Sprite relativesBackground;
    [SerializeField]
    public Sprite dropZone;
    [SerializeField]
    public Sprite button;
    [SerializeField]
    public Sprite titleBackground;
    [SerializeField]
    public Sprite cross;
    [SerializeField]
    public Sprite dropdownArrow;
    [SerializeField]
    public Sprite villagerBorder;
    [SerializeField]
    public Sprite originTown;
    [SerializeField]
    public Sprite originNeighbour;
    [SerializeField]
    public Sprite originMarriage;
    [SerializeField]
    public Sprite interrogationMark;
    [SerializeField]
    public Sprite ring;
    [SerializeField]
    public Sprite blood;
    [SerializeField]
    public Sprite single;
    [SerializeField]
    public Sprite graveyard;
    [SerializeField]
    public Sprite book;
    [SerializeField]
    public Sprite sliderHandle;
    [SerializeField]
    public Sprite sliderDisplay;
}
