using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModelCreator : MonoBehaviour
{
    public int fabricWidth;
    public int fabricHeight;

    private GameObject[] FabricUnits;
    // Start is called before the first frame update
    void Start()
    {
        GameObject fabric = new GameObject();
        fabric.name = "Fabric";
        fabric.transform.position = Vector3.zero;
        for (int i = 0; i < fabricHeight; i++)
        {
            for (int j = 0; j < fabricWidth; j++)
            {
                GameObject fabricUnit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                fabricUnit.name = "Fabric Unit (" + i + "," + j + ")";
                fabricUnit.tag = "FabricUnit";
                fabricUnit.transform.position = new Vector3(j*2, i*2, 0);
                fabricUnit.transform.SetParent(fabric.transform);
            }
        }
        FabricUnits = GameObject.FindGameObjectsWithTag("FabricUnit");


        for (int i = 0; i < fabricHeight; i++)
        {
            for (int j = 0; j < fabricWidth; j++)
            {
                if(i<fabricHeight-1 || j<fabricWidth-1)
                {
                    GameObject structuralSpring1 = new GameObject();
                    structuralSpring1.name = "structural springs (" + i + "," + j + ")" + "(" + (i) + ", " + (j + 1) + ")";
                    structuralSpring1.tag = "StructuralSpring";
                    GameObject structuralSpring2 = new GameObject();
                    structuralSpring2.name = "structural springs (" + i + "," + j + ")" + "(" + (i+1) + ", " + (j) + ")";
                    structuralSpring2.tag = "StructuralSpring";
                }

                //GameObject shearSpring = new GameObject();
                //structuralSpring.name = "Fabric Unit (" + i + "," + j + ")";
                //structuralSpring.tag = "FabricUnit";
                //structuralSpring.transform.position = new Vector3(j * 2, i * 2, 0);
                //structuralSpring.transform.SetParent(fabric.transform);

                //GameObject flexionSpring = new GameObject();
                //structuralSpring.name = "Fabric Unit (" + i + "," + j + ")";
                //structuralSpring.tag = "FabricUnit";
                //structuralSpring.transform.position = new Vector3(j * 2, i * 2, 0);
                //structuralSpring.transform.SetParent(fabric.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(FabricUnits.Length);
        Debug.Log(FabricUnits.GetValue(1));
    }
}
