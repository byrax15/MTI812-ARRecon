using System;
using System.Collections;
using System.Globalization;
using System.Text;
using SimpleFileBrowser;
using UnityEngine;

public partial class RawPointCloudBlender
{
    
    public bool Paused;
    private MeshFilter _meshFilter;

    public void TogglePause() => Paused = !Paused;

    public void ExportAsXyzRgb()
    {
        try
        {
            StartCoroutine(StartSaveRoutine());
        }
        catch (Exception e)
        {
            ErrorOut.ShowError(e);
        }
    }

    private IEnumerator StartSaveRoutine()
    {
        var pausedBak = Paused;
        try
        {
            Paused = true;
            yield return QueryExportPath();
            FileBrowserHelpers.WriteTextToFile(FileBrowser.Result[0], PointCloudToString());
        }
        finally
        {
            Paused = pausedBak;
        }
    }

    private IEnumerator QueryExportPath()
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files);
        if (!FileBrowser.Success)
            ErrorOut.ShowError("Save cancelled");
    }

    private string PointCloudToString()
    {
        var builder = new StringBuilder();
        var confidence = ConfidenceSlider.value;

        for (var n = 0; n < _verticesCount; ++n)
        {
            var i = (_verticesIndex + n) % _maxVerticesInBuffer;

            if (_colors[i].a < confidence * 255f)
                continue;

            builder.Append(_vertices[i].x.ToString(CultureInfo.InvariantCulture));
            builder.Append(" ");
            builder.Append(_vertices[i].y.ToString(CultureInfo.InvariantCulture));
            builder.Append(" ");
            builder.Append(_vertices[i].z.ToString(CultureInfo.InvariantCulture));
            builder.Append(" ");
            builder.Append(_colors[i].r.ToString(CultureInfo.InvariantCulture));
            builder.Append(" ");
            builder.Append(_colors[i].g.ToString(CultureInfo.InvariantCulture));
            builder.Append(" ");
            builder.Append(_colors[i].b.ToString(CultureInfo.InvariantCulture));
            builder.Append("\n");
        }

        return builder.ToString();
    }

}