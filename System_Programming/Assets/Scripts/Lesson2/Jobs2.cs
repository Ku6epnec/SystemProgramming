using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;

public class Jobs2 : MonoBehaviour
{
    /* Cоздайте задачу типа IJobParallelFor, которая будет принимать данные в виде двух 
    контейнеров: Positions и Velocities — типа NativeArray<Vector3>. Также создайте 
    массив FinalPositions типа NativeArray<Vector3>.
    ? Сделайте так, чтобы в результате выполнения задачи в элементы массива 
    FinalPositions были записаны суммы соответствующих элементов массивов Positions и 
    Velocities.
    ? Вызовите выполнение созданной задачи из внешнего метода и выведите в консоль 
    результат выполнения.*/

    private NativeArray<Vector3> positionArray;
    private NativeArray<Vector3> velocitiesArray;
    private NativeArray<Vector3> finalArray;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            JobDo();
        }
    }

    private void JobDo()
    {
        positionArray = new NativeArray<Vector3>(new Vector3[] { Vector3.up, Vector3.right, Vector3.forward }, Allocator.Persistent);
        velocitiesArray = new NativeArray<Vector3>(new Vector3[] { Vector3.up, Vector3.right, Vector3.forward }, Allocator.Persistent);
        finalArray = new NativeArray<Vector3>(new Vector3[] { Vector3.up, Vector3.right, Vector3.forward }, Allocator.Persistent);

        MyJobParallelFor newJob = new MyJobParallelFor() { positionArray = positionArray, velocitiesArray = velocitiesArray };

        Debug.Log("PositionArray: ");

        RandomizeArrayValue(positionArray);

        Debug.Log("VelocitiesArray: ");

        RandomizeArrayValue(velocitiesArray);

        JobHandle jobHandle = newJob.Schedule(positionArray.Length, default);
        jobHandle.Complete();

        Debug.Log("After");

        for (int i = 0; i < finalArray.Length; i++)
        {
            finalArray[i] = positionArray[i];
            Debug.Log(finalArray[i].ToString());
        }

        positionArray.Dispose();
        velocitiesArray.Dispose();
        finalArray.Dispose();
    }

    private void RandomizeArrayValue(NativeArray<Vector3> nativaArray)
    {
        for (int i = 0; i < nativaArray.Length; i++)
        {
            nativaArray[i] = new Vector3(Random.Range(0, 30), Random.Range(0, 30), Random.Range(0, 30));
            Debug.Log(nativaArray[i].ToString());
        }
    }
}

public struct MyJobParallelFor: IJobParallelFor
{
    public NativeArray<Vector3> positionArray;
    public NativeArray<Vector3> velocitiesArray;

    public void Execute(int index)
    {
        positionArray[index] += velocitiesArray[index];
    }
}
