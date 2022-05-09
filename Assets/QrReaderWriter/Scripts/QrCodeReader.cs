using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QrCodeReader : MonoBehaviour {
    [SerializeField]
    private RawImage _rawImageBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private Text _textScanOutput;
    [SerializeField]
    private RectTransform _scanZone;

    private bool _isCamAvailable;
    private WebCamTexture _webCamTexture;

    // Start is called before the first frame update
    void Start() {
        SetupCamera();
    }

    // Update is called once per frame
    void Update() {
        UpdateCameraRender();
    }

    private void SetupCamera() {
        WebCamDevice[] _devices = WebCamTexture.devices;

        if (_devices.Length == 0) {
            _isCamAvailable = false;
            return;
        }
        for (int i = 0; i < _devices.Length; i++) {
            ////For moblile
            //if (!_devices[i].isFrontFacing) {
            //    _webCamTexture = new WebCamTexture(_devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
            //}
            _webCamTexture = new WebCamTexture(_devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
        }

        _webCamTexture.Play();
        _rawImageBackground.texture = _webCamTexture;
        _isCamAvailable = true;
    }

    private void UpdateCameraRender() {
        if (!_isCamAvailable) {
            return;
        }
        float _ratioHeightWidth = (float)_webCamTexture.width / (float)_webCamTexture.height;
        _aspectRatioFitter.aspectRatio = _ratioHeightWidth;

        int _orientation = _webCamTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, _orientation);
    }

    private void Scan() {
        try {
            IBarcodeReader _barcodeReader = new BarcodeReader();
            Result _result = _barcodeReader.Decode(_webCamTexture.GetPixels32(), _webCamTexture.width, _webCamTexture.height);
            if (_result != null) {
                _textScanOutput.text = _result.Text;
                _textScanOutput.color = new Color32(15, 255, 0, 255);
            } else {
                _textScanOutput.text = "FAILED TO READ QR CODE";
                _textScanOutput.color = new Color32(255, 0, 0, 255);
            }
        } catch {
            _textScanOutput.text = "'TRY' IN SCAN FAILED";
            _textScanOutput.color = new Color32(255, 0, 0, 255);
        }
    }

    public void OnClickScan() {
        Scan();
    }
}
