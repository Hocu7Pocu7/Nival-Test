using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float Speed = 5;
    [SerializeField] float RotationSpeed = 5;

    public bool Alarm;

    Animator anim;
    Transform MrWhite;
    FieldManager FM;
    Navigation Nav;
    Vector3 Target;
    Vector3 StepTarget;
    Vector3 LastPos;
    bool Detour;

    void Awake()
    {
        FM = GameObject.Find("Field").GetComponent<FieldManager>();
        Nav = GetComponent<Navigation>();
        anim = GetComponentInChildren<Animator>();
        MrWhite = anim.transform;
        Alarm = false;

    }

    private void Start()
    {
        Target = transform.localPosition;
        StepTarget = Target;
    }

    void Update()
    {
       if(transform.localPosition == Target)
        {
            if(!Alarm)
            SetTarget();
            else
                anim.SetBool("Run", false);
        }
        else
        {
            if (transform.localPosition == StepTarget)
            {
                if(StepTarget!=Target)
                Step();

            }
            else
            {
                if (StepTarget != Vector3.one)
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, StepTarget, Speed * Time.deltaTime);
                    Quaternion newRotation = Quaternion.LookRotation(-LastPos + transform.position);
                    transform.rotation = Quaternion.LerpUnclamped(transform.rotation,newRotation,RotationSpeed*Time.deltaTime);
                    anim.SetBool("Run",true);

                    LastPos = transform.position;
                }
                else
                {
                    Step();
                    anim.SetBool("Run", false);
                }
            }
        }
        

    }

    void Step()
    {
       // Debug.Log("Step");
        StepTarget = Nav.Navigate(Target);
        
        Debug.Log(StepTarget);
    }

    public void SetTarget()
    {
        Debug.Log("SetTarget");
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Ground");
        int Selector;
        bool isEmpty;

        do
        {
            Selector = Random.Range(0,Tiles.Length);
            isEmpty = Tiles[Selector].GetComponent<Ground>().GetState() == 0 || Tiles[Selector].GetComponent<Ground>().GetState() == 2;
        }
        while (!isEmpty);

        Target = Tiles[Selector].transform.localPosition + Vector3.up * 0.5f;
        Debug.Log(Target);
    }

    public void SetTarget(Vector3 Pos)
    {
        Target = Pos;
    }
}
