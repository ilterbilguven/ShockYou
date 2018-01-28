using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpell : BaseSpell
{
    public int BalloonBonusCharge = 20;

    public static void UseSpell(PlayerController player)
    {
        player.AddCharge(20);
        Debug.Log("BaloonSpell");
    }
}
