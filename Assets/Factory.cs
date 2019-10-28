using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : SpawningBuilding
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    public GameObject unit_to_spawn;

    [SerializeField]
    private float ms_between_possible_spawns = 10000f;

    private float next_spawn_time = 0;
    private bool should_spawn = true;

    protected override void Update()
    {
        base.Update();
        Spawn();
    }

    private void Spawn()
    {
        if (Time.time > next_spawn_time && should_spawn)
        {
            next_spawn_time = Time.time + ms_between_possible_spawns / 1000;
            GameObject spawned = Instantiate(unit_to_spawn, spawnPoint.position, Quaternion.identity);
            should_spawn = false;
            //PostSpawnEvent(spawned as LivingEntity);
        }
    }

    public override void QueueUnit()
    {
        should_spawn = true;
    }

}
