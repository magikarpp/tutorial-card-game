using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    public GameObject Canvas;
    public PlayerManager PlayerManager;


    private bool isDraggable = true;
    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPos;
    private GameObject dropZone;
    private bool isOverDropZone;


    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isOverDropZone = true;
        dropZone = col.gameObject;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        if (!isDraggable) return;

        isDragging = true;
        startParent = transform.parent.gameObject;
        startPos = transform.position;

    }

    public void EndDrag()
    {
        if (!isDraggable) return;

        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
            isDraggable = false;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        }
        else
        {
            transform.position = startPos;
            transform.SetParent(startParent.transform, false);
        }
    }

}
