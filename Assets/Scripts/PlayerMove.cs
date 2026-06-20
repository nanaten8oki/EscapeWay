using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // プレイヤーの移動速度
    public float gravity = -9.81f; // 現実世界の重力加速度

    // ────── プレイヤーの視点 ──────

    public Transform cameraTransform; // 視点制御用カメラのtransform
    public float mouseSensitivity = 300f; // 視点制御のマウス感度
    public float minPitch = -90f; // カメラの下方向制限
    public float maxPitch = 90f; //　カメラの上方向制限

    public CharacterController controller; // Characterコントローラー取得用

    private Vector3 velocity; // プレイヤーの現在の移動速度
    private float yaw = 0f; // 水平方向の回転
    private float pitch = 0f;//　垂直方向の回転

    // ────── プレイヤーの視点 ──────


    // Update is called once per frame
    void Update()
    {
        HandleMouseLock();
        HandleMouseMovement();
    }

    // マウスによる視点回転処理
    void HandleMouseLock()
    {
        if(Input.GetMouseButton(0))// マウスの左ボタンが押されている時のみ回転処理
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//　マウスの左右X軸の入力を取得
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;//　マウスの上下Y軸の入力を取得

            yaw += mouseX; // 左右の回転角を足す

            pitch -= mouseY;//　上下の回転角を足す（Y軸は反転）
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);// 上下90度に上下回転を制限

            transform.rotation = Quaternion.Euler(0f, yaw, 0f); // プレイヤーをY軸中心に左右回転させる

            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f); // カメラを親オブジェクトのローカル基準で上下回転

        }
    }

    void HandleMouseMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // AもしくはD（矢印キー）が入力された際にY軸にインクリメント
        float moveZ = Input.GetAxis("Vertical");//　WもしくはS（矢印キー）が入力された際にZ軸にインクリメント

        Vector3 move = transform.right * moveX + transform.forward * moveZ; //　カメラ向きに応じた前後左右の移動ベクトル作成
        controller.Move(move * moveSpeed *Time.deltaTime); //　入力に応じた方向へプレイヤーを移動
        velocity.y += gravity * Time.deltaTime; //　重力の影響を下方向に追加
        controller.Move(velocity * Time.deltaTime); // 垂直方向の移動を適用
    }

}
