using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject Card;

    public GameObject PlayerArea;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClick()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(PlayerArea.transform, false);
        }
    }
}
