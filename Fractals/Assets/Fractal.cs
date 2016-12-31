﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh mesh;
    public Material material;

    public int maxDepth;
    private int depth;

    public float childScale;

    private static Vector3[] childDirections =
    {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back,
        Vector3.down
    };

    private static Quaternion[] childOrientations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,0f,-90f),
        Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f),
        Quaternion.Euler(180f,0,0f)
    };

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.yellow, (float)depth / maxDepth);

        if ( depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
	}

    private IEnumerator CreateChildren ()
    {
        for(int i=0; i < childDirections.Length; i++)
        {
            if(i==6 && depth != 0)
            {
                continue; //only needed for the first cube
            }
            float delay = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(delay);
            new GameObject("Fractal Child").AddComponent<Fractal>()
            .Initialize(this, i);

        }
    }

    private void Initialize(Fractal parent, int childIndex)
    {
        //forwarding everything
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;

        //parent child relation
        transform.parent = parent.transform;

        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex]
            * (0.5f + 0.5f * childScale);

        transform.localRotation = childOrientations[childIndex];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
