using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh[] meshes;
    public Material material;

    public int maxDepth;
    private int depth;

    public float childScale;

    public float spawnProbability;

    public float maxRotationSpeed;
    private float rotationSpeed;

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

    private Material[,] materials;

    private void InitializeMaterials()
    {
        materials = new Material[maxDepth + 1, 2];
        for (int i = 0; i < materials.GetLength(0); i++)
        {
            float t = (float)i / (maxDepth - 1f);
            t *= t;
            materials[i, 0] = new Material(material);
            materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
        }
        materials[maxDepth, 0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.red;
    }

	// Use this for initialization
	void Start () {
        if(materials == null)
        {
            InitializeMaterials();
        }

        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0,meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth,Random.Range(0,2)];
        //instead of this which whould silently produces duplicates
        //GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.yellow, (float)depth / maxDepth);x

        if ( depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }

        rotationSpeed = Random.Range(0f, maxRotationSpeed);
	}

    private IEnumerator CreateChildren ()
    {
        for(int i=0; i < childDirections.Length; i++)
        {
            if(i==childDirections.Length-1 && depth != 0)
                continue; //only needed for the first cube

            if (Random.value >= spawnProbability)
                continue;

            float delay = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(delay);
            new GameObject("Fractal Child").AddComponent<Fractal>()
            .Initialize(this, i);

        }
    }

    private void Initialize(Fractal parent, int childIndex)
    {
        //forwarding everything
        meshes = parent.meshes;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        spawnProbability = parent.spawnProbability;
        maxRotationSpeed = parent.maxRotationSpeed;

        //parent child relation
        transform.parent = parent.transform;

        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex]
            * (0.5f + 0.5f * childScale);

        transform.localRotation = childOrientations[childIndex];
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}
}
