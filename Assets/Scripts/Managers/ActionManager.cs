using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent<Vector2> moveCheck;
    public UnityEvent interact;
    public UnityEvent evade;
    public UnityEvent playPause;

    public void OnInteractAction(InputAction.CallbackContext context)
    {
        if (context.performed) interact.Invoke();
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveCheck.Invoke(context.ReadValue<Vector2>());
        }
        if (context.canceled)
        {
            moveCheck.Invoke(new Vector2(0, 0));
        }

    }

    public void OnUseConsumableAction(InputAction.CallbackContext context)
    {
        if (context.started) GameManager.instance.UseCurrentConsumable();
    }

    public void OnCycleConsumableAction(InputAction.CallbackContext context)
    {
        if (context.started) GameManager.instance.CycleInventory();
    }

    public void OnEvadeAction(InputAction.CallbackContext context)
    {
        if (context.started) evade.Invoke();
    }
    public void OnPlayPauseAction(InputAction.CallbackContext context)
    {
        if (context.performed) playPause.Invoke();
    }
    // Started - The button press has been initiated.
    // Performed - The button press has been successfully performed. Runs immediately after the started phase.
    // Canceled - The button has been released.
}