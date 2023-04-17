using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.VFX;

public class ForceController : MonoBehaviour
{
    public int forceID = 0;
    public int forceCount = 1;
    
    [Space(10)]
    public float intensity;
    public float radius;
    public Vector3 axis;

    protected BalletPattern pattern; // Handle positions of the force
    protected VisualEffect[] m_vfxs;
    protected string m_suffix = "";
    protected GraphicsBuffer m_buffer;
    
    private BalletManager m_balletMngr;
    private OrbsManager m_orbsMngr;
    private BalletPatternController m_patternController;


    private void Start()
    {
        m_orbsMngr = GameObject.FindGameObjectWithTag("Orb Manager").GetComponent<OrbsManager>();

        // Listen to vfxController to know when we created a new orb.
        // Update our list when it is the case
        if(m_orbsMngr != null)
		{
            m_orbsMngr.GetOnOrbCreated().AddListener(UpdateVfxArray);
        }
        else
		{
            Debug.LogError("Can not find Orbs Manager in " + gameObject.name);
		}

        // Set suffix to handle same multiple force on the same object.
        if (forceID != 0)
        {
            m_suffix = " " + forceID.ToString();
        }

        // Get ballet manager reference
        m_balletMngr = GameObject.FindGameObjectWithTag("Ballet Manager").GetComponent<BalletManager>();
        if(m_balletMngr != null)
		{
            // Add a pattern to ballet manager
            pattern = m_balletMngr.AddPattern(BalletManager.PatternGroup.Force);
            if(pattern != null)
			{
                m_patternController = this.gameObject.AddComponent<BalletPatternController>();
                m_patternController.SetPattern(pattern);
			}
        }
        else
		{
            Debug.LogError("Can't find Ballet Manager in " + gameObject.name);
		}

        if (m_buffer == null)
		{
            m_buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 1, Marshal.SizeOf(typeof(Vector3)));
        }
    }

    protected virtual void Update()
    {
        if(m_vfxs == null)
		{
            UpdateVfxArray();
		}

        // Update pattern dancer count
        if(pattern != null && forceCount != pattern.dancerCount)
		{
            pattern.UpdateDancerCount(forceCount);
            m_buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, forceCount, Marshal.SizeOf(typeof(Vector3)));
        }     

        // Update positions
        if (m_buffer != null)
		{
            List<Vector3> target = GetPositions();
            if(target != null)
                m_buffer.SetData(GetPositions());
        }

        foreach (VisualEffect vfx in m_vfxs)
        {
            if (vfx == null)
                UpdateVfxArray();
        }
    }

    void UpdateVfxArray()
    {
        if (m_orbsMngr == null)
        {
            m_vfxs = GetComponentsInChildren<VisualEffect>();
        }
        else
        {
            m_vfxs = m_orbsMngr.GetComponentsInChildren<VisualEffect>();
        }
    }

    protected List<Vector3> GetPositions()
	{
        List<Vector3> targetPositions = new List<Vector3>();

        if (pattern == null)
            return null;

        for(int i = 0; i < pattern.dancerCount; i++)
		{
            targetPositions.Add(pattern.GetPosition(i));
		}

        return targetPositions;
	}

	private void OnDestroy()
	{
        if(m_buffer != null)
            m_buffer.Release();
	}
}
