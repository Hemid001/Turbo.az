using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.BusinessLayer.Model.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public void Success(T data)
        {
            IsSuccess = true;
            Data = data;
            Errors = null;
        }
        public void Error(List<string> errorList)
        {
            Errors = errorList;
            IsSuccess = false;
            Data = default(T);
        }
        public void Error(string error)
        {
            Errors = new List<string> { error };
            IsSuccess = false;
            Data = default(T);
        }
    }
}
