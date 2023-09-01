using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace YaDea.Messaging.EpPlus.Extension
{
    /// <summary>
    /// Excel扩展
    /// </summary>
    public static class ExcelExtend
    {
        /// <summary>
        /// 将与HttpRequest一起发送的文件转化成集合对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="formFile">与HttpRequest一起发送的文件</param>
        /// <param name="fromRow">重那一会开始读取</param>
        /// <param name="nullIdentification">空标识</param>
        /// <returns>集合对象</returns>
        public static List<T> LoadFromExcel<T>(IFormFile formFile, int fromRow,  string nullIdentification = "/") where T : new()
        {
            var resultList = new List<T>();
            var dictHeader = new Dictionary<string, int>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage(formFile.OpenReadStream());
            var worksheet = package.Workbook.Worksheets[0];
            //工作区开始列
            var colStart = worksheet.Dimension.Start.Column;
            //工作区结束列
            var colEnd = worksheet.Dimension.End.Column;
            //工作区开始行号
            var rowStart = fromRow;
            //工作区结束行号
            var rowEnd = worksheet.Dimension.End.Row;

            //将每列标题添加到字典中
            for (var i = colStart; i <= colEnd; i++)
            {
                dictHeader[worksheet.Cells[rowStart, i].Value.ToString()] = i;
            }
            var type = typeof(T);
            var properties = type.GetProperties();
            for (var row = rowStart + 1; row <= rowEnd; row++)
            {
                var result = new T();
                foreach (var propertyInfo in properties)
                {
                    try
                    {
                        var customAttributes = propertyInfo.GetCustomAttributes(true);
                        var obj = customAttributes.FirstOrDefault(t => t is DescriptionAttribute);
                        if (!(obj is DescriptionAttribute descriptionAttribute))
                        {
                            continue;
                        }
                        if (!dictHeader.ContainsKey(descriptionAttribute.Description))
                        {
                            continue;
                        }

                        var cell = worksheet.Cells[row, dictHeader[descriptionAttribute.Description]]; //与属性名对应的单元格
                        if (cell.Value == null)
                        {
                            continue;
                        }
                        var propertyType = propertyInfo.PropertyType;
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            var value = cell.GetValue<string>();
                            if (value == null || string.IsNullOrWhiteSpace(value) || value == nullIdentification)
                            {
                                propertyInfo.SetValue(result, null);
                                continue;
                            }
                            propertyType = propertyType.GetGenericArguments()[0];
                        }
                        if (propertyType == typeof(string))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<string>());
                        }
                        else if (propertyType == typeof(decimal))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<decimal>());
                        }
                        else if (propertyType == typeof(DateTime))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<DateTime>());
                        }
                        else if (propertyType.IsEnum)
                        {
                            var value = cell.GetValue<string>();
                            var enumValue = EnumConvertByDescription(propertyType, value);
                            propertyInfo.SetValue(result, enumValue);
                        }
                        else if (propertyType == typeof(int))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<int>());
                        }
                        else if (propertyType == typeof(uint))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<uint>());
                        }
                        else if (propertyType == typeof(long))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<long>());
                        }
                        else if (propertyType == typeof(double))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<double>());
                        }
                        else if (propertyType == typeof(bool))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<bool>());
                        }
                        else if (propertyType == typeof(byte))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<byte>());
                        }
                        else if (propertyType == typeof(char))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<char>());
                        }
                        else if (propertyType == typeof(float))
                        {
                            propertyInfo.SetValue(result, cell.GetValue<float>());
                        }
                    }
                    catch (KeyNotFoundException ex)
                    {
                    }
                }
                resultList.Add(result);
            }

            return resultList;
        }

        /// <summary>
        /// 根据Description转化成对应的枚举值
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="desc">描述</param>
        /// <returns>结果</returns>
        private static object EnumConvertByDescription(Type type, string desc)
        {
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            var field = fields.FirstOrDefault(w => (w.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description == desc);
            return field == null ? default! : Enum.Parse(type, field.Name);
        }

        public static byte[] Export<T>(List<T> list, string author, string title) where T : class
        {
            if (list == null || !list.Any())
            {
                throw new ArgumentNullException(nameof(list));
            }
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var excelPackage = new ExcelPackage();
            excelPackage.Workbook.Properties.Author = author;
            excelPackage.Workbook.Properties.Title = title;
            excelPackage.Workbook.Properties.Created = DateTime.Now;
            excelPackage.Workbook.Worksheets.Add("Sheet1");
            var worksheet = excelPackage.Workbook.Worksheets[0];
            worksheet.Cells.Style.Font.Size = 11f;
            var array = list.First().GetType().GetProperties().Where(p => !Attribute.IsDefined(p, typeof(ExportIgnoreAttribute))).ToArray();
            worksheet.Cells["A1"].LoadFromCollection<T>(list, true, TableStyles.None, BindingFlags.Instance | BindingFlags.Public, array);
            worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column].Style.Font.Bold = true;
            var source = new List<int>();
            for (var index1 = 0; index1 < array.Count(); ++index1)
            {
                var propertyInfo = array[index1];
                var cell = worksheet.Cells[2, index1 + 1, worksheet.Dimension.End.Row, index1 + 1];
                if (propertyInfo.PropertyType.IsGenericType &&
                    propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    source.Add(index1 + 1);
                }
                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    cell.Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                }
                if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                {
                    cell.Style.Numberformat.Format = "0.00";
                }
                if (propertyInfo.PropertyType.IsEnum || propertyInfo.PropertyType.IsNullableEnum())
                {
                    var list1 = list.ToList();
                    for (var index2 = 0; index2 < list1.Count; ++index2)
                    {
                        var obj1 = list1[index2];
                        var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                        var type = !(underlyingType == null) ? underlyingType : propertyInfo.PropertyType;
                        var obj2 = propertyInfo.GetValue(obj1);
                        if (obj2 != null)
                        {
                            var field = type.GetField(obj2.ToString());
                            if (field != null)
                            {
                                object? obj3 = null;
                                foreach (var o in field.GetCustomAttributes(true)
                                             .Where(t => t is DescriptionAttribute))
                                {
                                    obj3 = o;
                                    break;
                                }

                                if (obj3 != null)
                                {
                                    worksheet.Cells[index2 + 2, index1 + 1].Value = ((DescriptionAttribute)obj3).Description;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var column in source.OrderByDescending<int, int>(x => x))
            {
                worksheet.DeleteColumn(column);
            }
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            for (var col = 1; col <= worksheet.Dimension.End.Column; ++col)
            {
                worksheet.Column(col).Width += 3.0;
            }
            return excelPackage.GetAsByteArray();
        }

        public static bool IsNullableEnum(this Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>) && t.GetGenericArguments()[0].IsEnum;


        public class ExportIgnoreAttribute : Attribute
        {
        }

    }
}