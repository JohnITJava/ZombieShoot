using UnityEngine;
using System.Collections;

public class TransformScripts : MonoBehaviour
{
    public Transform Target;
    public float Speed, RotSpeed;
    public Vector3 My3DVector;
    public Quaternion StartPos;
    public Vector3 RelativePos;

    // Use this for initialization
    void Start ()
    {
        StartPos = transform.rotation;
        
       // GetComponent<Rigidbody>().AddForce(Vector3.forward * Speed, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Телепортация объекта по оси x
        transform.position += new Vector3(0, Speed * Time.deltaTime, 0);

        // Движение объекта вперед со скоростью 1 единица в секунду
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        // Движение объекта вперед в глобальной системе координат
        // xyz = right, up, forward
        transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);

        // Движение объекта вдоль оси Z
        transform.Translate(0, 0, Speed * Time.deltaTime);

        // Движение объекта вперед относительно камеры
        transform.Translate(Vector3.forward * Time.deltaTime * Speed, Camera.main.transform);

        // Движение к цели с указанной скоростью
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);


        /////////////////////// повороты



        // нулевое вращение (вращение равное нулю)
        transform.rotation = Quaternion.identity;
        print(transform.rotation.eulerAngles);

        // поворот в сторону направления (вектора)
        Vector3 relativeDirection = Target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativeDirection);
        print(relativeDirection);

        // Поворот в сторону объекта (трансформа)
        transform.LookAt(Target);

        // Преобразование вектора3 в кватернионы для поворота
        transform.rotation = Quaternion.Euler(My3DVector);

        // Возвращение угла в градусах между двумя направлениями
        float angle = Quaternion.Angle(transform.rotation, Target.rotation);
        print("Угол в градусах " + angle); // вывод на консоль

        // Сфеерическая интерполяция между двумя вращениями с указанной скоростью (повтор вращения) - плавный поворот текущего вращения к заданному
        transform.rotation = Quaternion.Slerp(transform.rotation, Target.rotation, RotSpeed * Time.deltaTime);

        // Создание поворота между двумя векторами.
        // Высчитывание направления
        Vector3 relativePos = Target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, relativePos);

        
        // Вращение объекта. В параметрах указывается вектор, вокруг которого происходит вращение, или его координаты по отдельности.
        // Второй параметр (необязательный) — скорость поворота.
        // Последний — в какой системе координат происходит поворот.
        // По умолчанию задана локальная система координат
        // Вращение объекта со скоростью 1 градус в секунду вокруг вектора, направленного вверх для объекта, в локальной системе координат
        transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);

        // Вращение объекта вокруг вектора, направленного вверх от объекта, в глобальной системе координат
        transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime, Space.Self); 

        // Скорость поворота можно задать отдельным параметром
        transform.Rotate(Vector3.up, RotSpeed * Time.deltaTime);                                 

        // Координаты вектора заданы по отдельности
        transform.Rotate(Time.deltaTime * Speed, 0, 0);   

        // Вращение в 2d пространстве
        var vector = new Vector3(0, 0, 50);
        transform.rotation *= Quaternion.Euler(vector * Time.deltaTime);


        //RotateTowards:
        //Первый параметр – текущее направление вектора, 
        //второй – необходимое направление, 
        //третий – скорость поворота, 
        //четвертый – максимальная длина вектора.
        
        // Направление вектора
        Vector3 relativePos1 = Target.position - transform.position;
        // Вращает указанный вектор в сторону цели 
        Vector3 newDir = Vector3.RotateTowards(transform.forward, relativePos1, RotSpeed * Time.deltaTime, 10F);  
        // Создание поворота
        transform.rotation = Quaternion.LookRotation(newDir);
        // отрисовка луча
        Debug.DrawRay(transform.position, newDir, Color.red);
        
        
        // Вращение обекта вокруг указанной точки и вокруг указанной оси
        // первый параметр точка, второй ось
        transform.RotateAround(Target.position, Vector3.right, RotSpeed * Time.deltaTime);


    }
}
