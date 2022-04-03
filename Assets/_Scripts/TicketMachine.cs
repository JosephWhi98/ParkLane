using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketMachine : InteractableItem
{
    public AudioSource source;

    public override void OnUse()
    {
        PurchaseTicket();
        GameManager.Instance.gotTicket = true;
        interactable = false;
    }

    public void PurchaseTicket()
    {
        source.Play();
    }
}
