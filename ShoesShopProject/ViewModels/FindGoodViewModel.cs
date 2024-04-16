using System.Collections;

using System.Reflection;
using System.Text;

namespace ShoesShopProject.ViewModels;

public record class FindGoodViewModel
{

    public string minPrice { get; set; } = string.Empty;
    public string isSaleOnly { get; set; } = string.Empty;
    public string maxPrice { get; set; } = string.Empty;
    public List<string> categories { get; set; }
    public List<string> sizes { get; set; }
    public List<string> brands { get; set; }
    public string gender { get; set; }
    public string SerializeFieldsToString()
    {
        StringBuilder serializedFields = new StringBuilder();
        PropertyInfo[] properties = GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            string fieldName = property.Name;
            object fieldValue = property.GetValue(this);
            if (fieldValue != null && fieldValue != "")
            {
                if (fieldValue is IList enumerable)
                {
                    foreach (var i in (IList)fieldValue)
                    {
                        serializedFields.Append($"{fieldName}={i}&");
                    }
                }
                else
                {
                    serializedFields.Append($"{fieldName}={fieldValue}&");
                }
            }
        }
        if (serializedFields.Length > 0)
        {
            serializedFields.Length--;
        }
        return serializedFields.ToString();
    }
}
