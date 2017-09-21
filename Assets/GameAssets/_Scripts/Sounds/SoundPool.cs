using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundPool : MonoBehaviour
{

    public static SoundPool Instance { get; set; }

    public enum ESounds
    {
        Arrow1,
        Arrow2,
        Arrow3,
        Arrow4,
        Arrow5,
        Spear1,
        Spear2,
        Sword1,
        Sword2,
        Sword3,
        Sword4,
        Sword5,
        Sword6,
        Sword7,
        Sword8,
        Sword9,
        Shout1,
        Shout2,
        Shout3,
        Shout4,
        Shout5,
        Gore1,
        Gore2,
        Dead1,
        Dead2,
        Dead3,
        Dead4,
        BuildingDestroyed,
        WallDestroyed
    }

    public enum ESounds2D
    {
        BuildingSelected,
        Upgrade,
        UpgradeFinished,
        Repair,
        RepairFinished,
        RecuitArcher,
        RecuitSwordsman,
        RecuitSpearman,
        Place1,
        Place2,
        Place3,
        Place4,
        UpgradeUnit,
        TimeButton,
        RoundBegin,
        RoundEnd
    }

    [Header("3D")]
    [SerializeField] private AudioClip Arrow1;
    [SerializeField] private AudioClip Arrow2;
    [SerializeField] private AudioClip Arrow3;
    [SerializeField] private AudioClip Arrow4;
    [SerializeField] private AudioClip Arrow5;
    [SerializeField] private AudioClip Spear1;
    [SerializeField] private AudioClip Spear2;
    [SerializeField] private AudioClip Sword1;
    [SerializeField] private AudioClip Sword2;
    [SerializeField] private AudioClip Sword3;
    [SerializeField] private AudioClip Sword4;
    [SerializeField] private AudioClip Sword5;
    [SerializeField] private AudioClip Sword6;
    [SerializeField] private AudioClip Sword7;
    [SerializeField] private AudioClip Sword8;
    [SerializeField] private AudioClip Sword9;
    [SerializeField] private AudioClip Shout1;
    [SerializeField] private AudioClip Shout2;
    [SerializeField] private AudioClip Shout3;
    [SerializeField] private AudioClip Shout4;
    [SerializeField] private AudioClip Shout5;
    [SerializeField] private AudioClip Gore1;
    [SerializeField] private AudioClip Gore2;
    [SerializeField] private AudioClip Dead1;
    [SerializeField] private AudioClip Dead2;
    [SerializeField] private AudioClip Dead3;
    [SerializeField] private AudioClip Dead4;
    [SerializeField] private AudioClip BuildingDestroyed;
    [SerializeField] private AudioClip WallDestroyed;

    [Header("2D")]
    [SerializeField] private AudioClip BuildingSelected;
    [SerializeField] private AudioClip Upgrade;
    [SerializeField] private AudioClip UpgradeFinished;
    [SerializeField] private AudioClip Repair;
    [SerializeField] private AudioClip RepairFinished;
    [SerializeField] private AudioClip RecuitArcher;
    [SerializeField] private AudioClip RecuitSwordsman;
    [SerializeField] private AudioClip RecuitSpearman;
    [SerializeField] private AudioClip Place1;
    [SerializeField] private AudioClip Place2;
    [SerializeField] private AudioClip Place3;
    [SerializeField] private AudioClip Place4;
    [SerializeField] private AudioClip UpgradeUnit;
    [SerializeField] private AudioClip TimeButton;
    [SerializeField] private AudioClip RoundBegin;
    [SerializeField] private AudioClip RoundEnd;


    private void Awake()
    {
        Instance = this;
    }

    #region 3D
    public AudioClip GetAudioClip(ESounds sound)
    {
        switch (sound)
        {
            case ESounds.Arrow1: return Arrow1;
            case ESounds.Arrow2: return Arrow2;
            case ESounds.Arrow3: return Arrow3;
            case ESounds.Arrow4: return Arrow4;
            case ESounds.Arrow5: return Arrow5;
            case ESounds.Spear1: return Spear1;
            case ESounds.Spear2: return Spear2;
            case ESounds.Sword1: return Sword1;
            case ESounds.Sword2: return Sword2;
            case ESounds.Sword3: return Sword3;
            case ESounds.Sword4: return Sword4;
            case ESounds.Sword5: return Sword5;
            case ESounds.Sword6: return Sword6;
            case ESounds.Sword7: return Sword7;
            case ESounds.Sword8: return Sword8;
            case ESounds.Sword9: return Sword9;
            case ESounds.Shout1: return Shout1;
            case ESounds.Shout2: return Shout2;
            case ESounds.Shout3: return Shout3;
            case ESounds.Shout4: return Shout4;
            case ESounds.Shout5: return Shout5;
            case ESounds.Gore1: return Gore1;
            case ESounds.Gore2: return Gore2;
            case ESounds.Dead1: return Dead1;
            case ESounds.Dead2: return Dead2;
            case ESounds.Dead3: return Dead3;
            case ESounds.Dead4: return Dead4;
            case ESounds.BuildingDestroyed: return BuildingDestroyed;
            case ESounds.WallDestroyed: return WallDestroyed;
            default:
                throw new ArgumentOutOfRangeException("sound", sound, null);
        }
    }

    public AudioClip GetArrowClip()
    {
        int value = Random.Range(1, 6);

        if (value < 2) return Arrow1;
        if (value < 3) return Arrow2;
        if (value < 4) return Arrow3;
        if (value < 5) return Arrow4;
        return Arrow5;
    }

    public AudioClip GetSpearClip()
    {
        float value = Random.value;

        if (value <= .5f) return Spear1;
        return Spear2;
    }

    public AudioClip GetSwordClip()
    {
        int value = Random.Range(1, 10);

        if (value < 2) return Sword1;
        if (value < 3) return Sword2;
        if (value < 4) return Sword3;
        if (value < 5) return Sword4;
        if (value < 6) return Sword5;
        if (value < 7) return Sword6;
        if (value < 8) return Sword7;
        if (value < 9) return Sword8;
        return Sword9;
    }

    public AudioClip GetShoutClip()
    {
        int value = Random.Range(1, 6);

        if (value < 2) return Shout1;
        if (value < 3) return Shout2;
        if (value < 4) return Shout3;
        if (value < 5) return Shout4;
        return Shout5;
    }

    public AudioClip GetGoreClip()
    {
        float value = Random.value;

        if (value <= .5f) return Gore1;
        else return Gore2;
    }

    public AudioClip GetDeadClip()
    {
        int value = Random.Range(1, 5);

        if (value < 2) return Dead1;
        if (value < 3) return Dead2;
        if (value < 4) return Dead3;
        return Dead4;
    }
    #endregion

    #region 2D
    public AudioClip GetAudioClip2D(ESounds2D sound)
    {
        switch (sound)
        {
            case ESounds2D.BuildingSelected: return BuildingSelected;
            case ESounds2D.Upgrade: return Upgrade;
            case ESounds2D.UpgradeFinished: return UpgradeFinished;
            case ESounds2D.Repair: return Repair;
            case ESounds2D.RepairFinished: return RepairFinished;
            case ESounds2D.RecuitArcher: return RecuitArcher;
            case ESounds2D.RecuitSwordsman: return RecuitSwordsman;
            case ESounds2D.RecuitSpearman: return RecuitSpearman;
            case ESounds2D.Place1: return Place1;
            case ESounds2D.Place2: return Place2;
            case ESounds2D.Place3: return Place3;
            case ESounds2D.Place4: return Place4;
            case ESounds2D.UpgradeUnit: return UpgradeUnit;
            case ESounds2D.TimeButton: return TimeButton;
            case ESounds2D.RoundBegin: return RoundBegin;
            case ESounds2D.RoundEnd: return RoundEnd;
            default:
                throw new ArgumentOutOfRangeException("sound", sound, null);
        }
    }

    public AudioClip GetPlaceClip2D()
    {
        int value = Random.Range(1, 5);

        if (value < 2) return Place1;
        if (value < 3) return Place2;
        if (value < 4) return Place3;
        return Place4;
    }
    #endregion

}
