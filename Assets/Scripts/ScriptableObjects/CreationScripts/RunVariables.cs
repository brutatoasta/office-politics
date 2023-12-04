using UnityEngine;

[CreateAssetMenu(fileName = "RunVariables", menuName = "ScriptableObjects/RunVariables")]
public class RunVariables : ScriptableObject
{
    // Tracks dynamic values for each Run
    // inventory
    public ABCConsumable[] consumableObjects;
    public int performancePoints;
    public bool upgradeBought;
    public int currentSceneIndex;

    public void Init()
    {
        // TODO: remove the consumables, let them be 0
        // start of whole game, not level
        consumableObjects = new ABCConsumable[]
        { new KitKat(20, 5), new Coffee(4, 10), new Adderall(3, 15), new Starman(1, 20) };
        upgradeBought = false;
        currentSceneIndex = 0;
        performancePoints = 0;
    }
}

public enum EvadeType
{
    Dash,
    Parry
}

public enum ConsumableType
{
    KitKat = 0,
    Coffee = 1,
    Adderall = 2,
    Starman = 3,
}