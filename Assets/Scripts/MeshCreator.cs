using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    public GameObject vertex;
    public GameObject spring;
    public int linesNumber = 2;
    public int columnsNumber = 2;
    public int distance = 3;
    public float structuralSpringStiffness = 6f; // Type 1
    public float shearSpringStiffness = 6f; // Type 2
    public float flexionSpringStiffness = 6f; // Type 3 
    public GameObject[,] mesh;
    // Start is called before the first frame update
    void Start()
    {
        // Vertices init
        mesh = new GameObject[linesNumber,columnsNumber];
        for (int i=0; i < linesNumber ; i++){
            for ( int j=0; j < columnsNumber ; j++){
                mesh[i,j]= Instantiate(vertex, new Vector3(i*distance,0,j*distance),Quaternion.identity);
                mesh[i,j].transform.name = $"vertex {i},{j}";
                mesh[i, j].GetComponent<Mass3>().indexj = j;
                mesh[i, j].GetComponent<Mass3>().indexi = i;
            }
        }
        // Fixing top 2 corner vertices
        //mesh[0,0].GetComponent<Mass2>().setStationary(true);
        //mesh[0,columnsNumber-1].GetComponent<Mass2>().setStationary(true);

        mesh[0, 0].GetComponent<Mass3>().setStationary(true);
        mesh[0, columnsNumber - 1].GetComponent<Mass3>().setStationary(true);
        /*for (int i=0; i< columnsNumber ; i++){
            mesh[0,i].GetComponent<Mass2>().setStationary(true);
        }*/



        for (int i=0; i< linesNumber ; i++){
            for ( int j=0; j<columnsNumber ; j++){
                if (j < columnsNumber -1)
                    createSpringType1Right(i,j);
                if (i < linesNumber -1)
                    createSpringType1Down(i,j);
                if ( (i < linesNumber -1) && (j < columnsNumber -1) ){
                    createSpringType2UlDr(i,j);
                    createSpringType2UrDl(i,j);
                    }
                if (j < columnsNumber -2)
                    createSpringType3Right(i,j);
                if (i < linesNumber -2)
                    createSpringType3Down(i,j);
            }
        }
    }

    void createSpringType1Right(int i,int j){
        GameObject st1 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st1.name = ("spring"+i+"-"+j+"T1R");
        st1.GetComponent<Spring2>().setObject1(mesh[i,j]);
        st1.GetComponent<Spring2>().setObject2(mesh[i,j+1]);
        st1.GetComponent<Spring2>().setStiffness(structuralSpringStiffness);
    }

    void createSpringType1Down(int i,int j){
        GameObject st1 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st1.name = ("spring"+i+"-"+j+"T1D");
        st1.GetComponent<Spring2>().setObject1(mesh[i,j]);
        st1.GetComponent<Spring2>().setObject2(mesh[i+1,j]);
        st1.GetComponent<Spring2>().setStiffness(structuralSpringStiffness);
    }

    // Upper right -> down left /
    void createSpringType2UrDl(int i,int j){
        GameObject st2 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st2.name = ("spring"+i+"-"+j+"T2UrDl");
        st2.GetComponent<Spring2>().setObject1(mesh[i,j+1]);
        st2.GetComponent<Spring2>().setObject2(mesh[i+1,j]);
        st2.GetComponent<Spring2>().setStiffness(shearSpringStiffness);
    }

    // Upper Left -> down right \
    void createSpringType2UlDr(int i,int j){
        GameObject st2 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st2.name = ("spring"+i+"-"+j+"T2UlDr");
        st2.GetComponent<Spring2>().setObject1(mesh[i,j]);
        st2.GetComponent<Spring2>().setObject2(mesh[i+1,j+1]);
        st2.GetComponent<Spring2>().setStiffness(shearSpringStiffness);
    }

    void createSpringType3Right(int i,int j){
        GameObject st3 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st3.name = ("spring"+i+"-"+j+"T3R");
        st3.GetComponent<Spring2>().setObject1(mesh[i,j]);
        st3.GetComponent<Spring2>().setObject2(mesh[i,j+2]);
        st3.GetComponent<Spring2>().setStiffness(flexionSpringStiffness);
    }

    void createSpringType3Down(int i,int j){
        GameObject st3 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st3.name = ("spring"+i+"-"+j+"T3D");
        st3.GetComponent<Spring2>().setObject1(mesh[i,j]);
        st3.GetComponent<Spring2>().setObject2(mesh[i+2,j]);
        st3.GetComponent<Spring2>().setStiffness(flexionSpringStiffness);
    }
}
