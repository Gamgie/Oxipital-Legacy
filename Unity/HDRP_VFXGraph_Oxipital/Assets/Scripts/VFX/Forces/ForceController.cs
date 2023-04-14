using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class ForceController : MonoBehaviour
{

    public int forceID = 0;
    public OrbsManager orbs;
    public Transform targetObject;
    [Space(10)]
    public float intensity;
    public float radius;
    public Vector3 axis;
    public bool useVector3;
    public Vector3 targetVector3;
    

    protected VisualEffect[] m_vfxs;
    protected string m_suffix = "";
    protected List<Vector3> m_targetPositions;
    protected GraphicsBuffer m_buffer;
    protected static readonly int s_BufferID = Shader.PropertyToID("Attractor Graphics Buffer");

    /*[VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    struct GraphicBufferData
    {
        public Vector3 position;
    }*/

    private void Start()
    {
        // Listen to vfxController to know when we created a new orb.
        // Update our list when it is the case
        orbs.GetOnOrbCreated().AddListener(UpdateVfxArray);

        // Set suffix to handle same multiple force on the same object.
        if (forceID != 0)
        {
            m_suffix = " " + forceID.ToString();
        }

        if(m_buffer == null)
		{
            m_buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 2, Marshal.SizeOf(typeof(Vector3)));
            var data = new List<Vector3>()
            {
                new Vector3(5,0,0),
                new Vector3(-5,0,0)
            };
            m_buffer.SetData(data);
        }
        
    }

    protected virtual void Update()
    {
        if(m_vfxs == null)
		{
            UpdateVfxArray();
		}

        foreach(VisualEffect vfx in m_vfxs)
        {
            if (vfx == null)
                UpdateVfxArray();
        }
    }

    void UpdateVfxArray()
    {
        if (orbs == null)
        {
            m_vfxs = GetComponentsInChildren<VisualEffect>();
        }
        else
        {
            m_vfxs = orbs.GetComponentsInChildren<VisualEffect>();
        }
    }
}
