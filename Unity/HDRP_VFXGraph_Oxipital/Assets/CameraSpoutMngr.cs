using UnityEngine;

[ExecuteInEditMode]
public class CameraSpoutMngr : MonoBehaviour
{
    public enum TextureResolution
    {
        Hd,
        FullHD,
        Lablab,
        Square1280,
        Custom 
    }

    #region Public members
    public Klak.Spout.SpoutSender spoutSender;
    public Camera mainCamera;
    public RenderTexture[] renderTextureList;
    #endregion

    #region Private members
    int _textureIndexSelected;
    #endregion

    public TextureResolution spoutTextureResolution;

    // Update is called once per frame
    void Update()
    {
        UpdateTexture();
    }

    void UpdateTexture()
    {
        int newIndex = -1;
        // look for corresponding texture index
        switch (spoutTextureResolution)
        {
            case TextureResolution.Hd:
                newIndex = 0;
                break;
            case TextureResolution.FullHD:
                newIndex = 1;
                break;
            case TextureResolution.Lablab:
                newIndex = 2;
                break;
            case TextureResolution.Square1280:
                newIndex = 3;
                break;
            case TextureResolution.Custom:
                newIndex = 4;
                break;
        }
        
        // If it's different from actual, then update
        if(newIndex != _textureIndexSelected)
        {
            _textureIndexSelected = newIndex;

            // Udate spout and main camera
            spoutSender.sourceTexture = renderTextureList[_textureIndexSelected];
            mainCamera.targetTexture = renderTextureList[_textureIndexSelected];

            // Disable and enable again to re-init spout plugin
            spoutSender.enabled = false;
            spoutSender.enabled = true;
        }

        
    }
}
