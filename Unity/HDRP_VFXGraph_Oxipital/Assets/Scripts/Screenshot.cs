using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Screenshot : MonoBehaviour
{
    public string filepath;
    public string filename;
    public int supersize;

    private int fileIndex = 0;

    private void OnEnable()
    {
        FileIndexSetup();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        string newScreenCaptureName = filepath + "\\" + filename + "." + fileIndex + ".png";

        while (File.Exists(newScreenCaptureName))
        {
            fileIndex++;
            newScreenCaptureName = filepath + "\\" + filename + "." + fileIndex + ".png";
        }
            

        ScreenCapture.CaptureScreenshot(newScreenCaptureName, supersize);
        fileIndex++;
    }

    void FileIndexSetup()
    {
        fileIndex = 0;

        string screenCaptureFileName = filepath + "\\" + filename + ".";

        foreach (string file in System.IO.Directory.GetFiles(filepath))
        {
            string actualFileNumber = file.Substring(screenCaptureFileName.Length);
            actualFileNumber = actualFileNumber.Remove(actualFileNumber.Length - 4);
            int actualFileIndex = Convert.ToInt32(actualFileNumber);
            if (actualFileIndex > fileIndex)
                fileIndex = actualFileIndex + 1;
        }
    }
}
