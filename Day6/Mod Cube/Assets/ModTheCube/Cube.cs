using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private float speedRotate = 10.0f;
    private float scale = 1.5f;
    private float time = 0;
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);
        transform.localScale = Vector3.one * scale;

    }

    void Update()
    {
        Renderer  renderer = GetComponent<Renderer>();
        //Random Color
        float color1 = Random.Range(0.1f, 1.0f);
        float color2 = Random.Range(0.1f, 1.0f);
        float color3 = Random.Range(0.1f, 1.0f);
        float color4 = Random.Range(0.1f, 1.0f);
        float metallic = Random.Range(0.1f, 1.0f);
        Material material = Renderer.material;

        transform.Rotate(0.0f, speedRotate * Time.deltaTime, speedRotate * Time.deltaTime);
        if (Time.time > time)
        {
            time = Time.time + 1;
            material.color = new Color(color1, color2, color3, color4);
            material.SetFloat("_Glossiness", metallic);
        }
    }
}
