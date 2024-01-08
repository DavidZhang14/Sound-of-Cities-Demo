using UnityEngine;
[System.Serializable]
public class SerializableStructure
{
    public Vector3Int position;
    public int buildingIndex;
    public CellType type;
    public short pitch;
    public string instrument;
    public short targetGrid;
    public short objectVolume = 100;
    public SerializableStructure(){}
    public SerializableStructure(Vector3Int position, Structure structure) {
        this.position = position;
        type = structure.type;
        if (type != CellType.Road) {
            buildingIndex = structure.buildingIndex;
            pitch = structure.structureSoundEmitter.pitch;
            instrument = structure.structureSoundEmitter.instrument;
            targetGrid = structure.structureSoundEmitter.targetGrid;
            objectVolume = structure.structureSoundEmitter.objectVolume;
        }
        else {
            buildingIndex = 0;
        }
    }
}