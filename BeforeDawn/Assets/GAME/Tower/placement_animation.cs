using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placement_animation : MonoBehaviour
{
    public Color placement_color;
    public Vector3 position;
    private Color startcolor;
    private Color startcolor2;
    private Renderer _renderer;
    private UnityEngine.Material[] list;
    private GameObject turret;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        list = _renderer.materials;
    }
    void OnMouseEnter()
    {
        startcolor = _renderer.material.color;
        _renderer.material.color = placement_color;
        startcolor2 = list[1].color;
        list[1].color = placement_color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("you can't place a turret here !");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurret();
        turret = (GameObject)(Instantiate(turretToBuild, transform.position + position, transform.rotation));

    }

    void OnMouseExit()
    {
        _renderer.material.color = startcolor;
        list[1].color = startcolor2;
    }
}
