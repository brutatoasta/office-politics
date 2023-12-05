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

    // J to interact
    public void OnInteractAction(InputAction.CallbackContext context)
    {
        if (context.started) interact.Invoke();
    }

    // WASD movement
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveCheck.Invoke(context.ReadValue<Vector2>());
        }
        if (context.canceled)
        {
            moveCheck.Invoke(Vector2.zero);
        }

    }

    // U for slot 1
    public void OnUseConsumable1Action(InputAction.CallbackContext context)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (context.started && scene.name != "PowerUpScene") GameManager.instance.UseCurrentConsumable(0);
    }

    // I for slot 2
    public void OnUseConsumable2Action(InputAction.CallbackContext context)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (context.started && scene.name != "PowerUpScene") GameManager.instance.UseCurrentConsumable(1);
    }

    // K to cycle inventory
    public void OnCycleConsumableAction(InputAction.CallbackContext context)
    {
        if (context.started) GameManager.instance.CycleInventory();
    }

    // Space to evade
    public void OnEvadeAction(InputAction.CallbackContext context)
    {
        if (context.started) evade.Invoke();
    }

    // Esc to pause
    public void OnPlayPauseAction(InputAction.CallbackContext context)
    {
        if (context.performed) playPause.Invoke();
    }

    // Tab to open tasklist
    public void OnPullupTaskAction(InputAction.CallbackContext context)
    {
        if (context.performed) showTaskList.Invoke();
    }
    // Started - The button press has been initiated.
    // Performed - The button press has been successfully performed. Runs immediately after the started phase.
    // Canceled - The button has been released.
}