using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BuildingManager : MonoBehaviourPun
{
    public GameObject[] objects;

    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    public bool canPlace = true;
    public float rotateAmount;
    public bool gridOn = true;
    public float gridSize;
    [SerializeField] private Toggle gridToggle;

    void Update()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x), RoundToNearestGrid(pos.y), RoundToNearestGrid(pos.z)
                    );
            }
            else
            {
                pendingObject.transform.position = pos;
            }
            
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(pendingObject);
                canPlace = false;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
        }
    }
    [PunRPC]
    public void PlaceObject()
    {
        GameObject towerInstanciate = pendingObject.GetComponent<CheckPlacement>().SelectTower();
        Destroy(pendingObject);
        PhotonNetwork.Instantiate(towerInstanciate.name, pos, pendingObject.transform.rotation);
        pendingObject = null;
    }
    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }
    
    private void FixedUpdate()
    {
        Camera cam = Camera.main;
        Ray ray;

        if (cam != null)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                pos = hit.point;
            }
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }

        return pos;
    }
}

