using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{
    private string saveFolderName = "Save";
    private string saveFolderPath;
    
    
    void Start()
    {
        saveFolderPath = string.Concat(Application.dataPath, "/", saveFolderName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save(Unit player)
    {        
        if(false == File.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
		}
        string saveFilePath = SaveFilePath(player);
        string json = JsonUtility.ToJson(player);
        File.WriteAllText(saveFilePath, json);        
	}

    public void Load(Unit player)
    {
        string saveFilePath = SaveFilePath(player);
        if(File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            JsonUtility.FromJsonOverwrite(json, player);
		}

        else
        {
            
		}
	}

    public void Save(Inventory inventory)
    {
        if (false == File.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }
        string saveFilePath = SaveFilePath(inventory);
        string json = JsonUtility.ToJson(inventory);
        File.WriteAllText(saveFilePath, json);
    }

    public void Load(Inventory inventory)
    {
        string saveFilePath = SaveFilePath(inventory);
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            JsonUtility.FromJsonOverwrite(json, inventory);
        }
        else
        {
            
        }
    }

    private string SaveFilePath(Unit player)
    {
        return string.Concat(saveFolderPath, "/", "dd", ".txt");
    }
	
    private string SaveFilePath(Inventory inventory)
    {
        return string.Concat(saveFolderPath, "/", "inventory.txt");
	}
}
