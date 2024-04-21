using Cinemachine;
using QFramework;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineImpulseSource mImpulseSource; // 脉冲源

    public static EasyEvent ShakeCamera = new EasyEvent();
    
    void Start()
    {
        mImpulseSource = GetComponent<CinemachineImpulseSource>(); // 缓存脉冲源 用作相机抖动效果
        ShakeCamera.Register(() =>
        {
            mImpulseSource.GenerateImpulse(); // 生成脉冲信号
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }
}
