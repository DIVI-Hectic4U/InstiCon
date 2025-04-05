using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;
[ExecuteInEditMode]
public class RenderTextureBaker : MonoBehaviour
{
    
    [Tooltip("Orthographic Camera")]
    public Camera camToDrawWith;
    // layer to render
    [SerializeField]
    LayerMask layer;
    // objects to render
    [Tooltip("Used to determine the camera's size depending on the mesh's dimensions ")]

    [SerializeField]
    Renderer[] renderers;
    // unity terrain to render
    
    [SerializeField]
    Terrain[] terrains;
    // map resolution
    public int resolution = 512;

    // update texture every frame
    [SerializeField]
    bool RealTimeDiffuse;
    RenderTexture tempTex;

    private Bounds bounds;
    // resolution of the map

    // Start is called before the first frame update

    public void GetBounds()
    {
        bounds = new Bounds(transform.position, Vector3.zero);
        if (renderers.Length > 0)
        {
            foreach (Renderer renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }
        }

        if (terrains.Length > 0)
        {
            foreach (Terrain terrain in terrains)
            {
                bounds.Encapsulate(terrain.terrainData.bounds);
            }
        }
    }
    public void ReDrawDiffuseMap()
    {
        tempTex = new RenderTexture(resolution, resolution, 24);
        DrawDiffuseMap();
    }



    void Start()
    {     
        ReDrawDiffuseMap();
    }

    void OnRenderObject()
    {
        if (!RealTimeDiffuse)
        {
            return;
        }
        UpdateTex();
    }

    void UpdateTex()
    {
        camToDrawWith.enabled = true;
        camToDrawWith.targetTexture = tempTex;
        Shader.SetGlobalTexture("TB_DEPTH", tempTex);
    }
    public void DrawDiffuseMap()
    {
        DrawToMap("TB_DEPTH");
    }
    private void OnDestroy()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext SRC, Camera camera)
    {
       // Unity reverted the obsolete status and RenderSingleCamera will soon be usable again while they are looking for a better solution.
        UniversalRenderPipeline.RenderSingleCamera(SRC, camToDrawWith);
    }

    void DrawToMap(string target)
    {
        camToDrawWith.enabled = true;
        camToDrawWith.targetTexture = tempTex;
        camToDrawWith.depthTextureMode = DepthTextureMode.Depth;

        //the total width of the bounding box of our cameras view
        Shader.SetGlobalFloat("TB_SCALE", GetComponent<Camera>().orthographicSize * 2);
        //find the bottom corner of the texture in world scale by subtracting the size of the camera from its x and z position
        Shader.SetGlobalFloat("TB_OFFSET_X", camToDrawWith.transform.position.x - camToDrawWith.orthographicSize);
        Shader.SetGlobalFloat("TB_OFFSET_Z", camToDrawWith.transform.position.z - camToDrawWith.orthographicSize);
        //we'll also need the relative y position of the camera, lets get this by subtracting the far clip plane from the camera y position
        Shader.SetGlobalFloat("TB_OFFSET_Y", camToDrawWith.transform.position.y - camToDrawWith.farClipPlane);
        //we'll also need the far clip plane itself to know the range of y values in the depth texture
        Shader.SetGlobalFloat("TB_FARCLIP", camToDrawWith.farClipPlane);

        //camToDrawWith.Render();
        RenderPipeline.beginCameraRendering += UpdateCamera; ;

        Shader.SetGlobalTexture(target, tempTex);
        camToDrawWith.enabled = false;
    }
    float CalculateOrthographicSize()
    {
        //return bounds.size.magnitude;
        var orthographicSize = camToDrawWith.orthographicSize;

        Vector2 min = bounds.min;
        Vector2 max = bounds.max;

        var width = (max - min).x ;
        var height = (max - min).y ;

        if (width > height)
        {
            orthographicSize = Mathf.Abs(width) / camToDrawWith.aspect / 2f;
        }
        else
        {
            orthographicSize = Mathf.Abs(height) / 2f;
        }

        return Mathf.Max(orthographicSize, 1f);
    }
    public void SetUpCam()
    {
        transform.position = Vector3.zero;
        camToDrawWith.transform.rotation = Quaternion.Euler(90f, 0, 0);

        if (camToDrawWith == null)
        {
            camToDrawWith = GetComponentInChildren<Camera>();
        }
       
        camToDrawWith.cullingMask = layer;
        camToDrawWith.orthographicSize = CalculateOrthographicSize();
        camToDrawWith.transform.parent = null;
        camToDrawWith.transform.position = bounds.center + new Vector3(0, bounds.extents.y + 3f, 0);
        camToDrawWith.transform.parent = gameObject.transform;
        camToDrawWith.enabled = false;
    }

}