using Controllers;
using UnityEngine;

public class TaserSpell : BaseSpell {

    public int TaserDamage = 30;

    public void UseSpell(PlayerController sender, PlayerController receiver)
    {
        Debug.Log(sender.PlayerID + " TASED " + receiver.PlayerID + " for " + sender.ChargeAmount + " static damage plus " + TaserDamage + " taser damage.");
        receiver.SetCurrentState(MovementType.Shock);
        sender.RemoveSpell();
    }
}
