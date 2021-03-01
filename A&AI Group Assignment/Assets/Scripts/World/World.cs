using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance;

    [Header("Steering")]
    [SerializeField] bool steeringEnabled = true;
    [SerializeField] GameObject objectToSeek = null; // also currently used for pathfinding

    [Header("Pathfinding")]
    [SerializeField] bool pathfindingEnabled = false;
    [SerializeField] int graphWidth = 6;
    [SerializeField] int graphHeight = 6;
    [SerializeField] GameObject nodePrefab = null;

    Graph graph;
    Vechile agent;
    Pathfinding pathfinding;

    #region Callbacks

    void Awake(){
        Instance = this;
    }

    void Start(){
        if(steeringEnabled){
            agent = FindObjectOfType<Vechile>();
        }
        pathfinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        // change the current location of objectToSeek to where you click on the screen
        if(Input.GetMouseButtonDown(0)){
            objectToSeek.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objectToSeek.transform.position = new Vector3(objectToSeek.transform.position.x, objectToSeek.transform.position.y, 0);

            if(pathfindingEnabled){
                List<Node> path = pathfinding.AStarPath((Vector2)agent.transform.position, (Vector2)objectToSeek.transform.position);
            }
        }
    }

    #endregion

    #region Functions

    public void EnablePathfinding(){
        pathfindingEnabled = true;
        steeringEnabled = false;
        agent.steeringMovementOnUpdate = false;
    }

    public void CreateGraph(){
        graph = new Graph(graphWidth, graphHeight, nodePrefab);
    }

    public void MakeGraphVisible(){
        if(graph != null) {
            Node[] nodes = FindObjectsOfType<Node>();
            foreach(Node node in nodes){
                node.EnableSprite();
            }
        }
    }

    #endregion

    #region Return Information

    public GameObject GetTargetObject(){return objectToSeek;}
    public int GetGraphWidth(){return graphWidth;}
    public int GetGraphHeight(){return graphHeight;}

    #endregion
}
