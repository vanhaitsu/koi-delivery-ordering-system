using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Data.BaseModels;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;

namespace KoiDeliveryOrderingSystem.Service
{
    public interface IHealthCheckService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetAllWithFilter(HealthCheckFilterModel healthCheckFilterModel);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(HealthCheck healthCheck);
        Task<IBusinessResult> Update(HealthCheck healthCheck);
        Task<IBusinessResult> Save(HealthCheck healthCheck);
        Task<IBusinessResult> DeleteById(int id);
    }
    public class HealthCheckService : IHealthCheckService
    {
        public readonly UnitOfWork _unitOfWork;
        public HealthCheckService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> Create(HealthCheck healthCheck)
        {
            if (healthCheck == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var result = await _unitOfWork.HealCheckRepository.CreateAsync(healthCheck);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, healthCheck);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            var healCheck = await _unitOfWork.HealCheckRepository.GetByIdAsync(id);
            if (healCheck == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var result = await _unitOfWork.HealCheckRepository.RemoveAsync(healCheck);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, healCheck);
                }
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            var healChecks = await _unitOfWork.HealCheckRepository.GetAllAsync();
            if (healChecks == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, healChecks);
            }
        }

        public async Task<IBusinessResult> GetAllWithFilter(HealthCheckFilterModel healthCheckFilterModel)
        {
            var healChecks = await _unitOfWork.HealCheckRepository.GetAllAsync(healthCheckFilterModel);
            if (healChecks == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, healChecks);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var healCheck = await _unitOfWork.HealCheckRepository.GetByIdAsync(id);
            if (healCheck == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, healCheck);
            }
        }

        public async Task<IBusinessResult> Save(HealthCheck healthCheck)
        {
            try
            {
                int result = -1;
                var healthCheckTmp = _unitOfWork.HealCheckRepository.GetById(healthCheck.HealthCheckId);

                if (healthCheckTmp != null)
                {
                    healthCheckTmp.CheckDate = healthCheck.CheckDate;
                    healthCheckTmp.NextCheckupDate = healthCheck.NextCheckupDate;
                    healthCheckTmp.Temperature = healthCheck.Temperature;
                    healthCheckTmp.Recommendations = healthCheck.Recommendations;
                    healthCheckTmp.Condition = healthCheck.Condition;
                    healthCheckTmp.DoctorName = healthCheck.DoctorName;
                    healthCheckTmp.PackagingType = healthCheck.PackagingType;
                    healthCheckTmp.ShipmentOrderDetail = healthCheck.ShipmentOrderDetail;
                    healthCheckTmp.ShipmentOrderDetailId = healthCheck.ShipmentOrderDetailId;
                    healthCheckTmp.ShipmentTrackingId = healthCheck.ShipmentTrackingId;
                    healthCheckTmp.Weight = healthCheck.Weight;

                    result = await _unitOfWork.HealCheckRepository.UpdateAsync(healthCheckTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, healthCheckTmp);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.HealCheckRepository.CreateAsync(healthCheck);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, healthCheckTmp);
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

        public Task<IBusinessResult> Update(HealthCheck healthCheck)
        {
            throw new NotImplementedException();
        }
    }
}
