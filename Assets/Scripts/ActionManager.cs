using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent<Vector2> moveCheck;
    public UnityEvent interact;
    public UnityEvent interactRelease;

    public void OnInteractAction(InputAction.CallbackContext context)
    {
        if (context.performed) interact.Invoke();
        if (context.canceled) interactRelease.Invoke();
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveCheck.Invoke(context.ReadValue<Vector2>());
        }
        if (context.canceled)
        {
            moveCheck.Invoke(new Vector2(0,0));
        }

    }



}