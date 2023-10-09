using System;
using UnityEngine;

public class SaveDateAndShips : MonoBehaviour
{
    public static SaveDateAndShips instance = null;

    public int TotalFlagships;
    public int TotalShips;
    public string SavedDate;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void GetData(SerializableSaveDateAndShips values)
    {
        TotalFlagships = values.TotalFlagships;
        TotalShips = values.TotalShips;
        SavedDate = values.SavedDate;
    }

    public SerializableSaveDateAndShips GetSerializable()
    {
        var output = new SerializableSaveDateAndShips();

        output.TotalFlagships = this.TotalFlagships;
        output.TotalShips = this.TotalShips;
        output.SavedDate = this.SavedDate;

        return output;
    }

    [Serializable]
    public class SerializableSaveDateAndShips
    {
        public int TotalFlagships;
        public int TotalShips;
        public string SavedDate;
    }
}