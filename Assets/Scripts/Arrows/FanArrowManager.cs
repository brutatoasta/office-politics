using UnityEngine;


public class FanArrowManager : MonoBehaviour
{
    public FanArrow arrow;

    public void SpawnArrow(Transform bossCoords)
    {
        for (int i = -2; i < 3; i++)
        {
            // initialise a new arrow
            FanArrow newArrow = Instantiate(arrow, bossCoords);

            // adjust its angle and shoot

            newArrow.Shoot2(bossCoords, i * 7f);
        }
        Destroy(gameObject);
    }
}