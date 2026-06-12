using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // プレイヤーの移動速度
    public float gravity = -9.81f; // 現実世界の重力加速度

    public Transform cameraTransform; // 視点制御用カメラのtransform
    public float mouseSensitivity = 300f; // 視点制御のマウス感度
    public float minPitch = -90f; // カメラの下方向制限
    public float maxPitch = 90f; //　カメラの上方向制限

    public CharacterController Controller; // Characterコントローラー取得用

    private Vector3 velocity; // プレイヤーの現在の移動速度
    private float yaw = 0f; // 水平方向の回転
    private float pitch = 0f;//　垂直方向の回転


    // Update is called once per frame
    void Update()
    {
        
    }
}
