using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

[ExecuteAlways]
public class SoftBackdrop : MonoBehaviour
{
    protected const int ParticleCount = 32;
    protected const float Size = 10.0f;

    public Material speckMaterial;
    public Mesh speckMesh;
    public float speckScale = 0.5f;
    public float speckMaxSpeed = 1.0f;

    public Material sphereMaterial;
    public Mesh sphereMesh;

    protected Mesh invertedSphereMesh;

    protected Vector3[] positions;
    protected Vector3[] velocities;
    protected Matrix4x4[] transforms;

    public void Start()
    {
        Init();
    }

    public void OnValidate()
    {
        Init();
    }

    public void Init()
    {
        positions = new Vector3[ParticleCount];
        velocities = new Vector3[ParticleCount];
        transforms = new Matrix4x4[ParticleCount];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = (new Vector3(Random.value, Random.value, Random.value) * 2.0f - new Vector3(1.0f, 1.0f, 1.0f)) * Size;
            velocities[i] = Random.insideUnitSphere * Random.value * speckMaxSpeed;
        }

        if (sphereMesh != null)
        {
            var n = sphereMesh.normals;
            for (int i = 0; i < n.Length; i++)
            {
                n[i] = -n[i];
            }
            var t = sphereMesh.triangles;
            for (int i = 0; i < t.Length / 3; i++)
            {
                var i1 = t[i * 3 + 1];
                t[i * 3 + 1] = t[i * 3 + 2];
                t[i * 3 + 2] = i1;
            }
            if (invertedSphereMesh != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(invertedSphereMesh);
                }
                else
                {
                    DestroyImmediate(invertedSphereMesh);
                }
            }
            invertedSphereMesh = new Mesh
            {
                vertices = sphereMesh.vertices,
                normals = n,
                triangles = t,
            };
        }
    }

    public void OnDestroy()
    {
        if (invertedSphereMesh != null)
        {
            if (Application.isPlaying)
            {
                Destroy(invertedSphereMesh);
            }
            else
            {
                DestroyImmediate(invertedSphereMesh);
            }
        }
    }

    public void Update()
    {
        if (positions == null)
        {
            return;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            var p = positions[i];
            var v = velocities[i];

            p += v * Time.deltaTime;

            if (p.magnitude > Size)
            {
                p = -p.normalized * Size;
            }

            positions[i] = p;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            var dir = positions[i].normalized;
            var size = 1.0f - Mathf.Pow(Mathf.Clamp01(positions[i].magnitude / Size), 4.0f);
            size *= speckScale;
            transforms[i] = Matrix4x4.TRS(transform.TransformPoint(dir * 0.3f), Quaternion.LookRotation(dir), transform.lossyScale * size);
        }

        if (invertedSphereMesh != null && sphereMaterial != null)
        {
            Graphics.DrawMesh(invertedSphereMesh, transform.localToWorldMatrix, sphereMaterial, 0, null, 0, null, false, false);
        }

        if (speckMesh != null && speckMaterial != null)
        {
            speckMaterial.enableInstancing = true;
            Graphics.DrawMeshInstanced(speckMesh, 0, speckMaterial, transforms, transforms.Length, null, ShadowCastingMode.Off, false);
        }
    }

    public void OnDrawGizmos()
    {
        if (positions == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < positions.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.TransformPoint(positions[i]), 0.5f);
        }
    }
}
