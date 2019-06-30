using System.Collections;
using System;
using UnityEngine;

public class Navigation : MonoBehaviour
{

    Unit U;
    FieldManager FM;
    Vector3 Target;
    int d;
    int[,] Field;
    int[,] WeightMatrix;
    int a, b;
    Vector3[] WayPoints;

   void Awake()
    {
        FM = GameObject.Find("Field").GetComponent<FieldManager>();
        U = GetComponent<Unit>();
    }

    void Wave()
    {
     
        for (int i = 0; i < FM.Size; i++)
        {
            for (int j = 0; j < FM.Size; j++)
            {
                if (WeightMatrix[i, j] == d)
                {
                    if (i + 1 < FM.Size)
                    {
                        if (WeightMatrix[i + 1, j] == -1)
                        {  
                                WeightMatrix[i + 1, j] = d+1;
                        }
                    }

                    if (i - 1 >= 0)
                    {
                        if (WeightMatrix[i - 1, j] == -1)
                        {
                                WeightMatrix[i - 1, j] = d+1;
                        }
                    }

                    if (j + 1 < FM.Size)
                    {
                        if (WeightMatrix[i, j + 1] == -1)
                        {
                                WeightMatrix[i, j+1] = d+1;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (WeightMatrix[i, j - 1] == -1)
                        {
                                WeightMatrix[i, j-1] = d+1;
                        }
                    }
                }
            }
        }
        
    }

    Vector3[] Track()
    {
        Vector3 CurrentPoint = Target;
        int[] neighbors = new int[4];
        int i = (int)CurrentPoint.x;
        int j = (int)CurrentPoint.z;

        Vector3[] Way = new Vector3[WeightMatrix[i,j]];
   

        int n = Way.Length;
        Vector3 LastPoint;
        for (int c=0;c<Way.Length;c++)
        {
            n--;
            LastPoint = CurrentPoint;
            if (i + 1 < FM.Size)
            {
                if (WeightMatrix[i, j] - WeightMatrix[i + 1, j]==1)
                    CurrentPoint.Set(i+1, 0, j);
            }
                
            if (i - 1 >= 0 && WeightMatrix[i - 1, j] > -1)
            {
                if (WeightMatrix[i, j] - WeightMatrix[i - 1, j] == 1)
                    CurrentPoint.Set(i - 1, 0, j);
            }
            
            if (j + 1 < FM.Size && WeightMatrix[i, j + 1] > -1)
            {
                if (WeightMatrix[i, j] - WeightMatrix[i, j+1] == 1)
                    CurrentPoint.Set(i, 0, j+1);
            }

            if (j - 1 >= 0 && WeightMatrix[i, j - 1] > -1)
            {
                if (WeightMatrix[i, j] - WeightMatrix[i, j - 1] == 1)
                    CurrentPoint.Set(i, 0, j - 1);
            }
            
                

            if(CurrentPoint != LastPoint)
            {
                Way[n] = CurrentPoint;
                i = (int)CurrentPoint.x;
                j = (int)CurrentPoint.z;
            }
            else
            {
                return null;
            }

        }
        return Way;
    }

    public Vector3 Navigate(Vector3 TargetPosition)
    {
      
        WeightMatrix = new int[FM.Size, FM.Size];
        Field = FM.GetFieldMatrix();
        for (int i = 0; i < FM.Size; i++)
        {
            for (int j = 0; j < FM.Size; j++)
            {
                if (Field[i, j] == 0)
                    WeightMatrix[i, j] = -1;
                else
                    WeightMatrix[i, j] = -3;
            }
        }

       
        Target = TargetPosition;
        a = (int)transform.localPosition.x;
        b = (int)transform.localPosition.z;
        d = 0;

        
        WeightMatrix[a, b] = d;

        while (d < FM.Size*FM.Size && WeightMatrix[(int)Target.x,(int)Target.z]==-1)
        {
           
            Wave();
            d++;

        }
       // WaveOutput();
        if (WeightMatrix[(int)Target.x, (int)Target.z] > -1)
        {
            
            WayPoints = Track();
            if (WayPoints == null)
            {
                U.SetTarget();
                Debug.Log("AllBlocked");
                return Vector3.one;
                
            }
            else
            if (WayPoints.Length>1)
                return WayPoints[1];
            else
                return Target;
        }
        else
        {
            U.SetTarget();
            Debug.Log("TargetBlocked");
            return Vector3.one;
        }
    }

    void WaveOutput()
    {
        string output = "";
        for (int i = 0; i < FM.Size; i++)
        {
            for (int j = 0; j < FM.Size; j++)
            {
                output += WeightMatrix[i, j].ToString() + " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }
}
