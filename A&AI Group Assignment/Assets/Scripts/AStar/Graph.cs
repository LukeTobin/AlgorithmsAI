using UnityEngine;

public class Graph
{
    // matrix to store our graph
    int[,] graphGrid;

    // constructor to create a graph for us
    // ------------------------------------
    // width / height: width and height of the graph
    // nodeObject: prefab node object to instantiate
    public Graph(int width, int height, GameObject nodeObject){
        // resize the matricies to our width & height
        graphGrid = new int[width, height];
        // create an empty object to parent each node in the hierarchy
        GameObject graphObject = new GameObject("Graph");

        // loop through each index of the matricies and instantiate a node at each co-ordinate
        for(int x = 0;x < graphGrid.GetLength(0);x++){
            for(int y = 0;y < graphGrid.GetLength(1);y++){
                // Instantiate the node at x,y with no rotation applied
                GameObject node = GameObject.Instantiate(nodeObject, new Vector3(x, y, 1), Quaternion.identity);
                // make the node a child of our previously create object
                node.transform.parent = graphObject.transform;
            }
        }
    }
}
