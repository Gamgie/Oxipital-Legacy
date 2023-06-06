using UnityEngine;

[ExecuteInEditMode]
public class CameraSpoutMngr : MonoBehaviour
{

    #region Public members
    public Klak.Spout.SpoutSender spoutSender;
    public Camera mainCamera;
    public int width;
    public int height;
    public RenderTextureFormat rtFormat;
    #endregion

    #region Private members
    RenderTexture m_renderTexture;
	#endregion

	private void OnEnable()
	{
        width = PlayerPrefs.GetInt("SpoutWidth", 1920);
        height = PlayerPrefs.GetInt("SpoutHeight", 1080);
    }

	// Update is called once per frame
	void Update()
    {
        UpdateTexture();
    }

    void UpdateTexture()
    {
        if(width == 0 || height == 0)
		{
            Debug.LogError("Can't instantiate a render texture with size of 0");
            return;
		}

        // No texture or not a valid one
        if(m_renderTexture == null || width != m_renderTexture.width || height != m_renderTexture.height)
		{
            m_renderTexture = new RenderTexture(width, height, 16, rtFormat);
            m_renderTexture.Create();

            // Udate spout and main camera
            spoutSender.sourceTexture = m_renderTexture;
            mainCamera.targetTexture = m_renderTexture;

            // Disable and enable again to re-init spout plugin
            spoutSender.enabled = false;
            spoutSender.enabled = true;
        }
    }

	private void OnDestroy()
	{
        PlayerPrefs.SetInt("SpoutWidth", width);
        PlayerPrefs.SetInt("SpoutHeight", height);
    }
}
