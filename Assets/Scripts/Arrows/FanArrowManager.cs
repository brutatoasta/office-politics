using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class FanArrowManager : MonoBehaviour
{
    public ArrowType arrowType = ArrowType.FanArrow;
    public FanArrow arrow;

    public void SpawnArrow(Transform bossCoords)
    {
        StartCoroutine(SpawnArrowRoutine(bossCoords));
    }

    IEnumerator SpawnArrowRoutine(Transform bossCoords)
    {
        // sfx for prepare fan arrow
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.prepareFanArrow);

        yield return new WaitForSeconds(0.4f);

        // sfx for throw fan arrow
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.throwFanArrow);

        for (int i = -1; i < 2; i++)
        {
            // initialise a new arrow
            FanArrow newArrow = Instantiate(arrow, bossCoords);

            // adjust its angle and shoot
            newArrow.Shoot2(bossCoords, i * 15f);
        }
        Destroy(gameObject);
    }
}