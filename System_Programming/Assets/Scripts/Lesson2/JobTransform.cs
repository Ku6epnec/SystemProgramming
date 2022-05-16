using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class JobTransform : MonoBehaviour
{
    // создайте задачу типа IJobForTransform, которая будет вращать указанные Transform вокруг своей оси с заданной скоростью.

    [SerializeField] GameObject _hyperCube;
    [SerializeField] GameObject _hyperSphere;
    [SerializeField] float _speed;
    [SerializeField] Vector3 _direction;

    private TransformAccessArray accessArray;
    private JobTransformMovement jobTransformMovement;

    void Start()
    {
        Transform[] objects = new Transform[2];
        objects[0] = _hyperCube.transform;
        objects[1] = _hyperSphere.transform;
        accessArray = new TransformAccessArray(objects);

        jobTransformMovement = new JobTransformMovement(_speed, _direction, Time.deltaTime);


    }

    void Update()
    {
        var handle = jobTransformMovement.Schedule(accessArray);
        handle.Complete();
    }
}

public struct JobTransformMovement : IJobParallelForTransform
{
    private float _speed;
    private Vector3 _direction;
    private float _deltaTime;

    public JobTransformMovement(float speed, Vector3 direction, float deltaTime)
    {
        _speed = speed;
        _direction = direction;
        _deltaTime = deltaTime;
    }

    public void Execute(int index, TransformAccess transform)
    {
        transform.rotation *= Quaternion.Euler(_direction * _speed * _deltaTime);
    }
}
