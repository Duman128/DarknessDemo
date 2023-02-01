using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Door;
    public LayerMask PlayerLayerMask;

    public GameObject BeginArea;
    public Vector2 DungeonsPointSize;
    public Transform BeginPoint;

    private bool Dungeon1IntroActive = true;


    public GameObject Dungeon_1_Area;
    public Transform Dungeon_1_Point;
    public Transform Dungeon_1_IntroExit;
    public Transform DoorPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(BeginPoint.position, DungeonsPointSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Dungeon_1_IntroExit.position, new Vector2(DungeonsPointSize.y, DungeonsPointSize.x));
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapBox(BeginPoint.position, DungeonsPointSize, 0, PlayerLayerMask))
            BeginArea.SetActive(false);
        else if (Dungeon1IntroActive && Physics2D.OverlapBox(Dungeon_1_IntroExit.position, new Vector2(DungeonsPointSize.y, DungeonsPointSize.x), 0, PlayerLayerMask))
        {
            Dungeon1IntroActive = false;
            Instantiate(Door, DoorPoint.position, Quaternion.identity, Dungeon_1_Area.transform);
        }
        else if (Physics2D.OverlapBox(Dungeon_1_Point.position, DungeonsPointSize, 0, PlayerLayerMask))
            Dungeon_1_Area.SetActive(false);


    }

}
