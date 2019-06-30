using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{

    GameObject[] Tiles;
    GameObject[] Units;
    Transform[] Selected;
    int CountOfSelected;
    int j;
    bool alarm;

public void StartAlarm()
    {
        if (!alarm)
        {
            alarm = true;
            Tiles = GameObject.FindGameObjectsWithTag("Ground");
            Units = GameObject.FindGameObjectsWithTag("Unit");
            CountOfSelected = 0;

            for (int i = 0; i < Tiles.Length; i++)
            {
                if (Tiles[i].GetComponent<Ground>().isSelected)
                    CountOfSelected++;
            }

            Selected = new Transform[CountOfSelected];
            j = 0;

            for (int i = 0; i < Tiles.Length; i++)
            {
                if (Tiles[i].GetComponent<Ground>().isSelected)
                {
                    Selected[j] = Tiles[i].transform;
                    j++;
                }
            }

            if (Selected.Length <= Units.Length)
            {
                for (int i = 0; i < Selected.Length; i++)
                {
                    Unit UnitScript = Units[i].GetComponent<Unit>();
                    UnitScript.SetTarget(Selected[i].localPosition+Vector3.up/2);
                    UnitScript.Alarm = true;
                }
            }
            else
            {
                for (int i = 0; i < Units.Length; i++)
                {
                    Unit UnitScript = Units[i].GetComponent<Unit>();
                    UnitScript.SetTarget(Selected[i].localPosition+Vector3.up/2);
                    UnitScript.Alarm = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < Units.Length; i++)
            {
                Unit UnitScript = Units[i].GetComponent<Unit>();
                UnitScript.Alarm = false;
                alarm = false;
            }
        }
    }
}
