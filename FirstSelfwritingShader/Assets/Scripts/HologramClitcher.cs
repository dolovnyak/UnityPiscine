using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramClitcher : MonoBehaviour
{
    private Renderer holoRenderer;
    private bool _isGlitched = false;
    private float _glitchTime;
    private float _glitchStartTime;
    private float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        holoRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGlitched && Random.Range(0f, 1f) < 0.0009)
        {
            _isGlitched = true;
            _glitchStartTime = Time.time;
            _glitchTime = Random.Range(.1f, .25f);
            GlitchOn();
        }
        else if (Time.time - _glitchStartTime >= _glitchTime)
        {
            _isGlitched = false;
            GlitchOff();
        }
        rotationX += 0.05f;
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
    }

    void GlitchOn()
    {
        holoRenderer.material.SetFloat("_Amount", 1.0f);
        holoRenderer.material.SetFloat("_Ampliture", Random.Range(10, 500));
        holoRenderer.material.SetFloat("_Speed", Random.Range(1, 10));
    }

    void GlitchOff()
    {
        holoRenderer.material.SetFloat("_Amount", 0f);
    }
}
