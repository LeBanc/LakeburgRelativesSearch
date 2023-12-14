using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitManager : MonoBehaviour
{
    public PortraitLib baby;
    public PortraitLib child;
    public PortraitLib boy;
    public PortraitLib girl;
    public PortraitLib man;
    public PortraitLib woman;
    public PortraitLib oldMan;
    public PortraitLib oldWoman;

    private static PortraitManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static PortraitLib GetPortraitLib(VillagerData.PortraitEnum e)
    {
        switch(e)
        {
            case VillagerData.PortraitEnum.Baby:
                return instance.baby;
            case VillagerData.PortraitEnum.Child:
                return instance.child;
            case VillagerData.PortraitEnum.Boy:
                return instance.boy;
            case VillagerData.PortraitEnum.Girl:
                return instance.girl;
            case VillagerData.PortraitEnum.Man:
                return instance.man;
            case VillagerData.PortraitEnum.Woman:
                return instance.woman;
            case VillagerData.PortraitEnum.OldMan:
                return instance.oldMan;
            case VillagerData.PortraitEnum.OldWoman:
                return instance.oldWoman;
            default: return null;
        }
    }
}
