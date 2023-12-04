using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ActionManager : MonoBehaviour
{
    public UnityEvent<Vector2> moveCheck;
    public UnityEvent interact;
    public UnityEvent evade;
    public UnityEvent playPause;
    public UnityEvent showTaskList;
    
    public void OnInteractAction(InputAction.CallbackContext context)
    {
        if (context.started) interact.Invoke();
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

    public void OnUseConsumable1Action(InputAction.CallbackContext context)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (context.started && scene.name != "PowerUpScene") GameManager.instance.UseCurrentConsumable(0);
    }
    public void OnUseConsumable2Action(InputAction.CallbackContext context)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (context.started && scene.name != "PowerUpScene") GameManager.instance.UseCurrentConsumable(1);
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
    public void OnPullupTaskAction(InputAction.CallbackContext context)
    {
        if (context.performed) showTaskList.Invoke();
    }
    // Started - The button press has been initiated.
    // Performed - The button press has been successfully performed. Runs immediately after the started phase.
    // Canceled - The button has been released.
}