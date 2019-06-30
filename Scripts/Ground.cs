using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject Halo;
    [SerializeField] GameObject Box;

    [HideInInspector] public State state;
    [HideInInspector] public bool isSelected;

   
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Select()
    {
        isSelected = !isSelected;
        Halo.SetActive(isSelected);
        anim.SetTrigger("Clck");
    }

    public void SpawnBox()
    {
        Instantiate(Box, transform.position + Vector3.up, transform.rotation,transform);
        state = State.isBlocked;
    }

    public void UnBlock()
    {
        state = State.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
                if (other.tag == "Unit")
                    state = State.UnitIn;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Unit")
            state = State.Empty;
    }

    public enum State
    {
        Empty,
        isBlocked,
        UnitIn
    }

    public int GetState()
    {
        int StateId;
        switch(state)
        {
            case State.Empty:
                StateId = 0;
                break;

            case State.isBlocked:
                StateId = 1;
                break;

            case State.UnitIn:
                StateId = 2;
                break;
            default:
                StateId = 0;
                break;

        }
        return StateId;
    }

}
