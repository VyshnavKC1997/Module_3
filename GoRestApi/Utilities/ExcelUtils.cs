using ExcelDataReader;
using GoRestApi.ExcelClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoRestApi.Utilities
{
    internal class ExcelUtils<T>
    {
        public static List<UserClass> ReadExcelData(string excelfilepath, Func<T> func)
        {
            List<UserClass> excelDatas = new List<UserClass>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(excelfilepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    var dataTable = result.Tables["User"];
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UserClass excelData = new()
                        {
                            Id = row["Id"].ToString(),
                            Name = row["Name"].ToString(),
                            Gender = row["Gender"].ToString(),
                            Email = row["Email"].ToString(),
                            Status = row["Status"].ToString(),  

                        };
                        excelDatas.Add(excelData);
                    }
                }
            }
            return excelDatas;
        }
    }
}
