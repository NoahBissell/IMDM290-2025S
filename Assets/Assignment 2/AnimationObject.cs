using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    private HeartAnimation.ParametricEquation equation;
    private float t;
    private MeshRenderer mr;
    private HeartAnimation.AnimationParams p;
    private float randomOffset;
    private Vector2 prevPosition;
    private Vector2 vel;

    private Vector2 prevDir;
    
    public void Initialize(HeartAnimation.AnimationParams animParams, HeartAnimation.ParametricEquation equation, float start)
    {
        p = animParams;
        this.equation = equation;
        t = start;
        this.p = animParams;
        mr = GetComponent<MeshRenderer>();

        randomOffset = Random.Range(0, 1000);
    }
    
    // Update is called once per frame
    void Update()
    {
        float angle = p.orbitAngle.GetRandom(t, randomOffset);
        float speed = p.speed.GetRandom(t, randomOffset);
        float offset = p.offset.GetRandom(t, randomOffset);
        float scale = p.scale.GetRandom(t, randomOffset);
        float hue = p.hue.GetRandom(t, randomOffset);
        float saturation = p.saturation.GetRandom(t, randomOffset);
        float value = p.value.GetRandom(t, randomOffset);
        
        
        t += Time.deltaTime * speed;
        
        Vector2 curvePos = equation(t);
        Vector2 dir = (curvePos - prevPosition).normalized;

        dir = Vector2.SmoothDamp(prevDir, dir, ref vel, .2f);
        
        Vector3 normal = new Vector3(dir.y, -dir.x, 0);
        Vector3 rotatedNormal = Quaternion.AngleAxis(angle, dir) * normal;
        
        mr.material.color = Color.HSVToRGB(hue, saturation, value);

        transform.localScale = Vector3.one * scale;

        transform.position = (Vector3)curvePos + rotatedNormal * offset;
        
        prevPosition = curvePos;
        prevDir = dir;
    }
}
