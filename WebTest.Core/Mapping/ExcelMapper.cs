using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OneOf;
using OneOf.Types;
using System.Reflection;
using WebTest.Core.Attrubutes;

namespace WebTest.Mapping;

public static class ExcelMapper
{
    public static List<T> MapExcelToObject<T>(this MemoryStream memoryStream) where T : new()
    {
        var workbook = new XSSFWorkbook(memoryStream);
        var result = new List<T>(workbook.NumberOfSheets * 200);

        var propertiesCount = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<NotMap>() is null)
            .Count();
        for (int i = 0; i < workbook.NumberOfSheets; i++)
        {
            var sheet = workbook[i];
            for (int j = sheet.FirstRowNum + 4; j <= sheet.LastRowNum; j++)
            {
                var row = sheet.GetRow(j);
                if (row is null || row.Cells.Count < propertiesCount)
                {
                    continue;
                }

                var obj = MapRowToType<T>(row);
                result.Add(obj);
            }
        }

        return result;
    }

    private static T MapRowToType<T>(IRow row) where T : new()
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<NotMap>() is null)
            .ToArray();
        var obj = new T();

        for (int j = 0; j < properties.Length; j++)
        {
            var cell = row.GetCell(j);
            var property = properties[j];
            if (cell is null || property.GetCustomAttribute<NotMap>() is not null)
            {
                continue;
            }

            var value = cell.GetValue();
            var converted = value.ConvertTo(property.PropertyType);
            if (converted != null)
            {
                property.SetValue(obj, converted);
            }
        }

        return obj;
    }

    private static object? ConvertTo(this OneOf<double, string, bool, None> value, Type targetType)
    {
        try
        {
            if (targetType == typeof(DateOnly))
            {
                return DateOnly.FromDateTime(Convert.ToDateTime(value.Value));
            }
            else if (targetType == typeof(TimeOnly))
            {
                return TimeOnly.FromDateTime(Convert.ToDateTime(value.Value));
            }
            else if (targetType == typeof(double) && value.IsT0)
            {
                return value.Value;
            }
            else if (targetType == typeof(string) && value.IsT1)
            {
                return value.Value;
            }
            else if (targetType == typeof(bool) && value.IsT2)
            {
                return value.Value;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    private static OneOf<double, string, bool, None> GetValue(this ICell cell)
    {
        switch (cell.CellType)
        {
            case CellType.Numeric:
                return cell.NumericCellValue;
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Boolean:
                return cell.BooleanCellValue;
            default:
                return new None();
        }
    }
}
