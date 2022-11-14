using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    /// <summary>
    /// loads data from the save file
    /// loads default data if no save file was found
    /// </summary>
    /// <param name="data"></param>
    void LoadData(GameData data);

    /// <summary>
    /// saves the gamedata upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    void SaveData(ref GameData data);
}
