using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public bool IsSpecial = false;
    public bool Enabled = true;

    public Material _NormalMaterial;
    public Material _SpecialMaterial;
    void Start()
    {
        SetMaterial();
        Enabled = true;
    }

    private void SetMaterial()
    {
        GetComponent<Renderer>().material = IsSpecial ? _SpecialMaterial : _NormalMaterial;
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameController>()._IsPlaying == false) return;

        var cameraPosition = FindObjectOfType<Camera>().transform.position;
        var direction = (cameraPosition - transform.position).normalized;

        GetComponent<Rigidbody>().AddForce(direction * 100f);
    }
}
