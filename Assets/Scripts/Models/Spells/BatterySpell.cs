using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpell : BaseSpell
{
    public int velocity = 500;
    public int BatteryDamage = 40;

    private PlayerController playerThatHasBattery;

    public void UseSpell(PlayerController player)
    {
        playerThatHasBattery = player;

        Debug.Log("BatterySpell");
        ThrowBattery(player);
    }
    
    public void ThrowBattery(PlayerController player)
    {
        Vector2 vector;

        if (player.Flip == true)    vector = new Vector2(velocity, 0);
        else                        vector = new Vector2(-velocity, 0); 

        player.PlayerHand.SpellInHand.gameObject.GetComponent<Rigidbody2D>().AddForce(vector, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
            Debug.Log("Player with id of " + player.PlayerID + " battered by " + BatteryDamage + " damage");
            //if (player.PlayerID == playerThatHasBattery.PlayerID) return;

            //Debug.Log(playerThatHasBattery.PlayerID + " BATTERED " + player.PlayerID);
            //Destroy(gameObject);
        }
    }


}
