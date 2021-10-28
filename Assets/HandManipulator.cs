using UnityEngine;

public class HandManipulator : MonoBehaviour
{
    [Header("Horizontal")] 
    [SerializeField] private Transform _base;
    [SerializeField] private SliderJoint2D _horizontalSlider;
    [SerializeField] private float _horizontalSpeed = 2f;

    [Header("Vertical")]
    [SerializeField] private Transform _head;
    [SerializeField] private SliderJoint2D _verticalSlider;
    [SerializeField] private LineRenderer _headLine;
    [SerializeField] private float _verticalSpeed = 2f;

    [Header("Finger")]
    [SerializeField] private HingeJoint2D _fingerLeftMiddleHinge;
    [SerializeField] private HingeJoint2D _fingerLeftEndHinge;
    [SerializeField] private HingeJoint2D _fingerRightMiddleHinge;
    [SerializeField] private HingeJoint2D _fingerRightEndHinge;

    [SerializeField] private float _fingersAngularSpeed = 90f;

    private bool _isOpen = true;
    
    void Start()
    {
        ResolveFingersState();
    }

    private void Update()
    {
        ResolveHeadLine();
        ProcessHorizontal();
        ProcessVertical();
        ProcessFingersState();
    }

    private void ProcessHorizontal()
    {
        var horizontalSpeed = 0f;
        if (Input.GetKey(KeyCode.A)) horizontalSpeed += _horizontalSpeed;
        if (Input.GetKey(KeyCode.D)) horizontalSpeed -= _horizontalSpeed;
        var horizontalMotor = _horizontalSlider.motor;
        horizontalMotor.motorSpeed = horizontalSpeed;
        _horizontalSlider.motor = horizontalMotor;
    }

    private void ProcessVertical()
    {
        var verticalSpeed = 0f;
        if (Input.GetKey(KeyCode.S)) verticalSpeed += _verticalSpeed;
        if (Input.GetKey(KeyCode.W)) verticalSpeed -= _verticalSpeed;
        var verticalMotor = _verticalSlider.motor;
        verticalMotor.motorSpeed = verticalSpeed;
        _verticalSlider.motor = verticalMotor;
    }

    private void ProcessFingersState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isOpen = !_isOpen;
            ResolveFingersState();
        }
    }

    private void ResolveHeadLine()
    {
        _headLine.SetPosition(0, _base.localPosition);
        _headLine.SetPosition(1, _head.localPosition);
    }
    
    private void ResolveFingersState()
    {
        var fingerLeftMiddleMotor = _fingerLeftMiddleHinge.motor;
        fingerLeftMiddleMotor.motorSpeed = _isOpen ? _fingersAngularSpeed : -_fingersAngularSpeed;
        _fingerLeftMiddleHinge.motor = fingerLeftMiddleMotor;
        
        var fingerLeftEndMotor = _fingerLeftEndHinge.motor;
        fingerLeftEndMotor.motorSpeed = _isOpen ? _fingersAngularSpeed : -_fingersAngularSpeed;
        _fingerLeftEndHinge.motor = fingerLeftEndMotor;
        
        var fingerRightMiddleMotor = _fingerRightMiddleHinge.motor;
        fingerRightMiddleMotor.motorSpeed = _isOpen ? -_fingersAngularSpeed : _fingersAngularSpeed;
        _fingerRightMiddleHinge.motor = fingerRightMiddleMotor;
        
        var fingerRightEndMotor = _fingerRightEndHinge.motor;
        fingerRightEndMotor.motorSpeed = _isOpen ? -_fingersAngularSpeed : _fingersAngularSpeed;
        _fingerRightEndHinge.motor = fingerRightEndMotor;
    }

}
