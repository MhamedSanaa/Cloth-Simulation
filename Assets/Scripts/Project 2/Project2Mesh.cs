using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project2Mesh : MonoBehaviour
{
    public GameObject vertex;
    public GameObject spring;
    public int linesNumber = 2;
    public int columnsNumber = 2;
    public int distance = 3;
    public float structuralSpringStiffness = 6f; // Type 1
    public float shearSpringStiffness = 6f; // Type 2
    public float structuralSpringLength = 3f; // Type 1
    public float shearSpringLength = 3f; // Type 2
    GameObject[,] mesh;
    // Start is called before the first frame update
    void Start()
    {
        // Vertices init
        mesh = new GameObject[linesNumber,columnsNumber];
        for (int i=0; i < linesNumber ; i++){
            for ( int j=0; j < columnsNumber ; j++){
                mesh[i,j]= Instantiate(vertex, new Vector3(i * distance, 0, j * distance),Quaternion.identity);
                mesh[i, j].name = "Vertex_" + i + "_" + j;
                //mesh[i,j].GetComponent<Project2Mass>().setStationary(true); 
            }
        }
        // Fixing top 2 corner vertices
        // mesh[0,0].GetComponent<Project2Mass>().setStationary(true);
        // mesh[0,columnsNumber-1].GetComponent<Project2Mass>().setStationary(true);
        // mesh[linesNumber-1,0].GetComponent<Project2Mass>().setStationary(true);
        // mesh[linesNumber-1,columnsNumber-1].GetComponent<Project2Mass>().setStationary(true);
        /*for (int i=0; i< columnsNumber ; i++){
            mesh[0,i].GetComponent<Project2Mass>().setStationary(true);
        }*/


         // fix the edges
        for(int i = 0; i < linesNumber; i++)
        {
            mesh[i, 0].GetComponent<Project2Mass>().setStationary(true);
            mesh[i, columnsNumber-1].GetComponent<Project2Mass>().setStationary(true);
        }

        for (int j = 0; j < columnsNumber; j++)
        {
            mesh[0, j].GetComponent<Project2Mass>().setStationary(true);
            mesh[linesNumber-1, j].GetComponent<Project2Mass>().setStationary(true);
        }

        

        for (int i=0; i< linesNumber ; i++){
            for ( int j=0; j<columnsNumber ; j++){
                if (j < columnsNumber -1)
                    createSpringType1Right(i,j);
                if (i < linesNumber -1)
                    createSpringType1Down(i,j);
                if ( (i < linesNumber -1) && (j < columnsNumber -1) ){
                    if(j % 2 == 0){
                        if (i % 2 == 0 ){
                            createSpringType2UlDr(i,j);
                        }
                        if (i % 2 != 0 ){
                            createSpringType2UrDl(i,j);
                        }   
                    }else{
                        if (i % 2 == 0 ){
                            createSpringType2UrDl(i,j); 
                        }
                        if (i % 2 != 0 ){
                            createSpringType2UlDr(i,j);
                        }   
                    }
                    
                }
            }
        }
    }

    void createSpringType1Right(int i,int j){
        GameObject st1 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st1.name = ("spring"+i+"-"+j+"T1R");
        st1.GetComponent<Project2Springs>().setObject1(mesh[i,j]);
        st1.GetComponent<Project2Springs>().setObject2(mesh[i,j+1]);
        st1.GetComponent<Project2Springs>().setStiffness(structuralSpringStiffness);
        st1.GetComponent<Project2Springs>().setStiffness(structuralSpringLength);
    }

    void createSpringType1Down(int i,int j){
        GameObject st1 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st1.name = ("spring"+i+"-"+j+"T1D");
        st1.GetComponent<Project2Springs>().setObject1(mesh[i,j]);
        st1.GetComponent<Project2Springs>().setObject2(mesh[i+1,j]);
        st1.GetComponent<Project2Springs>().setStiffness(structuralSpringStiffness);
        st1.GetComponent<Project2Springs>().setStiffness(structuralSpringLength);
    }

    // Upper right -> down left /
    void createSpringType2UrDl(int i,int j){
        GameObject st2 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st2.name = ("spring"+i+"-"+j+"T2UrDl");
        st2.GetComponent<Project2Springs>().setObject1(mesh[i,j+1]);
        st2.GetComponent<Project2Springs>().setObject2(mesh[i+1,j]);
        st2.GetComponent<Project2Springs>().setStiffness(shearSpringStiffness);
        st2.GetComponent<Project2Springs>().setStiffness(shearSpringLength);
    }

    // Upper Left -> down right \
    void createSpringType2UlDr(int i,int j){
        GameObject st2 = Instantiate(spring, new Vector3(0,0,0),Quaternion.identity);
        st2.name = ("spring"+i+"-"+j+"T2UlDr");
        st2.GetComponent<Project2Springs>().setObject1(mesh[i,j]);
        st2.GetComponent<Project2Springs>().setObject2(mesh[i+1,j+1]);
        st2.GetComponent<Project2Springs>().setStiffness(shearSpringStiffness);
        st2.GetComponent<Project2Springs>().setStiffness(shearSpringLength);
    }
}
