using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent<Vector2> moveCheck;
    public UnityEvent interact;
    public UnityEvent interactRelease;

    // called twice, when pressed and unpressed
    public void OnInteractAction(InputAction.CallbackContext context)
    {
        Debug.Log("Press");
        if (context.performed) interact.Invoke();
        if (context.canceled) interactRelease.Invoke();
    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        Debug.Log("Press");
        if (context.started)
        {
            moveCheck.Invoke(context.ReadValue<Vector2>());
        }
        if (context.canceled)
        {
            moveCheck.Invoke(new Vector2(0,0));
        }

    }



}