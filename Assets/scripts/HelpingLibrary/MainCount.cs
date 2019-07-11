using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCount : MonoBehaviour {

    //Same Delta Time For All Objects
    public static MainCount instance;
    [HideInInspector]
    public float deltaTime;
    [HideInInspector]
    public float fixedDeltaTime;

    private void Awake()
    {
        instance = instance ?? this;
    }

    private void Start()
    {
        instance = instance ?? this;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
    }

    private void FixedUpdate()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    public int IntegerRandom(int from, int to)
    {
        return Random.Range(from, to);
    }

    public float FloatRandom(float from, float to)
    {
        return Random.Range(from, to);
    }

    public bool BoolRandom()
    {
        // return 0 == Random.Range(0, 2);
        return Random.value > 0.5f;
    }
    public int PositiveNegativeRandom()
    {
        return BoolRandom() ? 1 : -1;
    }
    public System.Guid TakeId()
    {
        return System.Guid.NewGuid();
    }

    public int DifferentWeightRandom(int[] weights)
    {
        int randomedValue = IntegerRandom(0, weights.Sum());
        for (int i = 0; i < weights.Length; i++)
        {
            randomedValue -= weights[i];
            if (randomedValue <= 0)
            {
                return i;
            }
        }
        return weights.Length;
    }
}
