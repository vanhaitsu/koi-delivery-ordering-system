using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Data.Base;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Data.Repository;
using KoiDeliveryOrderingSystem.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Service
{
    public interface IShipmentTrackingService
    {
        Task<IBusinessResult> GetAll(ShipmentTrackingFilterModel model);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(ShipmentTracking shipmentTracking);
        Task<IBusinessResult> Update(ShipmentTracking shipmentTracking);
        Task<IBusinessResult> Save(ShipmentTracking shipmentTracking);
        Task<IBusinessResult> DeleteById(int id);
    }

    public class ShipmentTrackingService : IShipmentTrackingService
    {
        private readonly UnitOfWork _unitOfWork;

        public ShipmentTrackingService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Create(ShipmentTracking shipmentTracking)
        {
            if (shipmentTracking == null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipmentTrackingRepository.CreateAsync(shipmentTracking);

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
            var shipmentTracking = await _unitOfWork.ShipmentTrackingRepository.GetByIdAsync(id);

            if (shipmentTracking == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipmentTrackingRepository.RemoveAsync(shipmentTracking);

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


        public async Task<IBusinessResult> GetAll(ShipmentTrackingFilterModel model)
        {
            var shipmentTrackings = await _unitOfWork.ShipmentTrackingRepository.GetAllAsync(model);

            if (shipmentTrackings == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ShipmentTracking>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentTrackings);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var shipmentTracking = await _unitOfWork.ShipmentTrackingRepository.GetByIdAsync(id);

            if (shipmentTracking == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new ShipmentTracking());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentTracking);
        }


        public async Task<IBusinessResult> Update(ShipmentTracking shipmentTracking)
        {
            if (shipmentTracking == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipmentTrackingRepository.UpdateAsync(shipmentTracking);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipmentTracking);
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

        public async Task<IBusinessResult> Save(ShipmentTracking shipmentTracking)
        {
            try
            {
                int result = -1;

                var shipmentTrackingTmp = await _unitOfWork.ShipmentTrackingRepository.GetByIdAsync(shipmentTracking.TrackingId);

                if (shipmentTrackingTmp != null)
                {
                    // Update các field thủ công
                    shipmentTrackingTmp.ShipperId = shipmentTracking.ShipperId;
                    shipmentTrackingTmp.OrderId = shipmentTracking.OrderId;
                    shipmentTrackingTmp.CurrentLocation = shipmentTracking.CurrentLocation;
                    shipmentTrackingTmp.ShipmentStatus = shipmentTracking.ShipmentStatus;
                    shipmentTrackingTmp.TemperatureDuringTransit = shipmentTracking.TemperatureDuringTransit;
                    shipmentTrackingTmp.HumidityDuringTransit = shipmentTracking.HumidityDuringTransit;
                    shipmentTrackingTmp.HandlerName = shipmentTracking.HandlerName;
                    shipmentTrackingTmp.Remarks = shipmentTracking.Remarks;
                    shipmentTrackingTmp.EstimatedArrival = shipmentTracking.EstimatedArrival;

                    result = await _unitOfWork.ShipmentTrackingRepository.UpdateAsync(shipmentTrackingTmp);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipmentTracking);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ShipmentTrackingRepository.CreateAsync(shipmentTracking);

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
