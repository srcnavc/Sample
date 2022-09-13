using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InfiniteRoad : MonoBehaviour
{
    public static InfiniteRoad ins;
    public static Action<string> OnActiveZoneChanged;
    public int howManyTimesMapMoved = 0;
    
    [SerializeField] string activeZoneId;
    [SerializeField] NavMeshSurface surface;
    [SerializeField] int currentLevel;
    [SerializeField] public List<GameObject> inSceneObjects = new List<GameObject>();
    [SerializeField] List<GameObject> roadPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> levels = new List<GameObject>();
    [SerializeField] MapComponents activeZone;
    bool rebuildedAtFirstUpdate = false;
    GameObject tempGo;
    float tempFloat;
    GameObject currentZone;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("LevelData"))
            PlayerPrefs.SetInt("LevelData", 0);

        currentLevel = PlayerPrefs.GetInt("LevelData");

        if (ins == null)
            ins = this;
    }

    private void Start()
    {
        InitializeMap();
    }
    private void LateUpdate()
    {
        if (!rebuildedAtFirstUpdate)
        {
            surface.BuildNavMesh();
            rebuildedAtFirstUpdate = true;
        }

    }
    public string ActiveZoneId
    {
        get => activeZoneId;
        set
        {
            if (!value.Equals(activeZoneId))
            {
                activeZoneId = value;
                OnActiveZoneChanged?.Invoke(activeZoneId);
            }
        }
    }

    public MapComponents ActiveZone { get => activeZone; set => activeZone = value; }

    private Vector3 NextLocation(MapComponents comp1, MapComponents comp2)
    {
        tempFloat = comp1.transform.position.z
            + comp1.Renderer.bounds.extents.z
            + comp2.Renderer.bounds.extents.z;

        return new Vector3(0f, comp2.transform.position.y, tempFloat);
    }

    public void InitializeMap()
    {
        inSceneObjects.Clear();
        for (int i = 0; i < 4; i++)
        {
            if(i % 2 == 0)
            {
                tempGo = Instantiate(roadPrefabs[0]);
                tempGo.GetComponent<MapComponents>().ZoneId = "Zone_" + i;
                
            }
            else
            {
                if (levels.Count > 0)
                {
                    tempGo = Instantiate(levels[currentLevel % levels.Count]);
                    tempGo.GetComponent<MapComponents>().ZoneId = tempGo.name;
                    currentLevel++;
                }
                else
                    tempGo = Instantiate(roadPrefabs[1]);
            }

            if (i == 0)
                tempGo.transform.position = Vector3.zero;
            else
                tempGo.transform.position = NextLocation(inSceneObjects[i - 1].GetComponent<MapComponents>(), tempGo.GetComponent<MapComponents>());
            
            tempGo.name = tempGo.name + " - " + i;
            inSceneObjects.Add(tempGo);
        }
        SetCurrentZone(inSceneObjects[0]);
        surface.BuildNavMesh();
        
    }

    public void NextZone()
    {
        SetCurrentZone(inSceneObjects[inSceneObjects.IndexOf(currentZone) + 1]);
    }

    public MapComponents GetNextZone()
    {
        return inSceneObjects[inSceneObjects.IndexOf(currentZone) + 1].GetComponent<MapComponents>();
    }
    public void BuildNavmesh()
    {
        surface.UpdateNavMesh(surface.navMeshData);
        //surface.BuildNavMesh();
    }

    private void SetCurrentZone(GameObject go)
    {
        currentZone = go;
        ActiveZone = currentZone.GetComponent<MapComponents>();
        ActiveZoneId = ActiveZone.ZoneId;
    }
    public void MoveForward()
    {
        tempGo = inSceneObjects[0];
        inSceneObjects.Remove(tempGo);

        if (!tempGo.CompareTag("IdleGround"))
        {
            Destroy(tempGo);
            tempGo = LoadLevel();
        }
        
        tempGo.transform.position = NextLocation(inSceneObjects[inSceneObjects.Count - 1].GetComponent<MapComponents>(), tempGo.GetComponent<MapComponents>());

        inSceneObjects.Add(tempGo);
        howManyTimesMapMoved++;
        BuildNavmesh();
    }

    private GameObject LoadLevel()
    {
        if (levels.Count > 0)
        {
            tempGo = Instantiate(levels[currentLevel % levels.Count]);
            tempGo.GetComponent<MapComponents>().ZoneId = tempGo.name;
            tempGo.GetComponent<MapComponents>().runwayNavmesh.Enable();
            tempGo.GetComponent<RiseUp>().ResetRoad();
            currentLevel++;
            PlayerPrefs.SetInt("LevelData", currentLevel);
        }
        else
            tempGo = Instantiate(roadPrefabs[1]);
        
        return tempGo;
    }
}
