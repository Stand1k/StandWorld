using System.Globalization;
using System.Linq;
using UnityEngine;

namespace StandWorld.Utils
{
    public class AutoReferencer<T> : MonoBehaviour where T : AutoReferencer<T>
    {
#if UNITY_EDITOR
        // Викликається один раз після додавання компонента до gameobject
        protected new virtual void Reset()
        {
            // Шукає порожні поля в класі або компоненті
            foreach (var field in typeof(T).GetFields().Where(field => field.GetValue(this) == null))
            {
                Transform obj;
                
                //Приводимо першу букву поля до UpperCase
                string[] words = field.Name.Split();
                words[0] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[0]);
                string fieldName = string.Join(" ", words);
                
                if (transform.name == fieldName)
                {
                    obj = transform;
                }
                else
                {
                    obj = transform.Find(fieldName);
                }

                // Якщо ми знаходимо об'єкт, який має те саме ім'я, що і поле,
                // ми намагаємось отримати компонент, який буде в типі поля, і призначити його
                if (obj != null)
                {
                    field.SetValue(this, obj.GetComponent(field.FieldType));
                }
            }
        }
#endif
    }
}