#region

using UnityEngine;

#endregion

public class PostProcessGui : MonoBehaviour
{
    private bool _autoFocus = true;

    private float _bloomAmount = 1.0f;

    private float _bloomThreshold = 0.8f;

    private float _dofFocalDepth = 10.0f;

    private float _dofMaxBlur;

    private float _dofMaxDistance = 50.0f;
    private bool _enablePostProcess = true;

    private bool _initialized;

    private float _lensDirtAmount = 1.0f;

    private float _lensFlareAmount = 0.5f;

    private OpaquePostProcess _oppScript;
    private PostProcess _ppScript;

    private float _vignetteAmount = 0.2f;

    private Rect _windowRect = new Rect(360, 330, 300, 530);
    public GameObject MainCamera;

    // Use this for initialization
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_initialized) return;
        _oppScript = MainCamera.GetComponent<OpaquePostProcess>();
        _ppScript = MainCamera.GetComponent<PostProcess>();
        _initialized = true;
    }

    public void PostProcessOn()
    {
        Initialize();

        _enablePostProcess = true;

        _oppScript.enabled = true;

        _ppScript.enabled = true;
        _ppScript.bloomThreshold = _bloomThreshold;
        _ppScript.bloomAmount = _bloomAmount;

        _ppScript.lensFlareAmount = _lensFlareAmount;
        _ppScript.lensDirtAmount = _lensDirtAmount;
        _ppScript.vignetteAmount = _vignetteAmount;


        if (_dofMaxBlur > 12)
            _ppScript.DOFMaxBlur = 16;
        else if (_dofMaxBlur > 6)
            _ppScript.DOFMaxBlur = 8;
        else if (_dofMaxBlur > 3)
            _ppScript.DOFMaxBlur = 4;
        else if (_dofMaxBlur > 1.5)
            _ppScript.DOFMaxBlur = 2;
        else if (_dofMaxBlur > 0.5)
            _ppScript.DOFMaxBlur = 1;
        else
            _ppScript.DOFMaxBlur = 0;
        _ppScript.focalDepth = _dofFocalDepth;
        _ppScript.DOFMaxDistance = _dofMaxDistance;

        _ppScript.AutoFocus = _autoFocus;
    }

    public void PostProcessOff()
    {
        Initialize();

        _enablePostProcess = false;

        _oppScript.enabled = false;
        _ppScript.enabled = false;
    }

    private void Update()
    {
        if (_enablePostProcess)
            PostProcessOn();
        else
            PostProcessOff();
    }

    private void DoMyWindow(int windowId)
    {
        const int offsetX = 10;
        var offsetY = 30;

        _enablePostProcess = GUI.Toggle(new Rect(offsetX, offsetY, 280, 30), _enablePostProcess, "Enable Post Process");
        offsetY += 40;


        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "Bloom Threshold", _bloomThreshold,
            out _bloomThreshold, 0.0f, 2.0f);
        offsetY += 40;

        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "Bloom Amount", _bloomAmount,
            out _bloomAmount, 0.0f, 8.0f);
        offsetY += 60;


        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "Lens Flare Amount", _lensFlareAmount,
            out _lensFlareAmount, 0.0f, 4.0f);
        offsetY += 40;

        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "Lens Dirt Amount", _lensDirtAmount,
            out _lensDirtAmount, 0.0f, 2.0f);
        offsetY += 40;

        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "Vignette Amount", _vignetteAmount,
            out _vignetteAmount, 0.0f, 1.0f);
        offsetY += 60;


        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "DOF Max Blur", _dofMaxBlur,
            out _dofMaxBlur, 0.0f, 16.0f);
        offsetY += 40;

        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "DOF Focal Depth", _dofFocalDepth,
            out _dofFocalDepth, 1.0f, 50.0f);
        offsetY += 40;

        GuiHelper.Slider(new Rect(offsetX, offsetY, 280, 50), "DOF Max Distance", _dofMaxDistance,
            out _dofMaxDistance, 5.0f, 200.0f);
        offsetY += 50;

        _autoFocus = GUI.Toggle(new Rect(offsetX, offsetY, 150, 20), _autoFocus, "Use Auto Focus");
        offsetY += 30;

        if (GUI.Button(new Rect(offsetX + 150, offsetY, 130, 30), "Close")) gameObject.SetActive(false);

        GUI.DragWindow();
    }

    private void OnGUI()
    {
        _windowRect.width = 300;
        _windowRect.height = 510;

        _windowRect = GUI.Window(19, _windowRect, DoMyWindow, "Post Process");
    }
}