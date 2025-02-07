﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public int buildingIndex;
    public CellType type;
    float yHeight = 0;
    public StructureSoundEmitter soundEmitter;

    public void CreateModel(GameObject model)
    {
        GameObject structure = Instantiate(model, transform);
        yHeight = structure.transform.position.y;
        soundEmitter = structure.GetComponent<StructureSoundEmitter>();
    }

    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
        structure.transform.localRotation = rotation;
    }
}
