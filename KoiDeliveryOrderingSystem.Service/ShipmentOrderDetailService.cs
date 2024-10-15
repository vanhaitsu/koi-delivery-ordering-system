using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Service.Base;

namespace KoiDeliveryOrderingSystem.Service
{
    public interface IShipmentOrderDetailService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(ShipmentOrderDetail shipmentOrderDetail);
        Task<IBusinessResult> Update(ShipmentOrderDetail shipmentOrderDetail);
        Task<IBusinessResult> Save(ShipmentOrderDetail shipmentOrderDetail);
        Task<IBusinessResult> DeleteById(int id);
    }

    public class ShipmentOrderDetailService : IShipmentOrderDetailService
    {
        private readonly UnitOfWork _unitOfWork;

        public ShipmentOrderDetailService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Create(ShipmentOrderDetail shipmentOrderDetail)
        {
            if (shipmentOrderDetail == null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipmentOrderDetailRepository.CreateAsync(shipmentOrderDetail);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            var shipmentOrderDetail = await _unitOfWork.ShipmentOrderDetailRepository.GetByIdAsync(id);

            if (shipmentOrderDetail == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipmentOrderDetailRepository.RemoveAsync(shipmentOrderDetail);

                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


        public async Task<IBusinessResult> GetAll()
        {
            var shipmentOrderDetail = await _unitOfWork.ShipmentOrderDetailRepository.GetAllAsync();

            if (shipmentOrderDetail == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ShipmentTracking>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentOrderDetail);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var shipmentOrderDetail = await _unitOfWork.ShipmentOrderDetailRepository.GetByIdAsync(id);

            if (shipmentOrderDetail == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new ShipmentTracking());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentOrderDetail);
        }


        public async Task<IBusinessResult> Update(ShipmentOrderDetail shipmentOrderDetail)
        {
            if (shipmentOrderDetail == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipmentOrderDetailRepository.UpdateAsync(shipmentOrderDetail);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipmentOrderDetail);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(ShipmentOrderDetail shipmentOrderDetail)
        {
            try
            {
                int result = -1;

                var shipmentOrderDetailTmp = await _unitOfWork.ShipmentOrderDetailRepository.GetByIdAsync(shipmentOrderDetail.ShipmentOrderDetailId);

                if (shipmentOrderDetailTmp != null)
                {
                    // Update các field thủ công
                    shipmentOrderDetailTmp.ShipmentOrderId = shipmentOrderDetail.ShipmentOrderId;
                    shipmentOrderDetailTmp.AnimalTypeId = shipmentOrderDetail.AnimalTypeId;
                    shipmentOrderDetailTmp.AdditionalServices = shipmentOrderDetail.AdditionalServices;
                    shipmentOrderDetailTmp.Fee = shipmentOrderDetail.Fee;
                    shipmentOrderDetailTmp.Status = shipmentOrderDetail.Status;
                    shipmentOrderDetailTmp.Weight = shipmentOrderDetail.Weight;
                    shipmentOrderDetailTmp.Length = shipmentOrderDetail.Length;
                    shipmentOrderDetailTmp.DateOfEntry = shipmentOrderDetail.DateOfEntry;
                    shipmentOrderDetailTmp.Gender = shipmentOrderDetail.Gender;
                    shipmentOrderDetailTmp.Color = shipmentOrderDetail.Color;
                    shipmentOrderDetailTmp.HealthStatus = shipmentOrderDetail.HealthStatus;
                    shipmentOrderDetailTmp.Age = shipmentOrderDetail.Age;
                    shipmentOrderDetailTmp.Achievement = shipmentOrderDetail.Achievement;
                    shipmentOrderDetailTmp.Origin = shipmentOrderDetail.Origin;
                    shipmentOrderDetailTmp.ImageUrl = shipmentOrderDetail.ImageUrl;
                    shipmentOrderDetailTmp.Description = shipmentOrderDetail.Description;

                    result = await _unitOfWork.ShipmentOrderDetailRepository.UpdateAsync(shipmentOrderDetailTmp);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipmentOrderDetail);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ShipmentOrderDetailRepository.CreateAsync(shipmentOrderDetail);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
