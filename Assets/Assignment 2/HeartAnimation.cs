using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeartAnimation : MonoBehaviour
{
    public int resolution;
    public PrimitiveType objType;

    public AnimationParams animParams;
    private AnimationObject[] objects;
    
    public delegate Vector2 ParametricEquation(float t);

    [Serializable]
    public struct AnimationParams
    {
        public AnimationParam orbitAngle;
        public AnimationParam offset;
        public AnimationParam scale;
        public AnimationParam speed;
        public AnimationParam hue;
        public AnimationParam saturation;
        public AnimationParam value;
    }

    [Serializable]
    public struct AnimationParam
    {
        public float rangeMin;
        public float rangeMax;
        public float noiseScale;

        public float GetRandom(float t, float offset)
        {
            return Mathf.Lerp(rangeMin, rangeMax, Mathf.PerlinNoise1D(t * noiseScale + offset));
        }
    }
    
    public Vector2 HeartCurve(float t)
    {
        float sin = Mathf.Sin(t);
        float cos = Mathf.Cos(t);
        return new Vector2(Mathf.Sqrt(2) * sin * sin * sin, -cos * cos * cos - cos * cos + 2 * cos);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objects = new AnimationObject[resolution];

        for (int i = 0; i < resolution; i++)
        {
            AnimationObject o = GameObject.CreatePrimitive(objType).AddComponent<AnimationObject>();
            o.gameObject.name = "obj " + i;
            o.Initialize(animParams, HeartCurve, Random.Range(0, 2 * Mathf.PI));

            objects[i] = o;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
