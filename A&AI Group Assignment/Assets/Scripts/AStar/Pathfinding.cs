using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance;

    World world;

    List<Node> openList;
    List<Node> closedList;

    const int STRAIGHT_COST = 10;
    const int DIAGONAL_COST = 14; // Square root of a diagonal

    void Awake() {
        Instance = this;
        world = FindObjectOfType<World>();
    }


    public List<Node> AStarPath(Vector2 startingPosition, Vector2 endingPosition){
        List<Node> path = FindPath(startingPosition, endingPosition);
        if(path != null){
            Debug.Log("A path was found and returned");
            return path;
        }else{
            // no path found GUI popup?
            Debug.Log($"No path was found from { startingPosition } to {endingPosition}.");
            return null;
        }
    }

    List<Node> FindPath(Vector2 start, Vector2 end){
        // find nodes at both vectors
        Node startNode = FindNode(start);
        Node endNode = FindNode(end);

        // make sure we have an acceptable start & end node
        if(startNode == null){Debug.Log("no start node found");return null;}
        if(endNode == null){ Debug.Log("no end node found"); return null;} 

        // create new lists for our open and closed list
        openList = new List<Node>() { startNode };
        closedList = new List<Node>();

        int width = world.GetGraphWidth();
        int height = world.GetGraphHeight();

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Node node = FindNode(new Vector2(width, height));
                if(node != null){
                    node.g = 100000;//int.MaxValue; // set our distance to infinite at first
                    node.f = node.g + node.h;
                    node.predecessor = null;
                }else{
                    Debug.Log($"no node at {x}, {y}");
                }
            }
        }

        startNode.g = 0; // starting point
        startNode.h = GetDistanceBetweenNodes(startNode, endNode);
        startNode.f = startNode.g + startNode.h; // movement cost

        while(openList.Count > 0){
            // get the lowest costing (f) node in our open list
            Node currentNode = GetLowestCostNode(openList);

            // check if it is our final node and if it is, calculate and return the path
            if(currentNode == endNode){
                Debug.Log("found a path...");
                return null; 
            }

            // move the current node from the open list to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(Node neighbour in GetNodeNeighbours(currentNode)){
                // if the node being examined has already been searched, continue the iteration
                if(closedList.Contains(neighbour)) 
                    continue;

                // if the node is unwalkable, add it to the closed list and continue the iteration
                /*
                if(!neighbour.unwalkable){
                    closedList.Add(neighbour);
                    continue;
                }
                */

                int tentativeGScore = currentNode.g + GetDistanceBetweenNodes(currentNode, neighbour);
                Debug.Log(tentativeGScore + " , " + neighbour.g);
                if(tentativeGScore < neighbour.g){
                    neighbour.predecessor = currentNode;
                    neighbour.g = tentativeGScore;
                    neighbour.h = GetDistanceBetweenNodes(neighbour, endNode);
                    neighbour.f = neighbour.g + neighbour.h;

                    if(!openList.Contains(neighbour)){
                        openList.Add(neighbour);
                    }
                }
            }
        }
        
        return null;
    }

    // creates a overlap circle which checks if another collider (which nodes have) is in the coordinate 
    Node FindNode(Vector2 location){
        Collider2D hit = Physics2D.OverlapCircle(new Vector2((int)location.x, (int)location.y), 0.2f);
        if(hit != null){
            if(hit.GetComponent<Node>())
                return hit.GetComponent<Node>();
        }
        return null;
    }

    // TODO
    int GetDistanceBetweenNodes(Node a, Node b){
        int distanceX = Mathf.Abs((int)a.transform.position.x - (int)b.transform.position.x);
        int distanceY = Mathf.Abs((int)a.transform.position.y - (int)b.transform.position.y);
        int remainder = Mathf.Abs(distanceX - distanceY);
        return DIAGONAL_COST * Mathf.Min(distanceX, distanceY) + STRAIGHT_COST * remainder;
    }

    List<Node> GetNodeNeighbours(Node nodeToSearch){
        List<Node> neighbours = new List<Node>();

        Node nodeRight = FindNode(new Vector2(nodeToSearch.transform.position.x + 1, nodeToSearch.transform.position.y));
        Node nodeLeft = FindNode(new Vector2(nodeToSearch.transform.position.x - 1, nodeToSearch.transform.position.y));
        Node nodeUp = FindNode(new Vector2(nodeToSearch.transform.position.x, nodeToSearch.transform.position.y + 1));
        Node nodeDown = FindNode(new Vector2(nodeToSearch.transform.position.x, nodeToSearch.transform.position.y - 1));

        Node nodeRightUp = FindNode(new Vector2(nodeToSearch.transform.position.x + 1, nodeToSearch.transform.position.y + 1));
        Node nodeRightDown = FindNode(new Vector2(nodeToSearch.transform.position.x + 1, nodeToSearch.transform.position.y - 1));
        Node nodeLeftUp = FindNode(new Vector2(nodeToSearch.transform.position.x - 1, nodeToSearch.transform.position.y + 1));
        Node nodeLeftDown = FindNode(new Vector2(nodeToSearch.transform.position.x - 1, nodeToSearch.transform.position.y - 1));
        
        if(nodeRight != null) neighbours.Add(nodeRight);
        if(nodeLeft != null) neighbours.Add(nodeLeft);
        if(nodeUp != null) neighbours.Add(nodeUp);
        if(nodeDown != null) neighbours.Add(nodeDown);
        if(nodeRightUp != null) neighbours.Add(nodeRightUp);
        if(nodeRightDown != null) neighbours.Add(nodeRightDown);
        if(nodeLeftUp != null) neighbours.Add(nodeLeftUp);
        if(nodeLeftDown != null) neighbours.Add(nodeLeftDown);

        return neighbours;
    }
    
    // compares the cost of all nodes in a list and returns the lowest cost
    Node GetLowestCostNode(List<Node> nodes){
        Node lowestCostNode = nodes[0];
        for(int i = 0;i < nodes.Count;i++){
            if(nodes[i].f < lowestCostNode.f)
                lowestCostNode = nodes[i];
        }
        return lowestCostNode;
    }
}
