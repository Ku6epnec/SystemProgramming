using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;

public class Jobs : MonoBehaviour
{
    /* �������� ������ ���� IJob, ������� ��������� ������ � ������� NativeArray<int> � � 
    ���������� ���������� ��� �������� ����� ������ ������ ������� ����.
    ? �������� ���������� ���� ������ �� �������� ������ � �������� � �������
    ��������� ����������.*/

    private NativeArray<int> nativaArray;

    void Start()
    {
        nativaArray = new NativeArray<int>(10, Allocator.Persistent);

        MyJob myJob = new MyJob() { value = 3, nativaArray = nativaArray};

        Debug.Log("Before");

        RandomizeArrayValue();

        Debug.Log("After");

        myJob.Schedule();

    }

    private void RandomizeArrayValue()
    {
        for (int i = 0; i < nativaArray.Length; i++)
        {
            nativaArray[i] = Random.Range(0,30);
            Debug.Log(nativaArray[i].ToString());
        }
    }

}

public struct MyJob: IJob
{
    public int value;
    public NativeArray<int> nativaArray;
    public void Execute()
    {
        for (int i = 0; i < nativaArray.Length; i++)
        {
            if (nativaArray[i] > 10) nativaArray[i] = 0;
            Debug.Log(nativaArray[i].ToString());
        }
    }
}
