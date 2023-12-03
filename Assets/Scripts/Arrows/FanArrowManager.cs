using Pathfinding;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FanArrowManager : MonoBehaviour
{

    //private TrailRenderer trailRenderer;


    public FanArrow arrow;
    private void Awake()
    {
        //speed = weaponGameConstants.jobArrowSpeed;
        //throwArrowAudioElement = GameManager.instance.audioElements.throwStressArrow;
        //trailRenderer = gameObject.GetComponent<TrailRenderer>();
        //trailRenderer.enabled = false;
    }

    public void Start()
    {

    }

    public void SpawnArrow(Transform bossCoords)
    {
        for (int i = -2; i < 3; i++)
        {
            //initialise a new arrow
            FanArrow newArrow = Instantiate(arrow, bossCoords);

            //adjust its angle
            //shoot
            //arrow.Shoot2(bossCoords, speed, i * 30f);
            newArrow.Shoot2(bossCoords, i * 7f);
        }
        Destroy(this.gameObject);
    }
}