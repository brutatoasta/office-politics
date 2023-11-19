using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meetings : BaseInteractable
{
    // Start is called before the first frame update
    public Sprite[] sprites;  // Array of sprites to switch between
    private SpriteRenderer spriteRenderer;
    TaskName heldType;
    GameObject held;
    private HashSet<TaskName> _invalidInputs;
    private HashSet<TaskName> _validInputs; // hashset for faster checks
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override bool CanInteract()
    {
        held = GameManager.instance.held;

        if (held == null) // nothing in hand
        {
            return false;
        }
        else
        {
            heldType = held.GetComponent<Holdable>().taskName;
            return _validInputs.Contains(heldType) || _invalidInputs.Contains(heldType);
        }

    }
    public void SetSprite()
    {

    }

    protected override void OnInteract()
    {
        SpriteRenderer prepsprite = GetComponent<SpriteRenderer>();
        Debug.Log("111");
        if (heldType == TaskName.PrepMeeting)
        {
            Debug.Log("11112233");
            prepsprite.sprite = sprites[0];
        }
        else
        {
            prepsprite.sprite = sprites[1];
        }
        spriteRenderer.enabled = true;
    }
}
