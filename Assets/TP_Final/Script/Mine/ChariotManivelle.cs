using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotManivelle : MonoBehaviour
{
    public GameObject Chariot;
    public bool Z;
    private Vector3 OriginalPos;
    private Vector3 LivePos;
    private Vector3 targetPos;
    private int Pos = 1;

    private void Start()
    {
        if(Z)
            OriginalPos = new Vector3(Chariot.transform.position.x, Chariot.transform.position.y, Chariot.transform.position.z - 1);
        else
            OriginalPos = new Vector3 (Chariot.transform.position.x - 1, Chariot.transform.position.y, Chariot.transform.position.z);

        targetPos = OriginalPos;
    }

    private void Update()
    {
        LivePos = Chariot.transform.position;

        float distance = Vector3.Distance(LivePos, targetPos);
        if(distance >= 1)
        {
            Chariot.transform.position = Vector3.Lerp(LivePos, targetPos, Time.deltaTime * 2);
        }
    }

    public int GetChariotPos()
    {
        return Pos;
    }

    public void ChangePos()
    {
        if (Z)
        {
            if (Pos == 1)
            {
                targetPos = new Vector3(OriginalPos.x, OriginalPos.y, OriginalPos.z + 4);
                Pos = 2;
            }
            else if (Pos == 2)
            {
                targetPos = new Vector3(OriginalPos.x, OriginalPos.y, OriginalPos.z + 5);
                Pos = 3;
            }
            else if (Pos == 3)
            {
                targetPos = OriginalPos;
                Pos = 1;
            }
        }
        else
        {
            if (Pos == 1)
            {
                targetPos = new Vector3(OriginalPos.x + 4, OriginalPos.y, OriginalPos.z);
                Pos = 2;
            }
            else if (Pos == 2)
            {
                targetPos = new Vector3(OriginalPos.x + 5, OriginalPos.y, OriginalPos.z);
                Pos = 3;
            }
            else if (Pos == 3)
            {
                targetPos = OriginalPos;
                Pos = 1;
            }
        }
    }
}