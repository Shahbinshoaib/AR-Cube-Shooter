
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

 
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;
    [SerializeField]
    GameManager gameManager;
    UnityEvent placementUpdate;

    [SerializeField]
    GameObject visualObject;

    ARRaycastManager m_RaycastManager;
    ARPointCloudManager m_PointCloudManager;
    ARPointCloud m_PointCloud;
    ARPlaneManager m_PlaneManager;
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    private bool isSpawned = false;
    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_PointCloudManager = GetComponent<ARPointCloudManager>();
        m_PointCloud = GetComponent<ARPointCloud>();
        m_PlaneManager = GetComponent<ARPlaneManager>();

        if (placementUpdate == null)
            placementUpdate = new UnityEvent();

        placementUpdate.AddListener(DiableVisual); //Hiding the blue Mark
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon) && !isSpawned)
        {
         

            if (gameManager.spawnedCube == null)
            {
                gameManager.SpawnCubes(); 
                isSpawned = true; //To Initilize only once

            }
            
            placementUpdate.Invoke();
        }
    }

    public void DiableVisual()
    {
        visualObject.SetActive(false);
        m_RaycastManager.useGUILayout = false;
        m_PointCloudManager.enabled = false;
        m_PointCloud.enabled = false;
        m_PlaneManager.enabled = false;

    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    

}