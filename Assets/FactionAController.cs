using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionAController : MonoBehaviour
{
    [SerializeField]
    private SpawningBuilding factory_prefab;
    [SerializeField]
    private Transform temp;
    [SerializeField]
    [Range(1, 5)]
    private int number_factories;

    private List<LivingEntity> unit_entities;
    private List<SpawningBuilding> spawning_entities;

    private Vector2 upper_bounds = new Vector2(10, -10);
    private Vector2 lower_bounds = new Vector2(40, -40);

    void Start()
    {
        unit_entities = new List<LivingEntity>();
        spawning_entities = new List<SpawningBuilding>();
        Vector3[] cachedPositions = new Vector3[number_factories]; //Defualts to zero
        for (int i = 0; i < number_factories; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(upper_bounds.x, lower_bounds.x),
                0, Random.Range(upper_bounds.y, lower_bounds.y));
            //Vector3 randomRotation = new Vector3(0, Random.Range(0, 360), 0);
            for (int ii = 0; ii < number_factories; ii++)
            {
                Vector3 dif = randomPos - cachedPositions[ii];
                if (dif.magnitude > 3)
                {
                    randomPos = new Vector3(Random.Range(upper_bounds.x, lower_bounds.x), 0, Random.Range(upper_bounds.y, lower_bounds.y));
                    dif = randomPos - cachedPositions[ii];
                }
            }
            SpawningBuilding b = Instantiate(factory_prefab, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
            b.OnSpawned += OnEntitySpawned;
            spawning_entities.Add(b);
            cachedPositions[i] = randomPos;
        }

    }

    void Update()
    {

    }

    void OnEntitySpawned(LivingEntity spawned)
    {
        unit_entities.Add(spawned);
        if (spawned is RangeUnit)
        {
            RangeUnit unit = (RangeUnits)spawned;
            unit.SetTarget(temp);
        }
        else if (spawned is MeleeUnits)
        {
            MeleeUnits unit = (MeleeUnits)spawned;
            unit.SetTarget(temp);
        }

    }

}
