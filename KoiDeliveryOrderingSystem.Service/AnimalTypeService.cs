using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;

namespace KoiDeliveryOrderingSystem.Service
{
    public interface IAnimalTypeService
    {
        Task<IBusinessResult> GetAll();
        //Task<IBusinessResult> GetById(int id);
        //Task<IBusinessResult> Create(Shipper shipper);
        //Task<IBusinessResult> Update(Shipper shipper);
        //Task<IBusinessResult> Save(Shipper shipper);
        //Task<IBusinessResult> DeleteById(int id);
    }
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly UnitOfWork _unitOfWork;

        public AnimalTypeService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var shippers = await _unitOfWork.AnimalTypeRepository.GetAllAsync();

            if (shippers == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Shipper>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shippers);
            }
        }
    }
}
