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
    public interface IPackagingProcessService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(PackagingProcess packagingProcess);
        Task<IBusinessResult> Update(PackagingProcess packagingProcess);
        Task<IBusinessResult> Save(PackagingProcess packagingProcess);
        Task<IBusinessResult> DeleteById(int id);
    }
    public class PackagingProcessService : IPackagingProcessService
    {
        private readonly UnitOfWork _unitOfWork;

        public PackagingProcessService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> Create(PackagingProcess packagingProcess)
        {
            if (packagingProcess is null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.PackagingProcessRepository.CreateAsync(packagingProcess);

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
            var packagingProcess = await _unitOfWork.PackagingProcessRepository.GetByIdAsync(id);

            if (packagingProcess is null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            try
            {
                var result = await _unitOfWork.PackagingProcessRepository.RemoveAsync(packagingProcess);

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
            var packagingProcesses = await _unitOfWork.PackagingProcessRepository.GetAllAsync();

            if (packagingProcesses == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ShipmentTracking>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, packagingProcesses);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var packagingProcess = await _unitOfWork.PackagingProcessRepository.GetByIdAsync(id);

            if (packagingProcess is null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new ShipmentTracking());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, packagingProcess);
        }

        public async Task<IBusinessResult> Save(PackagingProcess packagingProcess)
        {
            try
            {
                int result = -1;

                var shipmentTrackingTmp = await _unitOfWork.PackagingProcessRepository.GetByIdAsync(packagingProcess.PackagingProcessId);

                if (shipmentTrackingTmp != null)
                {
                    result = await _unitOfWork.PackagingProcessRepository.UpdateAsync(packagingProcess);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, packagingProcess);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.PackagingProcessRepository.CreateAsync(packagingProcess);

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


        public async Task<IBusinessResult> Update(PackagingProcess packagingProcess)
        {
            if (packagingProcess is null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }

            try
            {
                var result = await _unitOfWork.PackagingProcessRepository.UpdateAsync(packagingProcess);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, packagingProcess);
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
    }
}
