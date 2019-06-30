using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject GroundPrefab = null;
    [SerializeField] GameObject UnitPrefab = null;

    public Transform[,] Tiles;

    [HideInInspector] public int Size;

    int UnitsCount;

    void Awake()
    {
        UnitsCount = Random.Range(1,6);
        Size = Random.Range(5,11);
        Tiles = new Transform[Size, Size];

        MoveCam();
        SpawnGround();
        SpawnUnits();

    }

    
    void SpawnGround()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Vector3 Pos = transform.position + Vector3.forward * i + Vector3.right * j + Vector3.down * 0.5f;
                GameObject instance = Instantiate(GroundPrefab, Pos, transform.rotation, transform);
                Tiles[i, j] = instance.transform;
            }
        }
    }

    void SpawnUnits()
    {
        for(int i = 0; i < UnitsCount; i++)
        {
            bool isEmpty;
            int a, b;
            do
            {
                a = Random.Range(0, Size);
                b = Random.Range(0, Size);
                isEmpty = Tiles[a, b].GetComponent<Ground>().GetState()==0;
            }
            while (!isEmpty);

            Vector3 Pos = transform.position + Vector3.forward * a + Vector3.right * b;
            Instantiate(UnitPrefab,Pos,transform.rotation,transform);
        }
    }

    public int[,] GetFieldMatrix()
    {
        int[,] FieldMatrix = new int[Size, Size];

        for(int i = 0; i < Size; i++)
        {
            for(int j = 0; j < Size; j++)
            {
                int StateID = Tiles[j, i].GetComponent<Ground>().GetState();
                if(StateID!=1)
                {
                    FieldMatrix[i, j] = 0;
                }
                else
                {
                    FieldMatrix[i, j] = 1;
                }
            }
        }
        return FieldMatrix;
    }

    void MoveCam()
    {
        Camera.main.transform.position += (Vector3.forward/1.5f + Vector3.right) * (Size / 2);
    }
}
