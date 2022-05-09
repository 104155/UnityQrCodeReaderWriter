using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class QrCodeWriter : MonoBehaviour {

    [SerializeField]
    private RawImage _rawImageReceiver;
    [SerializeField]
    private Text _textOutputField;
    private UserIdGenerator _userIdGenerator;
    private Texture2D _storeEncodeTexture;

    // Start is called before the first frame update
    void Start() {
        _userIdGenerator = new UserIdGenerator();
        _storeEncodeTexture = new Texture2D(256, 256);
        _textOutputField = GameObject.Find("TextGeneratedUserId").GetComponent<Text>();
    }

    private Color32[] Encode(string _textForEncoding, int _width, int _height) {
        BarcodeWriter _writer = new BarcodeWriter {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions {
                Height = _height,
                Width = _width
            }
        };
        return _writer.Write(_textForEncoding);
    }

    public void EncodeTextToQrCode() {
        string _userId = _userIdGenerator.GenerateUserId();

        Color32[] _convertToPixelTexture = Encode(_userId, _storeEncodeTexture.width, _storeEncodeTexture.height);
        _storeEncodeTexture.SetPixels32(_convertToPixelTexture);
        _storeEncodeTexture.Apply();

        _rawImageReceiver.texture = _storeEncodeTexture;
        _textOutputField.text = _userId;
    }

    public void OnClickEncode() {
        EncodeTextToQrCode(); 
    }
}
