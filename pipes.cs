using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class pipes : MonoBehaviour
{ 
    public GameObject sphere;                                              // Intialize game objects
    public GameObject cylinder;

    public int minX = -25;
    public int maxX = 25;
    public int minY = -25;                                                 // Define the boundaries
    public int maxY = 25;
    public int minZ = -25;
    public int maxZ = 25;


    Color clr1 = Color.white;
    Color clr2 = Color.red;
    Color clr3 = Color.green;
    Color clr4 = Color.blue;                                               // Intialize the colors
    Color clr5 = Color.yellow;
    Color clr6 = Color.magenta;
    Color clr7 = Color.grey;


    public Vector3 currentPos = new Vector3(0, 0, 0);                      //Initialize current position vector
    public Color chosenclr;                                                //Color that is applied to the pipe
    public int direction;                                                  //Direction variable to decide the directio of generation

    public float time_to_generate = 0.3f;                                  //Time to call the function in definite intervels

    void Start()
    {
        Vector3 startPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));                              //Find a random start position
        currentPos = startPos;                                                                                                                     // Assign the start position to the current position
        GameObject firstSphere = Instantiate(sphere, startPos, Quaternion.identity, this.transform) as GameObject;                                 //spawn a sphere at this postion
        List<Color> pipecolors = new List<Color>() { clr1, clr2, clr3, clr4, clr5, clr6, clr7 };
        int i = Random.Range(0, 7);                                                                                                                //Create a list of colors and from that asign a random color to the sphere
        chosenclr = pipecolors[i];
        firstSphere.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        direction = Random.Range(1, 7);                                                                                                            //Choose a random direction to generate
        InvokeRepeating("generatePipes", 0.1f, time_to_generate);                                                                                  //Keep extending the pipes
        InvokeRepeating("Start", 5.0f, 5.0f);                                                                                                      //Keep generating new pipes in definite intervals
    }

    void generatePipes()
    {
        if (direction == 1)
        {
            if (currentPos.x >= maxX)                                             //Check whether the pipe is at boundary
            {
                direction = 2;                                                    //If so change direction and generate again
                generatePipes();
            }
            else
            {
                GenerateposX(Random.Range(3, maxX - (int)currentPos.x));       //otherwise generate pipe in positive x direction with a random length
            }
        }
        else if (direction == 2)                                                 //simlarly for other 5 direcitons
        {
            if (currentPos.x <= minX)
            {
                direction = 3;
                generatePipes();
            }
            else
            {
                GeneratenegX(Random.Range(minX - (int)currentPos.x, -2));         
            }

        }
        else if (direction == 3)                                                  
        {
            if (currentPos.y >= maxY)
            {
                direction = 4;
                generatePipes();
            }
            else
            {
                GenerateposY(Random.Range(3, maxY - (int)currentPos.y));
            }

        }
        else if (direction == 4)
        {
            if (currentPos.y <= minY)
            {
                direction = 3;
                generatePipes();
            }
            else
            {
                GeneratenegY(Random.Range(minY - (int)currentPos.y, -2));
            }
        }
        else if (direction == 5)
        {
            if (currentPos.z >= maxZ)
            {
                direction = 6;
                generatePipes();
            }
            else
            {
                GenerateposY(Random.Range(3, maxZ - (int)currentPos.z));
            }

        }
        else
        {
            if (currentPos.z <= minZ)
            {
                direction = 6;
                generatePipes();
            }
            else
            {
                GeneratenegZ(Random.Range(minZ - (int)currentPos.z, -2));
            }
        }
    }

    void GenerateposX(int len)                                                                            // Function to generate pipe in postive x direction
    {

        GameObject pipe = Instantiate(cylinder, new Vector3(currentPos.x + (len / 2.0f), currentPos.y, currentPos.z), Quaternion.Euler(new Vector3(0, 0, 270)), this.transform) as GameObject;                      //spawn a cyclinder at the current positon with rotation in z axis
        pipe.transform.localScale = new Vector3(1, (len/ 2.0f), 1);                                                                                                                                                //scale the cyclinder
        pipe.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);                                                                                                                                       //Assign the chosen color
        GameObject corner = Instantiate(sphere, new Vector3(currentPos.x + len, currentPos.y, currentPos.z), Quaternion.identity, this.transform) as GameObject;                                                    //At the end of the cyclinder spawn a sphere to create bends
        corner.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);                                                                                                                                     //Assign the chosen color
        currentPos += new Vector3(len, 0, 0);                                                                                                                                                                       //Update the current position
        direction = Random.Range(3, 7);                                                                                                                                                                             //Assign a new direction to generate
    }

    void GeneratenegX(int len)                                                                           //Similarly for the other five directions
    {

        GameObject pipe = Instantiate(cylinder, new Vector3(currentPos.x + (len / 2.0f), currentPos.y, currentPos.z), Quaternion.Euler(new Vector3(0, 0, 90)), this.transform) as GameObject;
        pipe.transform.localScale = new Vector3(1, (len/2.0f), 1);
        pipe.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        GameObject corner = Instantiate(sphere, new Vector3(currentPos.x + len, currentPos.y, currentPos.z), Quaternion.identity, this.transform) as GameObject;
        corner.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        currentPos += new Vector3(len, 0, 0);
        direction = Random.Range(3, 7);
    }

    void GenerateposY(int len)
    {
        GameObject pipe = Instantiate(cylinder, new Vector3(currentPos.x, currentPos.y + (len / 2.0f), currentPos.z), Quaternion.identity,this.transform) as GameObject;
        pipe.transform.localScale = new Vector3(1, (len/2.0f), 1);
        pipe.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        GameObject corner = Instantiate(sphere, new Vector3(currentPos.x, currentPos.y + len, currentPos.z), Quaternion.identity, this.transform) as GameObject;
        corner.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        currentPos += new Vector3(0, len, 0);
        int randnum = Random.Range(0, 1);
        if (randnum == 0)
        {
            direction = Random.Range(1, 3);
        }
        else
        {
            direction = Random.Range(5, 7);
        }
    }

    void GeneratenegY(int len)
    {
        GameObject pipe = Instantiate(cylinder, new Vector3(currentPos.x, currentPos.y + (len / 2.0f), currentPos.z), Quaternion.identity,this.transform) as GameObject;
        pipe.transform.localScale = new Vector3(1, (len/2.0f), 1);
        pipe.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        GameObject corner = Instantiate(sphere, new Vector3(currentPos.x, currentPos.y + len, currentPos.z), Quaternion.identity, this.transform) as GameObject;
        corner.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        currentPos += new Vector3(0, len, 0);
        int randnum = Random.Range(0, 1);
        if (randnum == 0)
        {
            direction = Random.Range(1, 3);
        }
        else
        {
            direction = Random.Range(5, 7);
        }
    }

    void GenerateposZ(int len)
    {

        GameObject pipe = Instantiate(cylinder, new Vector3(currentPos.x, currentPos.y, currentPos.z + (len / 2.0f)), Quaternion.Euler(new Vector3(270, 0, 0)), this.transform) as GameObject;
        pipe.transform.localScale = new Vector3(1, (len/2.0f), 1);
        pipe.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        GameObject corner = Instantiate(sphere, new Vector3(currentPos.x, currentPos.y, currentPos.z + len), Quaternion.identity, this.transform) as GameObject;
        corner.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        currentPos += new Vector3(0, 0, len);
        direction = Random.Range(1, 5);
    }
    void GeneratenegZ(int len)
    {

        GameObject pipe = Instantiate(cylinder, new Vector3(currentPos.x, currentPos.y, currentPos.z + (len / 2.0f)), Quaternion.Euler(new Vector3(90, 0, 0)), this.transform) as GameObject;
        pipe.transform.localScale = new Vector3(1, (len/2.0f), 1);
        pipe.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        GameObject corner = Instantiate(sphere, new Vector3(currentPos.x, currentPos.y, currentPos.z + len), Quaternion.identity, this.transform) as GameObject;
        corner.GetComponent<Renderer>().material.SetColor("_Color", chosenclr);
        currentPos += new Vector3(0, 0, len);
        direction = Random.Range(1, 5);
    }

    void OnCollisionEnter(Collision collision)                                                                        //Function to check for collisions
    {
        if (collision.gameObject.name == "Gameobject" || collision.gameObject.name =="darkroom" || collision.gameObject.name == "plane")
        {
            Debug.Log("Collision detected");
            CancelInvoke();
            Start();
        }
    }

}