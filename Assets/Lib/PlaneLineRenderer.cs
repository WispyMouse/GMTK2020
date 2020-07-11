using System.Collections.Generic;
using UnityEngine;

public class PlaneLineRenderer : MonoBehaviour
{
    [System.Serializable]
    public struct LineSegment
    {
        public Vector2 p0;
        public Vector2 p1;
        public float width;
        public int colorIndex;
    }

    public Vector3 _planesNormal = Vector3.up;
    [SerializeField]
    private List<Color> _initColorPallet = new List<Color>();

    [SerializeField]
    private List<LineSegment> _segments = new List<LineSegment>();

    public float _defaultWidth = 0.1f;
    private List<Material> _materialPallet = new List<Material>();
    private Mesh _protoSegMesh;
    public void ChangeColorPallet(List<Color> colorPallet)
    {
        _materialPallet = new List<Material>(colorPallet.Count);
        for (int iCol = 0; iCol < colorPallet.Count; iCol++)
            _materialPallet.Add(CreateLineMaterial(colorPallet[iCol]));
    }

    public void ClearSegments()
    {
        _segments.Clear();
    }

    public void AddSegment(Vector2 pnt0, Vector2 pnt1, int colorIndex = 0)
    {
        AddSegment(pnt0, pnt1, _defaultWidth, colorIndex);
    }

    public void AddSegment(Vector2 pnt0, Vector2 pnt1, float width, int colorIndex = 0)
    {
        LineSegment ls = new LineSegment();
        ls.p0 = pnt0;
        ls.p1 = pnt1;
        ls.colorIndex = colorIndex;
        ls.width = width;
        _segments.Add(ls);
    }

    public void CreateProtoSegment()
    {
        _protoSegMesh = new Mesh();
        ;
        List<Vector3> vtx = new List<Vector3> {
       Vector3.right + Vector3.up,
       Vector3.right - Vector3.up,
      -Vector3.right - Vector3.up,
      -Vector3.right + Vector3.up
    };
        _protoSegMesh.SetVertices(vtx);
        _protoSegMesh.SetIndices(new int[] { 0, 1, 2, 3 }, MeshTopology.Quads, 0);
        _protoSegMesh.UploadMeshData(true);
    }

    public void Start()
    {
        ChangeColorPallet(_initColorPallet);
        CreateProtoSegment();
    }

    public void LateUpdate()
    {
        Matrix4x4 matPlaneNormal;
        if (_planesNormal != Vector3.forward)
        {
            matPlaneNormal = Matrix4x4.LookAt(Vector3.zero, _planesNormal, Vector3.forward);
        }
        else
        {
            matPlaneNormal = Matrix4x4.identity;
        }
        Matrix4x4 matObject = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        foreach (var seg in _segments)
        {
            Vector3 delta = seg.p1 - seg.p0;
            Vector3 pos = (seg.p1 + seg.p0) / 2;
            float length = delta.magnitude;
            float lengthExt = length + seg.width;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            Matrix4x4 matLocal = Matrix4x4.TRS(pos, rot, new Vector3(lengthExt, seg.width) / 2);
            Matrix4x4 mat = matObject * matPlaneNormal * matLocal;
            //Debug.DrawLine(seg.p0,seg.p1,colorPallet[seg.colorIndex]);
            Graphics.DrawMesh(_protoSegMesh, mat, _materialPallet[seg.colorIndex], 0);
        }
    }

    public static Material CreateLineMaterial(Color color)
    {
        // Unity has a built-in shader that is useful for drawing
        // simple colored things.
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        Material result = new Material(shader);
        result.hideFlags = HideFlags.HideAndDontSave;
        // Turn on alpha blending
        result.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        result.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        // Turn backface culling off
        result.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        // Turn off depth writes
        result.SetInt("_ZWrite", 0);
        result.color = color;
        return result;
    }
}
