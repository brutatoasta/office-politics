public class KitKat : ABCConsumable
{
    public KitKat(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/Choco");
    }

    // Implementation of abstract class for effect
    public override void ConsumeEffect()
    {
        // Reduce stress by 10, cap min at 0
        GameManager.instance.levelVariables.stressPoints = (GameManager.instance.levelVariables.stressPoints > 10)?
                                                                GameManager.instance.levelVariables.stressPoints - 10:
                                                                0;
        GameManager.instance.IncreaseStress();
        // Invoke Side effects (particles etc.)
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.KitKat);
    }

}
