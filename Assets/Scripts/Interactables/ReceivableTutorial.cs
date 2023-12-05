using UnityEngine.Events;

// Represents an interactable object that can receive items from players.
// Shredder and Laminator
public class ReceivableTutorial : Receivable
{
    public UnityEvent finishTutorial;
    protected override void OnInteract()
    {
        int originalPP = GameManager.instance.levelVariables.levelPP;

        // go through all relevant checks in the parent class
        base.OnInteract();

        // signal to the coffee cup that they should stop being active
        finishTutorial.Invoke();

        // reset Performance Point to avoid point farming in tutorial room
        GameManager.instance.levelVariables.levelPP = originalPP;
        GameManager.instance.showPerformancePoint.Invoke();
    }
}

