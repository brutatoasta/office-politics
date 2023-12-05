using UnityEngine;

[CreateAssetMenu(fileName = "RunVariables", menuName = "ScriptableObjects/RunVariables")]
public class RunVariables : ScriptableObject
{
    // Tracks dynamic values for each Run
    // inventory
    public ABCConsumable[] consumableObjects;
    public int performancePoints;
    public bool upgradeBought;
    public int currentSceneIndex; //
    public float duration;  // how many real world seconds each level lasts

    public void Init()
    {
        // start of whole game, not level
        consumableObjects = new ABCConsumable[]
        { new KitKat(0, 150), new Coffee(0, 150), new Adderall(0, 200), new Starman(0, 300) };
        upgradeBought = false;
        currentSceneIndex = 0;
        performancePoints = 0;
        GameManager.instance.playerConstants.Init();
    }
}

public enum EvadeType
{
    Dash,
    Parry
}

public enum ConsumableType
{
    KitKat,
    Coffee,
    Adderall,
    Starman,
}