using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject button1;
    private GameObject button2;
    private GameObject pont;

    Vector3 pontPositionDestination;

    private int indexBoutton;
    private bool enigmeFini;
    private bool bouttonAppuye;

    private float buttonDestination1Y;
    private float buttonDestination2Y;

    void Start()
    {
        indexBoutton = 0;
        enigmeFini = false;
        button1 = GameObject.Find("Button1");
        button2 = GameObject.Find("Button2");
        pont = GameObject.Find("Pont");

        buttonDestination1Y = button1.transform.position.y - 0.05f;
        buttonDestination2Y = button2.transform.position.y - 0.05f;

        pontPositionDestination = new Vector3(pont.gameObject.transform.position.x,-2, pont.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (indexBoutton == 2 && !enigmeFini)
        {

            bouttonAppuye = true;

            enigmeFini = true;
            

        }
        if(bouttonAppuye)
        {
            pont.transform.position = Vector3.MoveTowards(transform.position, pontPositionDestination, Time.deltaTime * 2);
        }

        if (enigmeFini)
        {
            descendreBoutton();
            indexBoutton = 2;

            // Set materials
            button1.GetComponent<Renderer>().material = button1.GetComponent<PressableButton>().vert;
            button2.GetComponent<Renderer>().material = button2.GetComponent<PressableButton>().vert;
            
        }
    }


    public void ajouterValeurIndex()
    {
        if (indexBoutton != 2)
        {
            indexBoutton++;
            
        }
    }
    public void diminuerValeurIndex()
    {
        if (indexBoutton != 2)
        {
            indexBoutton--;
        }
    }

    public void descendreBoutton()
    {
        button1.gameObject.transform.position = new Vector3(button1.gameObject.transform.position.x, buttonDestination1Y, button1.transform.position.z);
        button2.gameObject.transform.position = new Vector3(button2.gameObject.transform.position.x, buttonDestination2Y, button2.transform.position.z);
    }

}
