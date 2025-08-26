using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public float[] position = new float[3];
    public float[] rotation = new float[4];

    public PlayerSave() { }
    public PlayerSave(float[] position, float[] rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
