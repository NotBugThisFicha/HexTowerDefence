using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

public class MoveableSpawner : IInitializable
{
    private readonly PoolerNonMono pooler;
    private readonly Settings settings;
    private float timeWave;

    public MoveableSpawner(PoolerNonMono pooler, Settings settings)
    {
        this.pooler = pooler;
        this.settings = settings;
    }

    public void Initialize()
    {
        MainThreadDispatcher.StartUpdateMicroCoroutine(DecriseTimeWave());
        MainThreadDispatcher.StartCoroutine(SpawnEnemy());
    }

    public Moveable CreateMoveable<T>(Vector2 pos) where T: Moveable
    {
        Moveable moveable = (Moveable)pooler.SpawnFromPool<T>(pos, Quaternion.identity);
        return moveable;
    }
         

    private IEnumerator DecriseTimeWave()
    {
        while(timeWave >= 0)
        {
            timeWave -= Time.deltaTime;
            if (settings.countWaves <= 0)
                yield break;
            yield return null; 
        }
    }
    private IEnumerator SpawnEnemy()
    {
        while (settings.countWaves > 0)
        {
            yield return Observable.Timer(TimeSpan.FromSeconds(settings.intervalSpawnEnemy)).ToYieldInstruction();
            CreateMoveable<Enemy>(CalculateFromRadiusPoint()).SetDirectionTo(Vector2.zero);
            if(timeWave <= 0)
            {
                yield return Observable.Timer(TimeSpan.FromSeconds(4)).ToYieldInstruction();
                timeWave = settings.timeWave;
                settings.countWaves--;
            }    
        }
        yield break;
    }

    private Vector2 CalculateFromRadiusPoint()
    {
        Vector2 truePos;
        while(true)
        {
            truePos = new Vector2(
                UnityEngine.Random.Range(-settings.boundaryX, settings.boundaryX), 
                UnityEngine.Random.Range(-settings.boundaryY, settings.boundaryY));

            if (Vector2.Distance(Vector2.zero, truePos) > settings.radiusSpawn)
                break;
        }
        return truePos;
    }

    [Serializable]
    public class Settings
    {
        public int countWaves;
        public float timeWave;
        public float intervalSpawnEnemy;
        public float radiusSpawn;
        public float boundaryX;
        public float boundaryY;
    }
}
