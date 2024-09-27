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
        public interface IShipperService
        {
            Task<IBusinessResult> GetAll();
            Task<IBusinessResult> GetById(int id);
            Task<IBusinessResult> Create(Shipper shipper);
            Task<IBusinessResult> Update(Shipper shipper);
            Task<IBusinessResult> Save(Shipper shipper);
            Task<IBusinessResult> DeleteById(int id);
        }

    public class ShipperService : IShipperService
    {
        private readonly UnitOfWork _unitOfWork;

        public ShipperService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Create(Shipper shipper)
        {
            if (shipper == null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipperRepository.CreateAsync(shipper);

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
            var shipper = await _unitOfWork.ShipperRepository.GetByIdAsync(id);

            if (shipper == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipperRepository.RemoveAsync(shipper);

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
            var shippers = await _unitOfWork.ShipperRepository.GetAllAsync();

            if (shippers == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Shipper>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shippers);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var shipper = await _unitOfWork.ShipperRepository.GetByIdAsync(id);

            if (shipper == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new Shipper());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipper);
        }


        public async Task<IBusinessResult> Update(Shipper shipper)
        {
            if (shipper == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.ShipperRepository.UpdateAsync(shipper);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipper);
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

        public async Task<IBusinessResult> Save(Shipper shipper)
        {
            try
            {
                int result = -1;

                var shipmentTrackingTmp = await _unitOfWork.ShipperRepository.GetByIdAsync(shipper.ShipperId);

                if (shipmentTrackingTmp != null)
                {
                    result = await _unitOfWork.ShipperRepository.UpdateAsync(shipper);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipper);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ShipperRepository.CreateAsync(shipper);

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
