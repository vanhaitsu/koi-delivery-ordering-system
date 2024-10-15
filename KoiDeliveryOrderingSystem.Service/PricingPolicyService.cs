using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;

namespace KoiDeliveryOrderingSystem.Service
{

  public interface IPricingPolicyService
  {
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetById(int id);
    Task<IBusinessResult> Create(PricingPolicy pricingPolicy);
    Task<IBusinessResult> Update(PricingPolicy pricingPolicy);
    Task<IBusinessResult> Save(PricingPolicy pricingPolicy);
    Task<IBusinessResult> DeleteById(int id);
  }

  public class PricingPolicyService
  {
    private readonly UnitOfWork _unitOfWork;

    public PricingPolicyService()
    {
      _unitOfWork ??= new UnitOfWork();
    }

    public async Task<IBusinessResult> GetAll()
    {
      List<PricingPolicy> shipmentTrackings = await _unitOfWork.PricingPolicyRepository.GetAllAsync();

      if (shipmentTrackings == null)
      {
        return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<PricingPolicy>());
      }
      else
      {
        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentTrackings);
      }
    }

    public async Task<IBusinessResult> GetById(int id)
    {
      PricingPolicy? packagingProcess = await _unitOfWork.PricingPolicyRepository.GetByIdAsync(id);

      if (packagingProcess is null)
      {
        return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new PricingPolicy());
      }

      return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, packagingProcess);
    }

    public async Task<IBusinessResult> Create(PricingPolicy pricingPolicy)
    {
      if (pricingPolicy is null)
      {
        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
      }

      try
      {
        int result = await _unitOfWork.PricingPolicyRepository.CreateAsync(pricingPolicy);

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

    public async Task<IBusinessResult> Update(PricingPolicy pricingPolicy)
    {
      if (pricingPolicy is null)
      {
        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
      }

      try
      {
        int result = await _unitOfWork.PricingPolicyRepository.UpdateAsync(pricingPolicy);

        if (result > 0)
        {
          return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, pricingPolicy);
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

    public async Task<IBusinessResult> DeleteById(int id)
    {
      PricingPolicy? pricingPolicy = await _unitOfWork.PricingPolicyRepository.GetByIdAsync(id);

      if (pricingPolicy is null)
      {
        return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
      }

      try
      {
        bool result = await _unitOfWork.PricingPolicyRepository.RemoveAsync(pricingPolicy);

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

    public async Task<IBusinessResult> Save(PricingPolicy pricingPolicy)
    {
      try
      {
        int result = -1;

        PackagingProcess pricingPolicyTmp = await _unitOfWork.PackagingProcessRepository.GetByIdAsync(pricingPolicy.PricingId);

        if (pricingPolicyTmp != null)
        {
          result = await _unitOfWork.PricingPolicyRepository.UpdateAsync(pricingPolicy);

          if (result > 0)
          {
            return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, pricingPolicy);
          }
          else
          {
            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
          }
        }
        else
        {
          result = await _unitOfWork.PricingPolicyRepository.CreateAsync(pricingPolicy);

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
