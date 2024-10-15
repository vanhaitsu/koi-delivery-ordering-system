using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Service
{
    public interface IUserService
    {
        Task<IBusinessResult> GetAll();
    }
    public class UserService : IUserService
    {

        private readonly UnitOfWork _unitOfWork;
        public UserService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            if (users == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Shipper>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
            }
        }
    }
}
