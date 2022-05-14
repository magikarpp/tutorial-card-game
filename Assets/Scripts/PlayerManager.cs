using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public GameObject Card;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject DropZone;

    private List<GameObject> cards = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        DropZone = GameObject.Find("DropZone");

    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        cards.Add(Card);
        cards.Add(Card);
    }

    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "Dealt");
        }
    }

    [ClientRpc]
    private void RpcShowCard(GameObject card, string type)
    {
        if (type == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
            }
            else
            {
                card.transform.SetParent(EnemyArea.transform, false);
            }
        }
        else if (type == "Played")
        {
            card.transform.SetParent(DropZone.transform, false);
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    private void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

}
