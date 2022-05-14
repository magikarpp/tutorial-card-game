using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardZoom : NetworkBehaviour
{

    public GameObject MainCanvas;
    public GameObject ZoomCard;

    private GameObject zoomCard;
    private Sprite zoomSprite;

    public void Awake()
    {
        MainCanvas = GameObject.Find("Main Canvas");
        zoomSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnHoverEnter()
    {
        if (!hasAuthority) return;

        RectTransform rt = gameObject.GetComponent<RectTransform>();

        zoomCard = Instantiate(ZoomCard, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + (rt.rect.height * 1.75f)), Quaternion.identity);
        zoomCard.GetComponent<Image>().sprite = zoomSprite;
        zoomCard.transform.SetParent(MainCanvas.transform, true);
        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rt.rect.width * 2, rt.rect.height * 2);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
