using Controllers;
using UnityEngine;

public class TaserSpell : BaseSpell {

    public ushort TaserDamage = 30;

    public void UseSpell(PlayerController sender, PlayerController receiver)
    {
        Debug.Log(sender.PlayerID + " TASED " + receiver.PlayerID + " for " + sender.ChargeAmount + " static damage plus " + TaserDamage + " taser damage.");
        sender.Score += (ushort)(sender.ChargeAmount + TaserDamage);

        receiver.SetCurrentState(MovementType.Shock);
        sender.RemoveSpell();
    }
}
