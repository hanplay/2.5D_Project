using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveContainer<T> where T : Unit
{
    [SerializeField]
    private Wave<T>[] waveList;
    private int index;

    public void Init()
    {
        for(int i = 0; i < waveList.Length; i++)
        {
            waveList[i].Init();
        }
    }

    public bool IsCurrentWaveEnd()
    {
        if (Wave<T>.State.End == waveList[index].GetState())
            return true;
        else
            return false;
    }

    public void SkipToNextWave()
    {
        index++;
    }

    public Wave<T> GetCurrentWave()
    {
        if (index < waveList.Length)
            return waveList[index];
        else
            return null;
    }

    public bool IsAllWavesEnd()
    {
        if (index < waveList.Length)
            return false;
        else
            return true;
    }

    public void HideAll()
    {
        for(int i = 0; i < waveList.Length; i++)
        {
            waveList[i].Hide();
        }
    }
}
